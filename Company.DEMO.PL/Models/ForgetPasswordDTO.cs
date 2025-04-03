using System.ComponentModel.DataAnnotations;

namespace Company.DEMO.PL.Models
{
    public class ForgetPasswordDTO
    {
        [Required(ErrorMessage = "Please enter the Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
