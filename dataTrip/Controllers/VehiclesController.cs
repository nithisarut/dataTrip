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
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetVehicle()
        {
            var result = (await _vehicleService.GetAllAsync()).Select(VehicleResponse.FromVehicle);
            return Ok(new { data = result });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicleByID(int id)
        {
            var result = VehicleResponse.FromVehicle(await _vehicleService.GetAsync(id));
            if (result == null) return Ok(new { msg = "ไม่พบข้อมูล" });
            return Ok(new { msg = "OK", data = result });
        }


        [HttpPost("[action]")]
        // [FromQuery] int id ใส่เต็มยศ
        public async Task<ActionResult<Vehicle>> DeleteVehicle([FromQuery] int id)
        {
            var result = await _vehicleService.GetAsync(id);
            if (result == null) return Ok(new { msg = "ไม่พบข้อมูล" });
            await _vehicleService.RemoveAsync(result);
            return Ok(new { msg = "OK", data = result });
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<Vehicle>> AddVehicle([FromForm] VehicleRequest vehicleRequest)
        {

            (string erorrMesageVehicle, string imageNameVehicle) = await _vehicleService.UploadImage1(vehicleRequest.ImageVehicle);
            (string erorrMesageDriver, string imageNameDriver) = await _vehicleService.UploadImage2(vehicleRequest.ImageDriver);
            if (!string.IsNullOrEmpty(erorrMesageVehicle) || !string.IsNullOrEmpty(erorrMesageDriver)) return BadRequest(erorrMesageDriver);
            var vehicle = vehicleRequest.Adapt<Vehicle>();
            vehicle.ImageVehicle = imageNameVehicle;
            vehicle.ImageDriver = imageNameDriver;
            await _vehicleService.CreactAsync(vehicle);
            return Ok(new { msg = "OK", data = vehicle });

        }


        [HttpPost("[action]")]
        public async Task<ActionResult<Vehicle>> UpdateVehiclet([FromForm] VehicleUpdate vehicleRequest)
        {
            var result = await _vehicleService.GetAsync(vehicleRequest.Id, tracked: false);
            if (result == null)
            {
                return Ok(new { msg = "ไม่พบสินค้า" });
            }
            var vehicle = vehicleRequest.Adapt<Vehicle>();
            #region จัดการรูปภาพ
            if (vehicleRequest.ImageVehicle != null)
            {
                (string erorrMesageVehicle, string imageNameVehicle) = await _vehicleService.UploadImage1(vehicleRequest.ImageVehicle);
                (string erorrMesageDriver, string imageNameDriver) = await _vehicleService.UploadImage2(vehicleRequest.ImageDriver);
                if (!string.IsNullOrEmpty(erorrMesageVehicle) || !string.IsNullOrEmpty(erorrMesageDriver)) return BadRequest(erorrMesageDriver);

                if (!string.IsNullOrEmpty(imageNameVehicle))
                {
                    await _vehicleService.DeleteImage(result.ImageVehicle);
                    vehicle.ImageVehicle = imageNameVehicle;
                }

                if (!string.IsNullOrEmpty(imageNameDriver))
                {
                    await _vehicleService.DeleteImage(result.ImageDriver);
                    vehicle.ImageDriver = imageNameDriver;
                }
            }
            else
            {
                vehicle.ImageDriver = result.ImageDriver;
                vehicle.ImageVehicle = result.ImageVehicle;
            }
            #endregion

            await _vehicleService.UpdateAsync(vehicle);
            return Ok(new { msg = "OK", data = vehicle });
        }

    }
}
