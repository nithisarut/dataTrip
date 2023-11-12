using dataTrip.DTOS.OrderTrip;
using dataTrip.DTOS.Role;
using dataTrip.DTOS.Trips;
using dataTrip.Interfaces;
using dataTrip.Models;
using dataTrip.Services;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dataTrip.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderTripsController : ControllerBase
    {
        private  readonly IOrderTripService _orderTripService;
        private readonly ITripsService _tripsService;
        private readonly DatabaseContext db;

        public OrderTripsController(IOrderTripService orderTripService , ITripsService tripsService, DatabaseContext db)
        {
            _orderTripService = orderTripService;
            _tripsService = tripsService;
            this.db = db;
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetOrderTrip()
        {
            var result = await _orderTripService.GetAllAsync();
            return Ok(new { data = result });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderTripByID(int id)
        {
            var result = await _orderTripService.GetAsync(id);
            if (result == null) return Ok(new { msg = "ไม่พบข้อมูล" });
            return Ok(new { msg = "OK", data = result });
        }


        [HttpPost("[action]")]
        // [FromQuery] int id ใส่เต็มยศ
        public async Task<ActionResult<Role>> DeleteOrderTrip([FromQuery] int id)
        {
            var result = await _orderTripService.GetAsync(id);
            if (result == null) return Ok(new { msg = "ไม่พบข้อมูล" });
            await _orderTripService.RemoveAsync(result);
            return Ok(new { msg = "OK", data = result });
        }

        [HttpPost]
        public async Task<IActionResult> AddOrderTrip([FromForm] OrderTripRequest orderTripRequest)
        {
            var tripDto = await db.Trips.AsNoTracking().FirstOrDefaultAsync(e => e.Id == orderTripRequest.TripId);

            var role = orderTripRequest.Adapt<OrderTrip>();

            tripDto.Amount -= (orderTripRequest.AmountAdult + orderTripRequest.AmountKid);

            await _tripsService.UpdateAsync(tripDto);
            role.Created = DateTime.Now;
            await _orderTripService.CreactAsync(role);
            return Ok(new { msg = "OK", data = role });

        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetByIdOrderAccountAsync(int id)
        {
            var result = await _orderTripService.GetByIdOrderAccountAsync(id);
            return Ok( result.Select(OrderTripResponse.FromOrderTrip));
         
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<OrderTrip>> UpdateOrderTrip([FromForm] OrderTripRequest orderTripRequest)
        {
            var result = await _orderTripService.GetAsync(orderTripRequest.Id, tracked: false);
            if (result == null)
            {
                return Ok(new { msg = "ไม่พบสินค้า" });
            }

            var orderTrip = orderTripRequest.Adapt<OrderTrip>();

            await _orderTripService.UpdateAsync(orderTrip);
            return Ok(new { msg = "OK", data = orderTrip });
        }


    }
}
