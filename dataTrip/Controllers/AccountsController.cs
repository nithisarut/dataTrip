using dataTrip.DTOS.Accounts;
using dataTrip.DTOS.Login;
using dataTrip.Interfaces;
using dataTrip.Models;
using dataTrip.Services;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dataTrip.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService accountsService;

        public AccountsController(IAccountsService accountsService)
        {
            this.accountsService = accountsService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromForm] LoginRequest loginRequest)
        {
            var result = await accountsService.Login(loginRequest.Email, loginRequest.Password);
            if (result == null)
            {
                return Ok(new { msg = "เข้าสู่ระบบไม่สำเร็จ" });
            }
            var token = accountsService.GenerateToken(result);
            return Ok(new { msg = "OK", data = AccountsResponse
                .FromAccount(result), token });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterAccount([FromForm] RegisterRequest registerRequest)
        {
            #region จัดการรูปภาพ
            (string erorrMesage, string imageName) = await accountsService.UploadImage(registerRequest.FormFiles);
            if (!string.IsNullOrEmpty(erorrMesage)) return BadRequest(erorrMesage);
            #endregion


            var account = registerRequest.Adapt<Accounts>();
            account.Image = imageName;  
            var data = await accountsService.Register(account);

            if (data != null) return Ok(data);
            return Ok(new { msg = "OK", data = account });
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetAccountByID(int id)
        {
            var result =  await accountsService.GetByID(id);
            if (result == null)
            {
                return Ok(new { msg = "ไม่มีผู้ใช้งานนี้" });
            }
            return Ok(AccountsResponse.FromAccount(result));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAccount ()
        {
            var result = (await accountsService.GetAll()).Select(AccountsResponse.FromAccount);
          return Ok(new { data = result });
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Accounts>> UpdateAccount(int id, [FromForm] RegisterRequest registerRequest)
        {

            var result = await accountsService.GetByID(id);
            if (result == null)
            {
                return Ok(new { msg = "ไม่พบผู้ใช้" });
            }

            #region จัดการรูปภาพ
            if (registerRequest.FormFiles != null)
            {
                (string erorrMesage, string imageName) = await accountsService.UploadImage(registerRequest.FormFiles);
                if (!string.IsNullOrEmpty(erorrMesage)) return BadRequest(erorrMesage);

                if (!string.IsNullOrEmpty(imageName))
                {
                   
                    await accountsService.DeleteImage(result.Image);
                    result.Image = imageName;
                }
            }
            #endregion

            var account = registerRequest.Adapt(result);
            if (registerRequest.FormFiles != null)
            {
                account.Image = result.Image;
            }
            await accountsService.UpdateAccount(account);
            return Ok(new { msg = "OK", data = AccountsResponse.FromAccount(account) });
        }
    }
}
