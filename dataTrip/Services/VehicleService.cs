using dataTrip.Interfaces;
using dataTrip.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace dataTrip.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly DatabaseContext _db;
        private readonly IUploadFileService _uploadFileService;

        public VehicleService(DatabaseContext db , IUploadFileService uploadFileService)
        {
            _db = db;
            _uploadFileService = uploadFileService;
        }
        public async Task CreactAsync(Vehicle vehicle)
        {
            await _db.AddAsync(vehicle);
            await _db.SaveChangesAsync();
        }

        

        public async Task<ICollection<Vehicle>> GetAllAsync()
        {
            return await _db.Vehicles.ToListAsync(); 
        }

        public async Task<Vehicle> GetAsync(int id, bool tracked = true)
        {
            IQueryable<Vehicle> query = _db.Vehicles;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async  Task RemoveAsync(Vehicle vehicle)
        {
            _db.Remove(vehicle);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Vehicle vehicle)
        {
            _db.Vehicles.Update(vehicle);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteImage(string fileName)
        {
            await _uploadFileService.DeleteImage(fileName);
        }

        public async Task<(string errorMessageVehicle, string imageNameVehicle)> UploadImage1(IFormFileCollection formFiles)
        {

            var errorMessageVehicle = string.Empty;
            //var imageName = new List<string>();
            var imageNameVehicle = string.Empty;
            if (_uploadFileService.IsUpload(formFiles))
            {
                errorMessageVehicle = _uploadFileService.Validation(formFiles);
                if (string.IsNullOrEmpty(errorMessageVehicle))
                {
                    imageNameVehicle = (await _uploadFileService.UploadImages(formFiles))[0];
                }
            }
            return (errorMessageVehicle, imageNameVehicle);
        }

        public async Task<(string errorMessageDriver, string imageNameDriver)> UploadImage2(IFormFileCollection formFiles)
        {

            var errorMessageDriver = string.Empty;
            //var imageName = new List<string>();
            var imageNameDriver = string.Empty;
            if (_uploadFileService.IsUpload(formFiles))
            {
                errorMessageDriver = _uploadFileService.Validation(formFiles);
                if (string.IsNullOrEmpty(errorMessageDriver))
                {
                    imageNameDriver = (await _uploadFileService.UploadImages(formFiles))[0];
                }
            }
            return (errorMessageDriver, imageNameDriver);
        }

      
    }
}
