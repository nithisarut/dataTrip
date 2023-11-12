using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace dataTrip.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        public string LocationName { get; set; }
        public string Details { get; set; }
        public string District { get; set; }
        public string SubDistrict { get; set; }
        public string Image { get; set; }
      
            
        public int TypeID { get; set; }
        [ForeignKey("TypeID")]
        public virtual Types Type { get; set; }



    }
}
