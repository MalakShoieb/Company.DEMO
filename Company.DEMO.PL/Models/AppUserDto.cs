using System.ComponentModel.DataAnnotations;
using Company.DEMO.DAL.Data.Configuration;

namespace Company.DEMO.PL.Models
{
    public class AppUserDto
    {
        [Required(ErrorMessage ="Please enter the UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter the FirstName")]
        public  string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter the LastName")]
        public string LastName { get; set; }
        public bool IsAgree { get; set; }
        [Required(ErrorMessage = "Please enter the Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter the Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter the ConfirmedPassword")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and ConfirmedPassword do not match")]
        public string ConfirmedPassword { get; set; }


    }
}
