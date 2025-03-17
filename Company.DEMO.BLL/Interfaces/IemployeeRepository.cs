﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.DEMO.DAL.Entities;

namespace Company.DEMO.BLL.Interfaces
{
   public  interface   IemployeeRepository:IGenericRepository<Employee>
    {
        public List<Employee>GetByName(string? name);


        //Employee? GetById(int id);
        //int Add(Employee employee);
        //int Delete(Employee employee);
        //int Update(Employee employee);
    }
}
