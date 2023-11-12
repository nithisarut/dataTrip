using dataTrip.DTOS.Payment;
using dataTrip.Interfaces;
using dataTrip.Models;
using Microsoft.EntityFrameworkCore;

namespace dataTrip.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly DatabaseContext db;
        private readonly IUploadFileService _uploadFileService;
        public PaymentService(DatabaseContext db, IUploadFileService uploadFileService)
        {
            this.db = db;
            _uploadFileService = uploadFileService;
        }
        public async Task CreactAsync(Payment payment)
        {
            payment.date = DateTime.Now;
            await db.AddAsync(payment);
            await db.SaveChangesAsync();
        }

        public async Task<ICollection<Payment>> GetAllAsync()
        {
            return await db.Payments.Include(e => e.OrderTrips).ToListAsync();
        }

        public async Task<Payment> GetAsync(int id, bool tracked = true)
        {
            IQueryable<Payment> query = db.Payments;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task RemoveAsync(Payment payment)
        {
            db.Remove(payment);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Payment payment)
        {
        
            db.Payments.Update(payment);
            await db.SaveChangesAsync();

        
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
    }
}
