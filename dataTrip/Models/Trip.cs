using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dataTrip.Models
{
    public class Trip
    {
        [Key]
        public int Id { get; set; }
        public string TripName { get; set; }
        public string Detail {get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }
        public string File { get; set; }
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }
        public string ImageTrip { get; set; }
        public int VehicleID { get; set; }
        [ForeignKey("VehicleID")]
        public virtual Vehicle? Vehicle { get; set; }

        public int ClassTripId { get; set; }
        [ForeignKey("ClassTripId")]
        public virtual ClassTrip? ClassTrip { get; set; }



    }
}
