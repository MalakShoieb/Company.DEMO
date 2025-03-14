using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.DEMO.BLL.Repository;
using Company.DEMO.DAL.Entities;

namespace Company.DEMO.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();

        T? GetById(int id);
        int Add(T MODEL);
        int Delete(T MODEL);
      
        int Update(T MODEL);

    }
}
