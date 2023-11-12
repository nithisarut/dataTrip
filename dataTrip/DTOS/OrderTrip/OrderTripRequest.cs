

using dataTrip.Models;

namespace dataTrip.DTOS.OrderTrip
{
    public class OrderTripRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
        public DateTime ContactTime { get; set; }
        public int AmountAdult { get; set; }
        public int AmountKid { get; set; }
        public double Total { get; set; }
        public int SingleStay { get; set; }
        public int Stay2Persons { get; set; }
        public int Stay3Persons { get; set; }
        public OrderStatus Status { get; set; }

        public int AccountsId { get; set; }

        public int TripId { get; set; }
     

    }
}
