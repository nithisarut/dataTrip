using dataTrip.Models;

namespace dataTrip.Interfaces
{
    public interface IOrderTripService : IService<OrderTrip>
    {
        Task<IEnumerable<OrderTrip>> GetByIdOrderAccountAsync(int id);

    }
}
