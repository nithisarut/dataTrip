using dataTrip.Interfaces;
using dataTrip.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace dataTrip.Services
{
    public class TypesService : ITypesService
    {
        private readonly DatabaseContext _databaseContext;
        public TypesService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task CreactAsync(Types types)
        {
            await _databaseContext.Types.AddAsync(types);
            await _databaseContext.SaveChangesAsync();
        }

      

        public async Task RemoveAsync(Types types)
        {
            _databaseContext.Remove(types);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<ICollection<Types>> GetAllAsync()
        {
            return await _databaseContext.Types.ToListAsync();
        }

        public async Task<Types> GetAsync(int id, bool tracked = true)
        {
            IQueryable<Types> query = _databaseContext.Types;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task UpdateAsync(Types types)
        {
            _databaseContext.Update(types);
            await _databaseContext.SaveChangesAsync();
        }
    }
}
