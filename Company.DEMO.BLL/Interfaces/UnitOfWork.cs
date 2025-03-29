using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.DEMO.BLL.Repository;
using Company.DEMO.DAL.Data.Data;

namespace Company.DEMO.BLL.Interfaces
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompanyContext _context;
        public DepartmentRepository DepartmentRepository { get; }

        public EmployeeRepository EmployeeRepository { get; }
        public UnitOfWork( CompanyContext context)
        {
            _context = context;
            DepartmentRepository = new DepartmentRepository(_context);
            EmployeeRepository = new EmployeeRepository(_context);
            

            
        }
    }
}
