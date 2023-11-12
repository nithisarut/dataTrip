using dataTrip.Models;
using dataTrip.Setting;

namespace dataTrip
{
    public class VehicleResponse
    {
        public int? VehicleID { get; set; }
        public string VehicleName { get; set; }
        public string Tel { get; set; }
        public string VehicleRegistration { get; set; }
        public string Company { get; set; }
        public string DriverName { get; set; }
        public int NumberOFSeats { get; set; }
        public bool status { get; set; }
        public string ImageDriver { get; set; }
        public string ImageVehicle { get; set; }

        static public VehicleResponse FromVehicle(Vehicle vehicle)
        {
            return new VehicleResponse
            {
                VehicleID = vehicle.Id,
                VehicleName = vehicle.VehicleName,
                Tel = vehicle.Tel,
                VehicleRegistration = vehicle.VehicleRegistration,
                Company = vehicle.Company,
                status = vehicle.status,
                DriverName = vehicle.DriverName,
                NumberOFSeats = vehicle.NumberOFSeats,
                ImageDriver = !string.IsNullOrEmpty(vehicle.ImageDriver) ? UrlServer.Url + "images/" + vehicle.ImageDriver : "",
                ImageVehicle = !string.IsNullOrEmpty(vehicle.ImageVehicle) ? UrlServer.Url + "images/" + vehicle.ImageVehicle : "",

            };

        }

    }
}
    
