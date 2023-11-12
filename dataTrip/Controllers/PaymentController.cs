using dataTrip.DTOS.OrderTrip;
using dataTrip.DTOS.Payment;
using dataTrip.DTOS.Role;
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
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IOrderTripService _orderTripService;

        public PaymentController(IPaymentService paymentService , IOrderTripService orderTripService)
        {
            _paymentService = paymentService;
            _orderTripService = orderTripService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetPayment()
        {
            var result = await _paymentService.GetAllAsync();
            return Ok(new { data = result.Select(PaymentResponse.FromPaymennt) });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentByID(int id)
        {
            var result = await _paymentService.GetAsync(id);
            if (result == null) return Ok(new { msg = "ไม่พบข้อมูล" });
            return Ok(new { msg = "OK", data = result });
        }


        [HttpPost("[action]")]
        // [FromQuery] int id ใส่เต็มยศ
        public async Task<ActionResult<Role>> DeletePayment([FromQuery] int id)
        {
            var result = await _paymentService.GetAsync(id);
            if (result == null) return Ok(new { msg = "ไม่พบข้อมูล" });
            await _paymentService.RemoveAsync(result);
            return Ok(new { msg = "OK", data = result });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddPayment([FromForm] PaymentRequest paymentRequest)
        {

            (string erorrMesage, string imageName) = await _paymentService.UploadImage(paymentRequest.Image);

            if (!string.IsNullOrEmpty(erorrMesage)) return BadRequest(erorrMesage);
            var payment = paymentRequest.Adapt<Payment>();
            payment.Image = imageName;
            var order = await _orderTripService.GetAsync(paymentRequest.OrDerTripId, tracked: false);
            order.Status = OrderStatus.PendingApproval;
            await _orderTripService.UpdateAsync(order);
            await _paymentService.CreactAsync(payment);
            return Ok(new { msg = "OK", data = payment });

        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Payment>> UpdatePayment([FromForm] PaymentStatus paymentStatus)
        {
            var result = await _paymentService.GetAsync((int)paymentStatus.Id, tracked: false);
            var order = await _orderTripService.GetAsync((int)paymentStatus.OrderId, tracked: false);
            if (result is null)
                return Ok(new { msg = "ไม่พบสินค้า" });
            
            if(order is not null) 
                order.Status = (OrderStatus)paymentStatus.OrderStatus;
            
            result.status = paymentStatus.Status;
            await _orderTripService.UpdateAsync(order);
            await _paymentService.UpdateAsync(result);
            return Ok(new { msg = "OK", data = result });
        }

    

    }
}
