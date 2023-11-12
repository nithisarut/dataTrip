using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations.Schema;

namespace dataTrip.Models
{
    public class Images
    {

        [Key]
        public int Id { get; set; }
        public string ImageSum { get; set; }
        public int LocationId { get; set; }
        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }

    }
}
