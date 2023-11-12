using dataTrip.DTOS.Type;
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
    public class TypesController : ControllerBase
    {
        private readonly ITypesService _typesService;
        public TypesController(ITypesService typesService)
        {
            _typesService = typesService;
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllType()
        {
            return Ok(await _typesService.GetAllAsync());
        }

        [HttpPost("[action]")]
        // [FromQuery] int id ใส่เต็มยศ
        public async Task<ActionResult<Trip>> DeleteType([FromQuery] int id)
        {
            var result = await _typesService.GetAsync(id);
            if (result == null) return Ok(new { msg = "ไม่พบข้อมูล" });
            await _typesService.RemoveAsync(result);
            return Ok(new { msg = "OK", data = result });
        }

        [HttpPost]
        public async Task<IActionResult> AddType([FromForm] TypeRequest typeRequest)
        {
            var type = typeRequest.Adapt<Types>();
            await _typesService.CreactAsync(type);
            return Ok(new { msg = "OK", data = "" });

        }
    }
}
