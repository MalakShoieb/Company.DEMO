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
   public class DepartmentRepository :  GenericRepository<Department>,IDepartmentRepository
    {
        public DepartmentRepository(CompanyContext context):base(context) 
        {
            
        }
        #region Before Generalization
        //private readonly CompanyContext _context; //nulll


        //public DepartmentRepository(CompanyContext company)
        //{
        //    _context = company; ///4 steps to intailize object 
        //}
        //public int Add(Department department)
        //{

        //_context.Department.Add(department);
        //    return _context.SaveChanges();
        //}

        //public int Delete(Department department)
        //{

        //    _context.Department.Remove(department);
        //    return _context.SaveChanges();

        //}

        //public IEnumerable<Department> GetAll()
        //{

        //    return _context.Department.ToList();
        //}

        //public Department? GetById(int id)
        //{

        //   return _context.Department.Find(id);
        //}

        //public int Update(Department department)
        //{

        //   _context.Department.Update(department);
        //    return _context.SaveChanges();
        //} 
        #endregion
    }
}
