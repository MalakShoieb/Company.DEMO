using System.ComponentModel.DataAnnotations;

namespace Company.DEMO.PL.Models
{
    public class ResetDTO
    {
        [Required(ErrorMessage = "Please enter the Password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Please enter the ConfirmedPassword")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Password and ConfirmedPassword do not match")]
        public string ConfirmedPassword { get; set; }

    }
}
