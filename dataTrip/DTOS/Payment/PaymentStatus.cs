using dataTrip.Models;

namespace dataTrip.DTOS.Payment
{
    public class PaymentStatus
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public int? OrderId { get; set; }
        public OrderStatus? OrderStatus { get; set; }
    }
}
