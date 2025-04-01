using System.ComponentModel.DataAnnotations;

namespace Company.DEMO.PL.Models
{
    public class SignInDTO
    {
        [Required(ErrorMessage = "Please enter the Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter the Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
