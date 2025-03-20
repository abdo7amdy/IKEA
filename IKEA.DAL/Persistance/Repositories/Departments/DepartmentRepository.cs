using IKEA.DAL.Models.Departments;
using IKEA.DAL.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.Repositories.Departments
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext dbContext ;
        public DepartmentRepository(ApplicationDbContext context)
        {
            dbContext = context;
        }
        public IEnumerable<Department> GetAll(bool WithNoTracking = true)
        {
            if (WithNoTracking) 
                return dbContext.Departments.AsNoTracking().ToList();

            return dbContext.Departments.ToList();
        }
        public Department GetById(int id)
        {
            #region First Way to find department by id 
            //var department = dbContext.Departments.Local.FirstOrDefault(d => d.id == id);

            //if (department is null)
            //    department = dbContext.Departments.FirstOrDefault(d => d.id == id);

            //return department;
            #endregion
            
            // Another way "better"
            var department = dbContext.Departments.Find(id);

            return department;

        }
        public int Add(Department department)
        {
            dbContext.Departments.Add(department);
            return dbContext.SaveChanges();
        }
        public int Update(Department department)
        {
            dbContext.Departments.Update(department);
            return dbContext.SaveChanges();
        }
        public int Delete(Department department)
        {
            dbContext.Departments.Remove(department);
            return dbContext.SaveChanges();
        }     
    }
}
