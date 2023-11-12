using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dataTrip.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public string Image { get; set; }
        public DateTime date { get; set; }
        public bool status { get; set; }

        public int OrDerTripId { get; set; }
        [ForeignKey("OrDerTripId")]
        public virtual OrderTrip OrderTrips { get; set; }
    }
}
