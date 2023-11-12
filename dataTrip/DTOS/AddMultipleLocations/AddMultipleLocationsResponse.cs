using dataTrip.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace dataTrip.DTOS.AddMultipleLocations
{
    public class AddMultipleLocationsResponse
    {
        public string Id { get; set; }

        public int LocationID { get; set; }
   

        public int TripID { get; set; }
     
    }
}
