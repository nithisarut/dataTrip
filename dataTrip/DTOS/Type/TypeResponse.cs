using dataTrip.Models;

namespace dataTrip
{
    public class TypeResponse
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
      
        static public TypeResponse FromType(Types types)
        {
            return new TypeResponse
            {
                Id = types.Id,
                TypeName = types.TypeName,

            };
        }
    }
}
