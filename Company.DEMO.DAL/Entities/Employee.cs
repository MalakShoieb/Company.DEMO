using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DEMO.DAL.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public  string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        bool IsDeleted { get; set; }
        bool IsActive { get; set; }
        public  decimal Salary { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime CreatedAt { get; set; }


    }
}
