using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Company.DEMO.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Company.DEMO.DAL.Data.Data
{
    public class CompanyContext:DbContext
    {
        public CompanyContext(DbContextOptions<CompanyContext>options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.; Database= Company; Trusted_Connection=True; TrustServerCertificate=True;");
        //}
         public DbSet<Department> Department { get; set; }
    }

}

