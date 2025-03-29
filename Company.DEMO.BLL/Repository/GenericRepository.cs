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


        public async Task AddAsync(TEntity MODEL)
        {
            await _context.Set<TEntity>().AddAsync(MODEL);
            
        }

     

        public  async Task<int> Complete()
        {
            return  await _context.SaveChangesAsync();
        }

        public void Delete(TEntity MODEL)
        {
            _context.Set<TEntity>().Remove(MODEL);
           
        }

        public async Task< IEnumerable<TEntity>> GetAllAsync()
        {
            if (_context == null)
            {
                throw new InvalidOperationException("Database context is not initialized.");
            }

            if (typeof(TEntity) == typeof(Employee))
            {
                return await _context.Employees.Include(e => e.Department).Cast<TEntity>().ToListAsync();
            }

            return  await _context.Set<TEntity>().ToListAsync();
        }

        //public IEnumerable<TEntity> GetAll()
        //{
        //    //if (typeof(TEntity) == typeof(Employee))
        //    //{
        //    //    return (IEnumerable<TEntity>)_context.Employees.Include(e => e.Department).ToList();
        //    //}
        //    return _context.Set<TEntity>().ToList();
        //}




        //public IEnumerable<TEntity> GetAll()
        //{
        //    if (_context == null) // Defensive check
        //    {
        //        throw new InvalidOperationException("Database context is not initialized.");
        //    }

        //    if (typeof(TEntity) == typeof(Employee))
        //    {

        //        return (IEnumerable<TEntity>)_context.Employees.Include(e => e.Department).ToList();
        //    }
        //    return _context.Set<TEntity>().ToList();

        //}

        public  async Task<TEntity?> GetByIdAsync(int id)
        {if(typeof(TEntity) == typeof(Employee))
            {
                return  await _context.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.Id == id) as TEntity;
            }


            return await  _context.Set<TEntity>().FindAsync(id);
        }



        public void Update(TEntity MODEL)
        {
            {
                 _context.Set<TEntity>().Update(MODEL);
             
            }


        }

       



  
    }
}
