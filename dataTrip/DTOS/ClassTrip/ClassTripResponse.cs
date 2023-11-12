

using dataTrip.Models;

namespace dataTrip
{
    public class ClassTripResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        static public ClassTripResponse FromClassTrip(ClassTrip classTrip)
        {
            return new ClassTripResponse
            {
                Id = classTrip.Id,
               Name = classTrip.Name,

            };
        }
    }
}
