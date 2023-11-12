using System.ComponentModel.DataAnnotations;

namespace dataTrip.DTOS.Login
{
    public class RegisterRequest
    {

        [Required]
        public string FullName { get; set; }
        [Required]
        //[EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        //[DataType(DataType.PhoneNumber)]
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
      
        public IFormFileCollection? FormFiles { get; set; }
        public int RoleID { get; set; }
    }
}
