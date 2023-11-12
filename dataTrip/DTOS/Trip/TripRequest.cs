using System.ComponentModel.DataAnnotations;

namespace dataTrip.DTOS.Trips
{
    public class TripRequest
    {
        public int? Id { get; set; }

        public string TripName { get; set; }
        public string Detail { get; set; }

        public int Amount { get; set; }
        public int Price { get; set; }

        public IFormFileCollection? File { get; set; }

        public DateTime DateTimeStart { get; set; }
      
        public DateTime DateTimeEnd { get; set; }

        public List<string> Location { get; set; }

        public IFormFileCollection? ImageTrip { get; set; }

        public int VehicleID { get; set; }
        public int ClassTripId { get; set; }



    }
}
