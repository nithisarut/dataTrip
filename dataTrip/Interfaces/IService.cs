namespace dataTrip.Interfaces
{
    public interface IService<T> where T : class
    {
        Task<ICollection<T>> GetAllAsync();
        Task<T> GetAsync(int id, bool tracked = true);
        Task CreactAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
    }
} 
