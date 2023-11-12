using dataTrip.Models;
using dataTrip.Setting;

namespace dataTrip
{
    public class ImagesResponse
    {
        public int Id { get; set; }
        public string ImageSum { get; set; }

        public int LocationId { get; set; }

        static public ImagesResponse FromImage(Images images)
        {
            return new ImagesResponse
            {
                Id = images.Id,
                ImageSum = !string.IsNullOrEmpty(images.ImageSum) ? UrlServer.Url + "images/" + images.ImageSum : "",
             LocationId = images.LocationId,

            };
        }
    }
}
