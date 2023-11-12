using dataTrip.Models;
using dataTrip.RequestHelpers;

namespace dataTrip.Interfaces
{
    public interface ITripsService
    {
        Task<(string errorMessage, string imageName)> UploadImage(IFormFileCollection formFiles);
        Task<(string errorMessage, string imageName)> UploadFile(IFormFileCollection formFiles);
        Task DeleteImage(string fileName);
        Task<IEnumerable<Trip>> GetAllCarAsync(string searchName = "", string searchCar = "");
        Task<IEnumerable<AddMultipleLocations>> CreateAddMultipleLocation(Trip trip, List<string> Location);
        Task<IEnumerable<TripDto>> GetAllAsync(TripParams tripParams);
        Task CreactAsync(Trip entity);
        Task UpdateAsync(Trip entity);
        Task RemoveAsync(Trip entity);
        Task<TripDto> GetAsync(int id, bool tracked = true);
        Task<IEnumerable<Trip>> FindNew();


    }
}
