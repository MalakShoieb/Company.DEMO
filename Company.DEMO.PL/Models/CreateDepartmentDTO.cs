using System.ComponentModel.DataAnnotations;

namespace Company.DEMO.PL.Models
{
    public class CreateDepartmentDTO
    {
        [Required(ErrorMessage =" you must enter code")]
        public string Code { get; set; }
        [Required(ErrorMessage = " you must enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = " you must enter CreateAT")]
        public DateTime CreateAt { get; set; }
    }
}
