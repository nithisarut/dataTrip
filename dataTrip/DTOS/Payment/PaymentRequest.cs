namespace dataTrip.DTOS.Payment
{
    public class PaymentRequest
    {
        public int? Id { get; set; }
        public FormFileCollection Image { get; set; }
        public bool Status { get; set; }
        public int OrDerTripId { get; set; }
    }
}
