
using dataTrip.Models;
using dataTrip.Setting;

namespace dataTrip.DTOS.Accounts
{
    public class AccountsResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string RoleName { get; set; }

        static public AccountsResponse FromAccount(Models.Accounts account)
        {

            return new AccountsResponse
            {
                Id = account.Id,
                Name = account.FullName,
                Image = !string.IsNullOrEmpty(account.Image) ? $"{UrlServer.Url}images/{account.Image}" :"",
                Email = account.Email,
                PhoneNumber = account.PhoneNumber,
                RoleName = account.Role.RoleName,
            };
        }
    }
}
