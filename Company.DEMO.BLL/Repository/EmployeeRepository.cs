using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.DEMO.BLL.Interfaces;
using Company.DEMO.DAL.Data.Data;
using Company.DEMO.DAL.Entities;

namespace Company.DEMO.BLL.Repository
{
    internal class EmployeeRepository : GenericRepository<Employee>, IemployeeRepository
    {
        public EmployeeRepository(CompanyContext context):base(context) 
        {
            
        }




    }
}
#region before generiz
    //{ private  readonly CompanyContext _context;
    //    public EmployeeRepository(CompanyContext companyContext)
    //    {
    //      _context = companyContext;
    //    }



    //    public int Add(Employee employee)
    //    { _context.Employees.Add(employee);
    //        return _context.SaveChanges();

    //    }

    //    public int Delete(Employee employee)
    //    {
    //     _context.Employees.Remove(employee);
    //        return _context.SaveChanges();
    //    }

    //    public IEnumerable<Employee> GetAll()
    //    {
    //        return _context.Employees.ToList();
    //    }

    //    public Employee? GetById(int id)
    //    {
    //        return _context.Employees.Find(id);
    //    }

    //    public int Update(Employee employee)
    //    {
    //      _context.Employees.Update(employee);
    //        return _context.SaveChanges(); 
    #endregion
  

