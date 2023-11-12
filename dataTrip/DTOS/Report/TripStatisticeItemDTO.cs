﻿namespace dataTrip.DTOS.Report
{
    public class TripStatisticeItemDTO
    {
        public double percent { get; set; }
        public double price { get; set; }
        public DateTime? FullTime { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
