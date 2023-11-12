using dataTrip.DTOS.Trips;
using dataTrip.Models;
using dataTrip.Setting;

namespace dataTrip.DTOS.OrderTrip
{
    public class OrderTripResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
        public DateTime ContactTime { get; set; }
        public int AmountAdult { get; set; }
        public int AmountKid { get; set; }
        public int Total { get; set; }
        public int SingleStay { get; set; }
        public int Stay2Persons { get; set; }
        public int Stay3Persons { get; set; }
        public int AccountsId { get; set; }

        public OrderStatus Status { get; set; } 
        public int TripId { get; set; }
        public  Trip Trip { get; set; }

        public static OrderTripResponse FromOrderTrip(Models.OrderTrip orderTrip)
        {
            return new OrderTripResponse
            {
                Id = orderTrip.Id,
                Name = orderTrip.Name,
                Tel = orderTrip.Tel,
                ContactTime = orderTrip.ContactTime,
                AmountAdult = orderTrip.AmountAdult,
                AmountKid = orderTrip.AmountKid,
                Total = orderTrip.Total,    
                SingleStay = orderTrip.SingleStay,
                Stay2Persons = orderTrip.Stay2Persons,
                Stay3Persons = orderTrip.Stay3Persons,
                AccountsId = orderTrip.AccountsId,
                TripId = orderTrip.TripId,
                Trip = FromTrip(orderTrip.Trip),
                Status = orderTrip.Status,

            };


        }

        public static Trip FromTrip(Trip trip)
        {
            return new Trip
            {
                TripName = trip.TripName,
                ImageTrip = !string.IsNullOrEmpty(trip.ImageTrip) ? $"{UrlServer.Url}images/{trip.ImageTrip}" : "",
                ClassTripId = trip.ClassTripId,
                Amount = trip.Amount,
                ClassTrip = trip.ClassTrip,
                DateTimeStart = trip.DateTimeStart,
                DateTimeEnd = trip.DateTimeEnd,
                Detail = trip.Detail,
                File = !string.IsNullOrEmpty(trip.File) ? $"{UrlServer.Url}images/{trip.File}" : "",
                Id = trip.Id,
                Price = trip.Price,
                Vehicle = trip.Vehicle,
                VehicleID = trip.VehicleID,

            };
        }
    }
}
