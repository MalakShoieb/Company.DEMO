using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.DEMO.BLL.Interfaces;
using Company.DEMO.DAL.Data.Data;

namespace Company.DEMO.BLL.Repository
{
    public class GenericRepository <T>: IGenericRepository<T> where T : BaseEntity
    {
        private readonly CompanyContext _context;

        public GenericRepository(CompanyContext context)
        {
            _context = context;
        }
        public int Add(T MODEL)
        {
            _context.Add(MODEL);
            return _context.SaveChanges();
        }

     

        public int Delete(T MODEL)
        {
            _context.Set<T>().Remove(MODEL);
            return _context.SaveChanges();

        }

       

        public IEnumerable<T> GetAll()
        {
           return _context.Set<T>().ToList();

        }





        public int Update(T MODEL)
        {
            _context.Set<T>().Update(MODEL);
            return _context.SaveChanges();
        }

       

        T? IGenericRepository<T>.GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
    }
}
