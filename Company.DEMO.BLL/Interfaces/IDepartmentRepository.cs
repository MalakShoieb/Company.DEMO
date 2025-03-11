using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.DEMO.DAL.Entities;

namespace Company.DEMO.BLL.Interfaces
{
    public interface IDepartmentRepository
    {IEnumerable<Department> GetAll();


        Department? GetById(int id);
        int Add(Department department);
        int Delete (Department department);
        int Update(Department department);

    }
}
