using dataTrip.Models;

namespace dataTrip.DTOS.Role
{
    public class RoleResponse
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        static public RoleResponse FromRole(Models.Role role)
        {
            return new RoleResponse
            {
                Id = role.Id,
                RoleName = role.RoleName,

            };
        }
    }
}
