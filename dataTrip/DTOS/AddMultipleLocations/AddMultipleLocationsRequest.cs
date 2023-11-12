using System.ComponentModel.DataAnnotations;

namespace dataTrip.DTOS.AddMultipleLocations
{
    public class AddMultipleLocationsRequest
    {
        [Key]
        public int Id { get; set; }
        public int LocationID { get; set; }
        public int TripID { get; set; }
    }
}
