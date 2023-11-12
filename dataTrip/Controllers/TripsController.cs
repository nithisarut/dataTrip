using Autofac.Core;
using dataTrip.DTOS.Location;
using dataTrip.DTOS.Trips;
using dataTrip.DTOS.Type;
using dataTrip.Interfaces;
using dataTrip.Models;
using dataTrip.RequestHelpers;
using dataTrip.Services;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dataTrip.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripsService _tripsService;
        private readonly IVehicleService _vehicleService;
        private readonly DatabaseContext db;

        public TripsController(ITripsService tripsService, IVehicleService vehicleService , DatabaseContext db)
        {
            _tripsService = tripsService;
            _vehicleService = vehicleService;
            this.db = db;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetTrip([FromQuery] TripParams  tripParams)
        {

            var result = (await _tripsService.GetAllAsync(tripParams));
            return Ok(new { data = result });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTripByID(int id)
        {

            var result = await _tripsService.GetAsync(id);
            if (result == null) return Ok(new { msg = "ไม่พบข้อมูล" });
            return Ok(new { msg = "OK", data = result });
        }


        [HttpPost("[action]")]
        // [FromQuery] int id ใส่เต็มยศ
        public async Task<ActionResult<Trip>> DeleteTrip([FromQuery] int id)
        {
            var result = await _tripsService.GetAsync(id, tracked : false);
            if (result == null) return Ok(new { msg = "ไม่พบข้อมูล" });
            await _tripsService.RemoveAsync(result.Adapt<Trip>());
            return Ok(new { msg = "OK", data = result });
        }

        [HttpPost]
        public async Task<IActionResult> AddTrip([FromForm] TripRequest tripRequest)
        {
          
            (string erorrMesage, string imageName) = await _tripsService.UploadImage(tripRequest.ImageTrip);
            (string erorrMesageFile, string fileName) = await _tripsService.UploadFile(tripRequest.File);
            if (!string.IsNullOrEmpty(erorrMesage) || !string.IsNullOrEmpty(erorrMesageFile)) return BadRequest(erorrMesage);
            var trip = tripRequest.Adapt<Trip>();
            trip.File= fileName;
            trip.ImageTrip = imageName;
                await _tripsService.CreactAsync(trip);
                await _tripsService.CreateAddMultipleLocation(trip , tripRequest.Location);
            return Ok(new { msg = "OK", data = trip });

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetTripWitCars(string? searchName = "", string? searchCar = "")
        {
            var result = (await _tripsService.GetAllCarAsync(searchName, searchCar)).Select(TripResponse.FromTrip);
            return Ok(new { data = result });

        }
        
        [HttpPost("[action]")]
        public async Task<IActionResult> ChangeVehiclesStatus(int id)
        {
          var vehicle = await _vehicleService.GetAsync(id , tracked : false);
          vehicle.status = false;
          await  _vehicleService.UpdateAsync(vehicle);
            return Ok(new { msg = "OK" });

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetTripNew()
        {
            var result = (await _tripsService.FindNew()).Select(TripResponse.FromTrip);
            return Ok(new { data = result });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> putUpdateTrip([FromForm] TripRequest body)
        {
            var result = await db.Trips.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(body.Id));
            if (result == null)
            {
                return Ok(new { msg = "ไม่พบสินค้า" });
            }
            var trip = body.Adapt<Trip>();
            #region จัดการรูปภาพ
            (string errorMesage, string imageName) = await _tripsService.UploadImage(body.ImageTrip);
            (string errorfileMesage, string fileName) = await _tripsService.UploadFile(body.File);
            if (!string.IsNullOrEmpty(errorMesage)) return BadRequest(errorMesage);
            if (!string.IsNullOrEmpty(errorfileMesage)) return BadRequest(errorfileMesage);

            if (!string.IsNullOrEmpty(imageName))
                trip.ImageTrip = imageName;
            
            else
                trip.ImageTrip = result.ImageTrip;

            if (!string.IsNullOrEmpty(fileName))
                trip.File = fileName;

            else
                trip.File = result.File;
            #endregion

            await _tripsService.UpdateAsync(trip);
            return Ok(new { msg = "OK", data = trip });

        }

    }
}
