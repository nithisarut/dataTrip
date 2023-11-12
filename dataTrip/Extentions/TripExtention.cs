using dataTrip.Models;
using dataTrip.Setting;
using Microsoft.EntityFrameworkCore;

namespace dataTrip.Extentions
{
    public static class TripExtention
    {
        public static IQueryable<TripDto> ProjectProductToProductDTO(this IQueryable<Trip> query, DatabaseContext db)
        {
            return query
                .Select(trip => new TripDto
                {
                    Id = trip.Id,
                    TripName = trip.TripName,
                    Detail = trip.Detail,
                    ClassTrip = trip.ClassTrip,
                    Amount = trip.Amount,
                    Price = trip.Price,
                    DateTimeStart = trip.DateTimeStart,
                    File = !string.IsNullOrEmpty(trip.File) ? $"{UrlServer.Url}/images/{trip.File}" :"",
                    DateTimeEnd = trip.DateTimeEnd,
                    ImageTrip = !string.IsNullOrEmpty(trip.ImageTrip) ? $"{UrlServer.Url}/images/{trip.ImageTrip}" : "",
                    Vehicle = FromVehicle(trip.Vehicle),
                    addMultipleLocations = db.AddMultipleLocations.Include(e => e.Location).Where(e => e.TripID.Equals(trip.Id)).Select(data => FromAddMultipleLocation(data)).ToList()
                }).AsNoTracking();
        }
        private static AddMultipleLocations FromAddMultipleLocation(AddMultipleLocations addMultipleLocations)
        {
            return new AddMultipleLocations
            {
                Id = addMultipleLocations.Id,
                TripID = addMultipleLocations.TripID,
                LocationID = addMultipleLocations.LocationID,
                Location = new Location
                {
                    Id = addMultipleLocations.Location.Id,
                    LocationName = addMultipleLocations.Location.LocationName,
                    Details = addMultipleLocations.Location.Details,
                    District = addMultipleLocations.Location.District,
                    SubDistrict = addMultipleLocations.Location.SubDistrict,
                    TypeID = addMultipleLocations.Location.TypeID,
             
                    Image = !string.IsNullOrEmpty(addMultipleLocations.Location.Image) ? $"{UrlServer.Url}/images/{addMultipleLocations.Location.Image}" : "",
                },

            };

        }
        private static Vehicle FromVehicle(Vehicle vehicle)
        {
            return new Vehicle
            {
                Id = vehicle.Id,
                Company = vehicle.Company,
                DriverName = vehicle.DriverName,
                Tel = vehicle.Tel,
                NumberOFSeats = vehicle.NumberOFSeats,
                VehicleName = vehicle.VehicleName,
                status = vehicle.status,
                VehicleRegistration = vehicle.VehicleRegistration,
                ImageDriver = !string.IsNullOrEmpty(vehicle.ImageDriver) ? $"{UrlServer.Url}/images/{vehicle.ImageDriver}" : "",
                ImageVehicle = !string.IsNullOrEmpty(vehicle.ImageVehicle) ? $"{UrlServer.Url}/images/{vehicle.ImageVehicle}" : "",


            };


        }

        public static IQueryable<Trip> Filter(this IQueryable<Trip> query, int classTripId)
        {
            if (classTripId == 0) return query;
            query = query.Where(p => p.ClassTripId == classTripId);
            return query;
        }

    }
}
