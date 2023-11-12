using dataTrip.Models;

namespace dataTrip.Interfaces
{
    public interface IAccountsService
    {
        Task<IEnumerable<Accounts>> GetAll();
        Task<Accounts> Login(string email, string password);
        Task<object> Register(Accounts accounts);

        Task<Accounts> GetByID(int id);

        Task UpdateAccount(Accounts accounts);

        string GenerateToken(Accounts account);
        Accounts GetInfo(string accessToken);

        Task<(string errorMessage, string imageName)> UploadImage(IFormFileCollection formFiles);
        Task DeleteImage(string fileName);
    }
}
