using System.ComponentModel.DataAnnotations;

namespace dataTrip.Models
{
    public class ClassTrip
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
