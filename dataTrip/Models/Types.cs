using System.ComponentModel.DataAnnotations;

namespace dataTrip.Models
{
    public class Types
    {
        [Key]
        public int Id { get; set; }
        public string TypeName { get; set; }
    }
}
