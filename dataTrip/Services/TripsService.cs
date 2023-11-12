using dataTrip.Extentions;
using dataTrip.Interfaces;
using dataTrip.Models;
using dataTrip.RequestHelpers;
using Microsoft.EntityFrameworkCore;

namespace dataTrip.Services
{
    public class TripsService : ITripsService
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IUploadFileService _uploadFileService;
        public TripsService(DatabaseContext databaseContext, IUploadFileService uploadFileService)
        {
            _databaseContext = databaseContext;
            _uploadFileService = uploadFileService;
        }

        public async Task<IEnumerable<Trip>> FindNew()
        {
            return await _databaseContext.Trips.Include(a => a.Vehicle).Include(e => e.ClassTrip).OrderByDescending(a => a.ClassTripId).Take(4).ToListAsync();
        }

        public async Task CreactAsync(Trip trip)
        {
            var vehicle = await _databaseContext.Vehicles.FirstOrDefaultAsync(e => e.Id.Equals(trip.VehicleID));
            if (vehicle != null)
            {
                vehicle.status = true;
            }
            _databaseContext.Update(vehicle);
            await _databaseContext.AddAsync(trip);
            await _databaseContext.SaveChangesAsync();
        }

        public Task DeleteImage(string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TripDto>> GetAllAsync(TripParams tripParams)
        {
            return await _databaseContext.Trips.Filter(tripParams.ClassTripId).ProjectProductToProductDTO(_databaseContext).ToListAsync();
        }

        public async Task<TripDto> GetAsync(int id, bool tracked = true)
        {
            IQueryable<Trip> query = _databaseContext.Trips;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            return await query.Include(e => e.Vehicle).ProjectProductToProductDTO(_databaseContext).FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task RemoveAsync(Trip trip)
        {
            _databaseContext.Remove(trip);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Trip trip)
        {
            _databaseContext.Update(trip);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<(string errorMessage, string imageName)> UploadImage(IFormFileCollection formFiles)
        {
            var errorMessage = string.Empty;
            //var imageName = new List<string>();
            var imageName = string.Empty;
            if (_uploadFileService.IsUpload(formFiles))
            {
                errorMessage = _uploadFileService.Validation(formFiles);
                if (string.IsNullOrEmpty(errorMessage))
                {
                    imageName = (await _uploadFileService.UploadImages(formFiles))[0];
                }
            }
            return (errorMessage, imageName);
        }

        public async Task<IEnumerable<Trip>> GetAllCarAsync(string searchName = "", string searchCar = "")
        {
            var data = _databaseContext.Trips.Include(e => e.Vehicle).AsQueryable();
            if (!string.IsNullOrEmpty(searchName))
            {
                data = data.Where(a => a.TripName.Contains(searchName));
            }

            if (!string.IsNullOrEmpty(searchCar))
            {
                data = data.Where(a => a.Vehicle.VehicleName.Contains(searchCar));
            }
            return data;
        }

        public async Task<IEnumerable<AddMultipleLocations>> CreateAddMultipleLocation(Trip trip, List<string> Location)
        {
            List<AddMultipleLocations> addMultipleLocations = new();
            for (int i = 0; i < Location.Count; i++) addMultipleLocations.Add(new AddMultipleLocations { LocationID = Convert.ToInt32(Location[i]), TripID = trip.Id });
            await _databaseContext.AddRangeAsync(addMultipleLocations);
            await _databaseContext.SaveChangesAsync();
            return addMultipleLocations;
        }

        public async Task<(string errorMessage, string imageName)> UploadFile(IFormFileCollection formFiles)
        {
            var errorMessage = string.Empty;
            //var imageName = new List<string>();
            var imageName = string.Empty;
            if (_uploadFileService.IsUpload(formFiles))
            {
                errorMessage = _uploadFileService.Validation(formFiles);
                if (string.IsNullOrEmpty(errorMessage))
                {
                    imageName = (await _uploadFileService.UploadImages(formFiles))[0];
                }
            }
            return (errorMessage, imageName);
        }
    }
}
