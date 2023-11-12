using dataTrip.DTOS.Payment;
using dataTrip.DTOS.Report;
using dataTrip.Interfaces;
using dataTrip.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace dataTrip.Services
{
    public class ReportService : IReportService
    {
        private readonly DatabaseContext _db;

        public ReportService(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<TripStatisticeDTO> TripStatisticeItem(DateTime? dateStart, DateTime? dateEnd)
        {
            TripStatisticeDTO TripStatistice = new();
            var orders = new List<OrderTrip>();
            if (dateStart == null || dateEnd == null)
            {
                orders = await _db.OrderTrips.Where(e => e.Created.Year == DateTime.Now.Year && e.Status == OrderStatus.SuccessfulPayment).ToListAsync();
            }
            else
            {
                orders = await _db.OrderTrips.Where(e => e.Created >= dateStart && e.Created <= dateEnd && e.Status == OrderStatus.SuccessfulPayment).ToListAsync();
            }
            
            //SalesStatistics.TotalPrice = orders.Sum(x => x.Subtotal);
            foreach (var order in orders)
            {
                var result = TripStatistice.Trip.Find(x => x.Month == order.Created.Month && x.Year == order.Created.Year);
                if (result == null)
                {
                    TripStatistice.Trip.Add(new TripStatisticeItemDTO
                    {
                        price = order.Total,
                        FullTime = order.Created,
                        Day = order.Created.Day,
                        Month = order.Created.Month,
                        Year = order.Created.Year
                    });
                }
                else result.price += order.Total;
            }

            TripStatistice.TotalPrice = TripStatistice.Trip.Sum(e => e.price);

            foreach (var item in TripStatistice.Trip)
            {
                var Percen = item.price * 100 / TripStatistice.TotalPrice;
                item.percent = Percen;
            }
            TripStatistice.Trip = TripStatistice.Trip.OrderBy(e => e.Month).ToList();


            return TripStatistice;
        }
    }
}
