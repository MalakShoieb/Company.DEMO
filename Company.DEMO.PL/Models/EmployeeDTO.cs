using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Company.DEMO.DAL.Entities;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Company.DEMO.PL.Models
{
    public class EmployeeDTO
    {
        [Required(ErrorMessage ="Enter the Name")]
        public string Name { get; set; }
        [DataType(DataType.EmailAddress , ErrorMessage ="Enter Valid Email")]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        [RegularExpression(@"^\d+\s[A-Za-z0-9\s.,'-]+$")]
        public string Address { get; set; }
        [Range(22, 60, ErrorMessage = "Please enter an age between 22 and 60.")]
  
        public int Age { get; set; }
        [DisplayName("Remove")]
        public bool IsDeleted { get; set; }
        [DisplayName("Active")]
        public bool IsActive { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [DisplayName("Hiring Date")]
        public DateTime StartAt { get; set; }
        [DisplayName("Date of Creation" )]
        public DateTime CreatedAt { get; set; }

        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public  IFormFile? Image { get; set; }
        public string? Imagenames { get; set; }
    }
}
