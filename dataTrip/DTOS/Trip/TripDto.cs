using dataTrip.Models;

namespace dataTrip
{
    public class TripDto
    {
        public int Id { get; set; }
        public string TripName { get; set; }
        public string Detail { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }
        public string ImageTrip { get; set; }
        public string File { get; set; }
        public int ClassTripId { get; set; }
        public ClassTrip ClassTrip { get; set; }
        public int TypeID { get; set; }
        public Type Type { get; set; }

        public int VehicleID { get; set; }
        public  Vehicle Vehicle { get; set; }
        public List<AddMultipleLocations> addMultipleLocations { get; set; }
    }
}
