using dataTrip.Models;
using dataTrip.Setting;
using System.ComponentModel.DataAnnotations.Schema;

namespace dataTrip
{
    public class LocationResponse
    {
        public int Id { get; set; }
        public int LocationID { get; set; }
        public string LocationName { get; set; }
        public string Details { get; set; }
        public string District { get; set; }
        public string SubDistrict { get; set; }
        public string Image { get; set; }
        public int TypeID { get; set; }
        public string TypeName { get; set; }

        static public LocationResponse FromLocation(Location location)
        {
            return new LocationResponse
            {
         
                LocationID = location.Id,
                LocationName = location.LocationName,
                Details = location.Details,
                District = location.District,
                SubDistrict = location.SubDistrict,
                Image = !string.IsNullOrEmpty(location.Image) ?  UrlServer.Url + "images/" + location.Image : "",
                TypeID = location.TypeID,
                TypeName = location.Type.TypeName,

            };
        }
    }
}
