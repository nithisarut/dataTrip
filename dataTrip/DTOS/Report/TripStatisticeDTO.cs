namespace dataTrip.DTOS.Report
{
    public class TripStatisticeDTO
    {
        public double TotalPrice { get; set; }
        public List<TripStatisticeItemDTO> Trip { get; set; } = new List<TripStatisticeItemDTO>();
    }
}
