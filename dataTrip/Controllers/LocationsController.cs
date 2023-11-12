using dataTrip.DTOS.Location;
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
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService _locationService;
        public LocationsController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        //[HttpGet("[action]")]
        //public async Task<IActionResult> GetLocation()
        //{
        //    var result = (await _locationService.GetAllAsync()).Select(LocationResponse.FromLocation);
        //    return Ok(new { data = result });
        //}


        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocationByID(int id)
        {
            var result = LocationResponse.FromLocation( await _locationService.GetAsync(id));
            if (result == null) return Ok(new { msg = "ไม่พบข้อมูล" });
            return Ok(new { msg = "OK", data = result });
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetLocationWiteType(string? searchName = "", string? searchType = "")
        {
            var result = (await _locationService.GetAllTyoeAsync(searchName, searchType)).Select(LocationResponse.FromLocation);
            return Ok(new { data = result });

        }

        [HttpPost("[action]")]
        // [FromQuery] int id ใส่เต็มยศ
        public async Task<ActionResult<Trip>> DeleteLocation([FromQuery] int id)
        {
            var result = await _locationService.GetAsync(id);
            if (result == null) return Ok(new { msg = "ไม่พบข้อมูล" });
            await _locationService.RemoveAsync(result);
            return Ok(new { msg = "OK", data = result });
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<Vehicle>> AddLocation([FromForm] LocationRequest01 loctionRequest)
        {

            (string erorrMesage, string imageName) = await _locationService.UploadImage(loctionRequest.Image);
       
            if (!string.IsNullOrEmpty(erorrMesage) ) return BadRequest(erorrMesage);
            var location = loctionRequest.Adapt<Location>();
            location.Image = imageName;
            await _locationService.CreactAsync(location);
            return Ok(new { msg = "OK", data = location });

        }


        [HttpPost("[action]")]
        public async Task<ActionResult<Location>> UpdateLocation([FromForm] LocationRequest locationRequest)
        {
            var result = await _locationService.GetAsync(locationRequest.Id, tracked: false); 
            if (result == null)
            {
                return Ok(new { msg = "ไม่พบสินค้า" });
            }
            var location = locationRequest.Adapt<Location>();
            #region จัดการรูปภาพ
            (string errorMesage, string imageName) = await _locationService.UploadImage(locationRequest.Image);
            if (!string.IsNullOrEmpty(errorMesage)) return BadRequest(errorMesage);

            if (!string.IsNullOrEmpty(imageName))
            {
                await _locationService.DeleteImage(result.Image);
                location.Image = imageName;
            }
            else
            {
                location.Image = result.Image;
            }
            #endregion



            await _locationService.UpdateAsync(location);
            return Ok(new { msg = "OK", data = location });
        }

    }

}
