using System.ComponentModel.DataAnnotations;

namespace dataTrip.DTOS.Accounts
{
    public class AccountsRequest
    {
        [Required]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Image { get; set; }
        public int? RoleID { get; set; }
        public IFormFileCollection? FormFiles { get; set; }
    }
}
