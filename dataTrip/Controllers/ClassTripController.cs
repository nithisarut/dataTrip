using dataTrip.DTOS.ClassTrip;
using dataTrip.DTOS.Role;
using dataTrip.Interfaces;
using dataTrip.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dataTrip.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClassTripController : ControllerBase
    {
        private readonly IClassTripService _classTripService;
        public ClassTripController(IClassTripService classTripService)
        {
            _classTripService = classTripService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetClassTrip()
        {
            var result = await _classTripService.GetAllAsync();
            return Ok(new { data = result });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClassTripByID(int id)
        {
            var result = await _classTripService.GetAsync(id);
            if (result == null) return Ok(new { msg = "ไม่พบข้อมูล" });
            return Ok(new { msg = "OK", data = result });
        }


        [HttpPost("[action]")]
        // [FromQuery] int id ใส่เต็มยศ
        public async Task<ActionResult<ClassTrip>> DeleteClassTrip([FromQuery] int id)
        {
            var result = await _classTripService.GetAsync(id);
            if (result == null) return Ok(new { msg = "ไม่พบข้อมูล" });
            await _classTripService.RemoveAsync(result);
            return Ok(new { msg = "OK", data = result });
        }

        [HttpPost]
        public async Task<IActionResult> AddRole([FromForm] ClassTripRequest classTripRequest)
        {
            var classTrip = classTripRequest.Adapt<ClassTrip>();
            await _classTripService.CreactAsync(classTrip);
            return Ok(new { msg = "OK", data = "" });

        }
    }

}
