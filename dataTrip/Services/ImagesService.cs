using dataTrip.Interfaces;
using dataTrip.Models;
using Microsoft.EntityFrameworkCore;

namespace dataTrip.Services
{
    public class ImagesService : IImagesService
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IUploadFileService _uploadFileService;

        public ImagesService(DatabaseContext databaseContext, IUploadFileService uploadFileService)
        {
            _databaseContext = databaseContext;
            _uploadFileService = uploadFileService;
        }

        public async Task CreactAsync(Images images, List<string> imageName)
        {
            List<Images> image777 = new();
            for (var i = 0; i < imageName.Count; i++)
            {
                image777.Add(new Images { ImageSum = imageName[i] , LocationId = images.LocationId});
            }
            await _databaseContext.AddRangeAsync(image777);
            await _databaseContext.SaveChangesAsync();
        }

 

        public async  Task DeleteImage(string fileName)
        {
            await _uploadFileService.DeleteImage(fileName);
        }

        public async Task<ICollection<Images>> GetAllAsync()
        {
            return await _databaseContext.Images.ToListAsync();
        }

        public async  Task<Images> GetAsync(int id, bool tracked = true)
        {

            IQueryable<Images> query = _databaseContext.Images;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

      

        public async Task RemoveAsync(Images images)
        {
            _databaseContext.Remove(images);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Images images)
        {
            _databaseContext.Update(images);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<(string errorMessage, List<string> imageName)> UploadImage(IFormFileCollection formFiles)
        {
            var errorMessage = string.Empty;
            var imageName = new List<string>(); //หลายรูป
            if (_uploadFileService.IsUpload(formFiles))
            {
                errorMessage = _uploadFileService.Validation(formFiles);
                if (string.IsNullOrEmpty(errorMessage))
                {
                    imageName = (await _uploadFileService.UploadImages(formFiles));
                }
            }
            return (errorMessage, imageName);
        }

        public async Task<List<Images>> GetImageLocation(int id) => await _databaseContext.Images.Where(x => x.LocationId == id).ToListAsync();
            
        





    }
}
