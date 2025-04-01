using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Company.DEMO.DAL.Data.Configuration;
using Company.DEMO.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Company.DEMO.DAL.Data.Data
{
    public class CompanyContext:IdentityDbContext<AppUser>
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
        public DbSet<Employee> Employees { get; set; }
    }

}

