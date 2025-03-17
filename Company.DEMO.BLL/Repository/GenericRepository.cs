using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Company.DEMO.BLL.Interfaces;
using Company.DEMO.DAL.Data.Data;
using Company.DEMO.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Company.DEMO.BLL.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly CompanyContext _context;

        public GenericRepository(CompanyContext context)
        {
            _context = context;
        }


        public int Add(TEntity MODEL)
        {
            _context.Set<TEntity>().Add(MODEL);
            return _context.SaveChanges();
        }



        public int Delete(TEntity MODEL)
        {
            _context.Set<TEntity>().Remove(MODEL);
            return _context.SaveChanges();

        }


        public TEntity? GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }



        public int Update(TEntity MODEL)
        {
            {
                _context.Set<TEntity>().Update(MODEL);
                return _context.SaveChanges();
            }


        }

        IEnumerable<TEntity> IGenericRepository<TEntity>.GetAll()
            
        { 
            if (typeof(TEntity) == typeof(Employee))
            {
               
                  return  (IEnumerable<TEntity>)_context.Employees.Include(e=>e.Department).ToList();
            }
            return _context.Set<TEntity>().ToList();
        }
    }
}
