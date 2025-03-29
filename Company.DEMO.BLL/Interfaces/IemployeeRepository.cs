using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.DEMO.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Company.DEMO.BLL.Interfaces
{
   public  interface   IemployeeRepository:IGenericRepository<Employee>
    {
        public   Task<List<Employee>>GetByNameAsync(string? name);
      


        //Employee? GetById(int id);
        //int Add(Employee employee);
        //int Delete(Employee employee);
        //int Update(Employee employee);
    }
}
