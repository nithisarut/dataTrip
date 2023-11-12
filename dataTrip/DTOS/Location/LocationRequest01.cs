namespace dataTrip.DTOS.Location
{
    public class LocationRequest01

    {
        public string LocationName { get; set; }
        public string Details { get; set; }
        public string District { get; set; }
        public string SubDistrict { get; set; }
        public IFormFileCollection Image { get; set; }
        public int TypeID { get; set; }
    }
}
                