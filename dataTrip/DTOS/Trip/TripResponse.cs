using dataTrip.Models;
using dataTrip.Setting;
using System.ComponentModel.DataAnnotations.Schema;

namespace dataTrip.DTOS.Trips
{
    public class TripResponse
    {
        public int? Id { get; set; }
        public string TripName { get; set; }
        public string Detail { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }
        public string ImageTrip { get; set; }
        public string File { get; set; }
        public int VehicleID { get; set; }
        public string VehicleName { get; set; }
        public int ClassTripId { get; set; }
        public string ClassTripName { get; set; }


        static public TripResponse FromTrip(Trip trip)
        {
            return new TripResponse
            {
                Id = trip.Id,
                TripName = trip.TripName,
                Detail = trip.Detail,
                Amount = trip.Amount,
                Price = trip.Price,
                File = trip.File,
                DateTimeStart = trip.DateTimeStart,
                DateTimeEnd = trip.DateTimeEnd,
                ImageTrip = !string.IsNullOrEmpty(trip.ImageTrip) ? UrlServer.Url + "images/" + trip.ImageTrip : "",
                VehicleID = trip.Vehicle.Id,
                VehicleName = trip.Vehicle.VehicleName,
                ClassTripId = trip.ClassTripId,
                ClassTripName = trip.ClassTrip.Name,
             


            };
        }

    }
}
