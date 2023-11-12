using dataTrip.Models;

namespace dataTrip.Interfaces
{
    public interface IImagesService 
    {
        Task<(string errorMessage, List<string> imageName)> UploadImage(IFormFileCollection formFiles);
        Task DeleteImage(string fileName);

        Task<ICollection<Images>> GetAllAsync();
        Task<Images> GetAsync(int id, bool tracked = true);
        Task CreactAsync(Images images, List<string> imageName);
        Task UpdateAsync(Images images);
        Task RemoveAsync(Images images);

        Task<List<Images>> GetImageLocation(int id);
    }
}
