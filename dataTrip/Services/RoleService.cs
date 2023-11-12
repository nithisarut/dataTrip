using dataTrip.Interfaces;
using dataTrip.Models;
using Microsoft.EntityFrameworkCore;

namespace dataTrip.Services
{
    public class RoleService : IRoleService
    {
        private readonly DatabaseContext _databaseContext;
        public RoleService(DatabaseContext databaseContext)
        {
              _databaseContext = databaseContext;
        }

        public async Task CreactAsync(Role role)
        {
            await _databaseContext.Roles.AddAsync(role);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<ICollection<Role>> GetAllAsync()
        {
            return await _databaseContext.Roles.ToListAsync();
        }

        public async Task<Role> GetAsync(int id, bool tracked = true)
        {
            IQueryable<Role> query = _databaseContext.Roles;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task RemoveAsync(Role role)
        {
            _databaseContext.Remove(role);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Role role)
        {
            _databaseContext.Update(role);
            await _databaseContext.SaveChangesAsync();
        }
    }
}
