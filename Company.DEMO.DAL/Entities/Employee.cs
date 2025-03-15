using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.DEMO.BLL.Repository;

namespace Company.DEMO.DAL.Entities
{
    public class Employee:BaseEntity
    {
        public int Id { get; set; }
        public  string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public  decimal Salary { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime CreatedAt { get; set; }


    }
}
