using dataTrip.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace dataTrip
{
    public class LocationRequest
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public string Details { get; set; }
        public string District { get; set; }
        public string SubDistrict { get; set; }
        public FormFileCollection? Image { get; set; }
        public int TypeID { get; set; }

    }
}
