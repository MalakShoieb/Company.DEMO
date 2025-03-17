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
      public  IEnumerable<TEntity> GetAll();

       public TEntity? GetById(int id);
        public int Add(TEntity MODEL);
        public int Delete(TEntity MODEL);
      
        public int Update(TEntity MODEL);

    }
}
