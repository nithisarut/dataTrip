using dataTrip.DTOS.AddMultipleLocations;
using dataTrip.DTOS.Trips;
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
    public class AddMultipleLocationsServiceController : ControllerBase
    {
        private readonly IAddMultipleLocationsService _addMultipleLocationsService;

        public AddMultipleLocationsServiceController(IAddMultipleLocationsService addMultipleLocationsService)
        {
            _addMultipleLocationsService = addMultipleLocationsService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAddMultipleLocations()
        {
            var result = await _addMultipleLocationsService.GetAllAsync();
            return Ok(new { data = result });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddMultipleLocationsByID(int id)
        {
            var result = await _addMultipleLocationsService.GetAsync(id);
            if (result == null) return Ok(new { msg = "ไม่พบข้อมูล" });
            return Ok(new { msg = "OK", data = result });
        }


        [HttpPost("[action]")]
        // [FromQuery] int id ใส่เต็มยศ
        public async Task<ActionResult<AddMultipleLocations>> DeleteAddMultipleLocations([FromQuery] int id)
        {
            var result = await _addMultipleLocationsService.GetAsync(id);
            if (result == null) return Ok(new { msg = "ไม่พบข้อมูล" });
            await _addMultipleLocationsService.RemoveAsync(result);
            return Ok(new { msg = "OK", data = result });
        }

        [HttpPost]
        public async Task<IActionResult> AddAddMultipleLocations([FromForm] AddMultipleLocationsRequest addMultipleLocationsRequest)
        {
            var trip = addMultipleLocationsRequest.Adapt<AddMultipleLocations>();
            await _addMultipleLocationsService.CreactAsync(trip);
            return Ok(new { msg = "OK", data = "" });

        }

    }
}
