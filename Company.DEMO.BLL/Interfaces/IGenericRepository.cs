using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.DEMO.BLL.Repository;
using Company.DEMO.DAL.Entities;

namespace Company.DEMO.BLL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
          public  Task<IEnumerable<TEntity>> GetAllAsync();

      Task<TEntity?> GetByIdAsync(int id);
         Task  AddAsync(TEntity MODEL);
        void Delete(TEntity MODEL);
      
      void Update(TEntity MODEL);
        Task<int> Complete();
    }
}
