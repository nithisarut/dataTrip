using dataTrip.DTOS.Trips;
using dataTrip.Models;
using dataTrip.Setting;

namespace dataTrip {
    public class PaymentResponse
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public DateTime date { get; set; }
        public bool status { get; set; }

        public int OrDerTripId { get; set; }
        public  OrderTrip OrderTrips { get; set; }

        static public PaymentResponse FromPaymennt(Models.Payment payment)
        {
        return new PaymentResponse
        {
            Id = payment.Id,
            date = payment.date,
            Image = !string.IsNullOrEmpty(payment.Image) ? UrlServer.Url + "images/" + payment.Image : "",
            status = payment.status,
            OrDerTripId = payment.OrDerTripId,
            OrderTrips = payment.OrderTrips,
          


            };
        }
    }
}
