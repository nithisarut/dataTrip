using dataTrip.Interfaces;
using dataTrip.Models;
using Microsoft.EntityFrameworkCore;

namespace dataTrip.Services
{
    public class OrderTripService : IOrderTripService
    {
        private readonly DatabaseContext db;
        public OrderTripService(DatabaseContext db)
        {
            this.db = db;
        }

        public async Task CreactAsync(OrderTrip orderTrip)
        {
            await db.AddAsync(orderTrip);
            await db.SaveChangesAsync();
        }

        public async Task<ICollection<OrderTrip>> GetAllAsync()
        {
            return await db.OrderTrips.ToListAsync();
        }

        public async Task<OrderTrip> GetAsync(int id, bool tracked = true)
        {
            IQueryable<OrderTrip> query = db.OrderTrips;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            return await query.Include(e => e.Trip).FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task<IEnumerable<OrderTrip>> GetByIdOrderAccountAsync(int id)
        {
            return await db.OrderTrips.Include(e => e.Trip).Where(e => e.AccountsId == id).ToListAsync();
        }

        public async Task RemoveAsync(OrderTrip orderTrip)
        {
            db.Remove(orderTrip);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(OrderTrip orderTrip)
        {
            db.OrderTrips.Update(orderTrip);
            await db.SaveChangesAsync();
        }
    }
}
