using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.DEMO.BLL.Interfaces;
using Company.DEMO.DAL.Data.Data;
using Company.DEMO.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Company.DEMO.BLL.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IemployeeRepository
    {
        private readonly CompanyContext _context;

        public EmployeeRepository(CompanyContext context):base(context) 
        {
            _context = context;
        }
        //public List<Employee>GetByName(string? name)
        //{
        //    return _context.Employees.Where(W=>W.Name.ToLower().Contains(name.ToLower())).ToList();
        //}

        public async Task<List<Employee>> GetByNameAsync(string? name)
        {
            return await _context.Employees.Where(W => W.Name.ToLower().Contains(name.ToLower())).ToListAsync();
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
  

