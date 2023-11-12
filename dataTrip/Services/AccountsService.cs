using dataTrip.Interfaces;
using dataTrip.Models;
using dataTrip.Setting;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace dataTrip.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly DatabaseContext databaseContext;
        private readonly IUploadFileService uploadFileService;
        private readonly JwtSetting jwtSetting;

        public AccountsService(DatabaseContext databaseContext, IUploadFileService uploadFileService, JwtSetting jwtSetting)
        {
            this.databaseContext = databaseContext;
            this.uploadFileService = uploadFileService;
            this.jwtSetting = jwtSetting;

        }
        public async Task<Accounts> Login(string email, string password)
        {
            var result = await databaseContext.Accounts.Include(a => a.Role).SingleOrDefaultAsync(e => e.Email == email);
            if (result != null && VerifyPassword(result.Password, password))
            {
                return result;
            }
            return null;
        }

        public async Task<object> Register(Accounts accounts)
        {
            if (accounts.RoleID == 0)
            {
                accounts.RoleID = 1;
            }
            var result = await databaseContext.Accounts.SingleOrDefaultAsync(e => e.Email == accounts.Email);
            if (result != null) return new { msg = "อีเมลซ้ำ" };
            //------------- Password ที่ไม่ผ่านการ Has ---------
            //await AddPassword(customer.ID, customer.Password);


            //------------- Password ที่ผ่านการ Has ---------
            accounts.Password = CreateHashPassword(accounts.Password);
            await databaseContext.Accounts.AddAsync(accounts);
            await databaseContext.SaveChangesAsync();
            return null;
        }


        private bool VerifyPassword(string saltAndHashFromDB, string password)
        {
            // ทำการแยกส่วนเป็น 2 ส่วน เป็นอเร
            var parts = saltAndHashFromDB.Split('.', 2);
            if (parts.Length != 2) return false;
            // ไปเอาเกลือมา
            // Convert.FromBase64String ให้กลับเหมือนเดิมปกติมันเป็นไบต์
            var salt = Convert.FromBase64String(parts[0]);
            var passwordHash = parts[1];
            // นำมาผสมกัน
            string hashed = HashPassword(password, salt);

            return hashed == passwordHash;
        }

        private string HashPassword(string password, Byte[] salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
              password: password,
              salt: salt,
              prf: KeyDerivationPrf.HMACSHA256,
              iterationCount: 100000,
              numBytesRequested: 256 / 8));
            return hashed;
        }

        private string CreateHashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            //Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            var hashed = HashPassword(password, salt);

            var hpw = $"{Convert.ToBase64String(salt)}.{hashed}";
            return hpw;
        }

       
      
            
        public async Task<Accounts> GetByID(int id)
        {
            var result = await databaseContext.Accounts.Include(e => e.Role).AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
            if (result == null) return null;
            return result;
        }

        public async Task UpdateAccount(Accounts accounts)
        {

            databaseContext.Accounts.Update(accounts);
            await databaseContext.SaveChangesAsync();
        }

        public async Task<(string errorMessage, string imageName)> UploadImage(IFormFileCollection formFiles)
        {
            var errorMessage = string.Empty;
            //var imageName = new List<string>();
            var imageName = string.Empty;
            if (uploadFileService.IsUpload(formFiles))
            {
                errorMessage = uploadFileService.Validation(formFiles);
                if (string.IsNullOrEmpty(errorMessage))
                {
                    imageName = (await uploadFileService.UploadImages(formFiles))[0];
                }
            }
            return (errorMessage, imageName);
        }



        public string GenerateToken(Accounts accounts)
        {
            //payload หรือ claim ข้อมูลที่ต้องการเก็บ ใส่อะไรก็ได้//
            //Claim("Sub", account.Username) ใส่ค่าที่ที่ไม่ซ้ำเช่น Username
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub,accounts.Email),
                new Claim("role",accounts.Role.RoleName),
                new Claim("additonal","TestSomething"),
                new Claim("todo day","10/10/99"),

            };

            return BuildToken(claims);
        }

        private string BuildToken(Claim[] claims)
        {
            var expires = DateTime.Now.AddDays(Convert.ToDouble(jwtSetting.Expire)); //ดึงข้อหมดอายุมา เเล้ว + วันที่
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Key)); //ให้ทำการเข้ารหัสอีกครั้ง 1
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //สร้าง Token ของเเท้
            var token = new JwtSecurityToken(
                issuer: jwtSetting.Issuer,
                audience: jwtSetting.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );
            //เขียน Token ออกมา
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Accounts GetInfo(string accessToken)
        {
            //แปลงค่า Token (ถอดรหัส)
            var token = new JwtSecurityTokenHandler().ReadToken(accessToken) as JwtSecurityToken;

            //ค้นหาค่า key ขึ้นมา
            var email = token.Claims.First(claim => claim.Type == "sub").Value;
            var role = token.Claims.First(claim => claim.Type == "role").Value;

            var accounts = new Accounts
            {
                Email = email,
                Role = new Role
                {
                    RoleName = role
                }
            };

            return accounts;
        }

        public async Task<IEnumerable<Accounts>> GetAll()
        {
            var result = await databaseContext.Accounts.Include(e => e.Role).AsNoTracking().ToListAsync();
            return result;
        }

        public async Task DeleteImage(string fileName)
        {
            await uploadFileService.DeleteImage(fileName);
        }
    }

}
