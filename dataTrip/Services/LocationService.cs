using dataTrip.Interfaces;
using dataTrip.Models;
using Microsoft.EntityFrameworkCore;

namespace dataTrip.Services
{
    public class LocationService : ILocationService
    {
        private readonly DatabaseContext db;
        private readonly IUploadFileService _uploadFileService;

        public LocationService(DatabaseContext db , IUploadFileService uploadFileService)
        {
            this.db = db;
            _uploadFileService = uploadFileService;
        }
        public async Task CreactAsync(Location location)
        {
            await db.AddAsync(location);
            await db.SaveChangesAsync();
        }

      

        public async Task<ICollection<Location>> GetAllAsync()
        {

            return await db.Locations.ToListAsync();

        }

        public async Task<Location> GetAsync(int id, bool tracked = true)
        {
            IQueryable<Location> query = db.Locations;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }



        public async Task RemoveAsync(Location location)
        {
            db.Remove(location);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Location location)
        {
            db.Locations.Update(location);
            await db.SaveChangesAsync();
        }

        public async Task<(string errorMessage, string imageName)> UploadImage(IFormFileCollection formFiles)
        {
            var errorMessag = string.Empty;
            //var imageName = new List<string>();
            var imageName = string.Empty;
            if (_uploadFileService.IsUpload(formFiles))
            {
                errorMessag = _uploadFileService.Validation(formFiles);
                if (string.IsNullOrEmpty(errorMessag))
                {
                    imageName = (await _uploadFileService.UploadImages(formFiles))[0];
                }
            }
            return (errorMessag, imageName);
        }
        public async Task DeleteImage(string fileName)
        {
            await _uploadFileService.DeleteImage(fileName);
        }


         public async Task<IEnumerable<Location>> GetAllTyoeAsync(string searchName="", string searchType="")
        {
            var data = db.Locations.Include(e => e.Type).AsQueryable();
            if (!string.IsNullOrEmpty(searchName))
            {
                data = data.Where(a => a.LocationName.Contains(searchName));
            }

            if (!string.IsNullOrEmpty(searchType))
            {
                data = data.Where(a => a.Type.TypeName.Contains(searchType));
            }
            return data;
        }
    }
}
