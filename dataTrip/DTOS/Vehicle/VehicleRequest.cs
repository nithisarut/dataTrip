using dataTrip.Models;

namespace dataTrip
{
    public class VehicleRequest
    {
        



        public string VehicleName { get; set; }
        public string Tel { get; set; }
        public string VehicleRegistration { get; set; }
        public bool status { get; set; }
        public string Company { get; set; }
        public string DriverName { get; set; }
        public int NumberOFSeats { get; set; }

        public IFormFileCollection ImageDriver { get; set; }
        public IFormFileCollection ImageVehicle { get; set; }
    }


  
}
