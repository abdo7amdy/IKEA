using IKEA.DAL.Models.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Data.Configurations.DepartmentConfigurations
{
    internal class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(d=>d.id).UseIdentityColumn(10,10);
            builder.Property(d => d.Name).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(d => d.Code).HasColumnType("nvarchar(20)").IsRequired();

            // Development Usage 
            builder.Property(d => d.CreatedOn).HasDefaultValueSql("GetDate()");
            builder.Property(d => d.LastModifiedOn).HasComputedColumnSql("GetDate()");

        }
    }
}
