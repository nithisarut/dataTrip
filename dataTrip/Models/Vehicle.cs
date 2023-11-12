using System.ComponentModel.DataAnnotations;

namespace dataTrip.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }
        public string VehicleName { get; set; }
        public string Tel { get; set; }
        public string VehicleRegistration { get; set; }
        public string Company { get; set; }
        public string DriverName { get; set; }
        public int NumberOFSeats { get; set; }
        public bool status { get; set; }

        public string ImageDriver { get; set; }
        public string ImageVehicle { get; set; }


    }
}
