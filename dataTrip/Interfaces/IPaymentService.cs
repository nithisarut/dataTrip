using dataTrip.Models;

namespace dataTrip.Interfaces
{
    public interface IPaymentService : IService<Payment>
        
    {
        Task<(string errorMessage, string imageName)> UploadImage(IFormFileCollection formFiles);
    }
}
