using System.ComponentModel.DataAnnotations;

namespace Wipro_OnlineMovieBookingApplication.DTO
{
    public class UserLoginDTO
    {
        public Int64 Id { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is Required")]
        public string? Password { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is Required")]
        public string? Email { get; set; }
        public int? IsAdmin { get; set; }
    }
}
