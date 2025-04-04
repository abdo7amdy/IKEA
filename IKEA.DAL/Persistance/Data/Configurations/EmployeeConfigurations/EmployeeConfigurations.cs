using IKEA.DAL.Common.Enums;
using IKEA.DAL.Models.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Data.Configurations.EmployeeConfigurations
{
	public class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
	{
		public void Configure(EntityTypeBuilder<Employee> builder)
		{
			builder.Property(E => E.Name).HasColumnType("nvarchar(50)").IsRequired();
			builder.Property(E => E.Address).HasColumnType("nvarchar(100)");
			builder.Property(E => E.Salary).HasColumnType("decimal(8,2)");
			builder.Property(E => E.Gender).HasConversion
				(
					(gender) => gender.ToString(),
					(gender) => (Gender)Enum.Parse(typeof(Gender), gender)
				); 
			
			builder.Property(E => E.EmpolyeeType).HasConversion
				(
					(empolyeeType) => empolyeeType.ToString(),
					(empolyeeType) => (EmpolyeeType)Enum.Parse(typeof(EmpolyeeType), empolyeeType)
				);
			builder.Property(E => E.CreatedOn).HasDefaultValueSql("GetDate()");
			builder.Property(E => E.LastModifiedOn).HasComputedColumnSql("GetDate()");


		}
	}
}
