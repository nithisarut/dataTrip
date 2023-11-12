using dataTrip.Interfaces;
using dataTrip.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace dataTrip.Services
{
    public class ClassTripService : IClassTripService
    {
        private readonly DatabaseContext _databaseContext;
        public ClassTripService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public async Task CreactAsync(ClassTrip classTrip)
        {
            await _databaseContext.ClassTrips.AddAsync(classTrip);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<ICollection<ClassTrip>> GetAllAsync()
        {
            return await _databaseContext.ClassTrips.ToListAsync();
        }

        public async Task<ClassTrip> GetAsync(int id, bool tracked = true)
        {

            IQueryable<ClassTrip> query = _databaseContext.ClassTrips;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task RemoveAsync(ClassTrip classTrip)
        {

            _databaseContext.Remove(classTrip);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ClassTrip classTrip)
        {
            _databaseContext.Update(classTrip);
            await _databaseContext.SaveChangesAsync();
        }
    }
}
