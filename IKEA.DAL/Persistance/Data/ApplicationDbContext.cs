using IKEA.DAL.Models.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Dependency Injection 
        // Department = Depend on > Context = Depend on > Options
        // Ask from CLR to generate Options for my context
        public ApplicationDbContext(DbContextOptions options):base(options)
        {
            
        }

        #region Old Way 2 Configure Contexts which is not good and not secure
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=DESKTOP-A1088VT\\ABDOHAMDY;Database=IKEA;Trusted_Connection=true;TrustServerCertificate=true");
        //} 
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Department> Departments { get; set; }
    }
}
