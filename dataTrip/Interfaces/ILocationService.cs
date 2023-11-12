using dataTrip.Models;

namespace dataTrip.Interfaces
{
    public interface ILocationService : IService <Location>
    {

        Task<IEnumerable<Location>> GetAllTyoeAsync(string searchName = "", string searchType = "");
        Task<(string errorMessage, string imageName)> UploadImage(IFormFileCollection formFiles);
        Task DeleteImage(string fileName);

    }
}
