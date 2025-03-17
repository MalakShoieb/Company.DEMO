using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.DEMO.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.DEMO.DAL.Data.Configuration
{
    internal class EmployeeConf : IEntityTypeConfiguration<Employee>

    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Id).UseIdentityColumn(100, 1);
             builder.Property(e => e.Salary)
              .HasPrecision(18, 2);
            builder.HasOne(e=>e.Department)
                .WithMany(e=>e.Employees)
                .HasForeignKey(e=>e.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
