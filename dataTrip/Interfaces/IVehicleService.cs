using dataTrip.Models;

namespace dataTrip.Interfaces
{
    public interface IVehicleService : IService <Vehicle>
    {
        Task<(string errorMessageVehicle, string imageNameVehicle)> UploadImage1(IFormFileCollection formFiles);
        Task<(string errorMessageDriver, string imageNameDriver)> UploadImage2(IFormFileCollection formFiles);
        Task DeleteImage(string fileName);
        Task 
            UpdateAsync(Vehicle vehicle);
    }
}
