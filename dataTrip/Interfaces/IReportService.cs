using dataTrip.DTOS.Report;

namespace dataTrip.Interfaces
{
    public interface IReportService
    {
        Task<TripStatisticeDTO> TripStatisticeItem( DateTime? dateStart , DateTime? dateEnd);

     
    }
}
