using dataTrip.Interfaces;
using dataTrip.Models;
using Microsoft.EntityFrameworkCore;

namespace dataTrip.Services
{
    public class AddMultipleLocationsService : IAddMultipleLocationsService
    {
        private readonly DatabaseContext _databaseContext;

        public AddMultipleLocationsService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task CreactAsync(AddMultipleLocations addMultipleLocations)
        {
            await _databaseContext.AddAsync(addMultipleLocations);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<ICollection<AddMultipleLocations>> GetAllAsync()
        {
            return await _databaseContext.AddMultipleLocations.Include(e => e.Location).Include(e => e.Trip).ToListAsync();
        }

        public async Task<AddMultipleLocations> GetAsync(int id, bool tracked = true)
        {
            IQueryable<AddMultipleLocations> query = _databaseContext.AddMultipleLocations.Include(e => e.Location).Include(e => e.Trip);
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task RemoveAsync(AddMultipleLocations addMultipleLocations)
        {
            _databaseContext.Remove(addMultipleLocations);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(AddMultipleLocations addMultipleLocations)
        {
            _databaseContext.Update(addMultipleLocations);
            await _databaseContext.SaveChangesAsync();
        }
    }
}
