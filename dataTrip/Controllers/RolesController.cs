using dataTrip.DTOS.Role;
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
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetRole()
        {
            var result = await _roleService.GetAllAsync();
            return Ok(new { data = result });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleByID(int id)
        {
            var result = await _roleService.GetAsync(id);
            if (result == null) return Ok(new { msg = "ไม่พบข้อมูล" });
            return Ok(new { msg = "OK", data = result });
        }


        [HttpPost("[action]")]
        // [FromQuery] int id ใส่เต็มยศ
        public async Task<ActionResult<Role>> DeleteRole([FromQuery] int id)
        {
            var result = await _roleService.GetAsync(id);
            if (result == null) return Ok(new { msg = "ไม่พบข้อมูล" });
            await _roleService.RemoveAsync(result);
            return Ok(new { msg = "OK", data = result });
        }

        [HttpPost]
        public async Task<IActionResult> AddRole([FromForm] RoleRequest roleRequest)
        {
            var role = roleRequest.Adapt<Role>();
            await _roleService.CreactAsync(role);
            return Ok(new { msg = "OK", data = "" });

        }
    }
}
