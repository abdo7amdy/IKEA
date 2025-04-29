using IKEA.DAL.Persistance.Data;
using IKEA.DAL.Persistance.Repositories.Departments;
using IKEA.DAL.Persistance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext applicationDbContext;
        public IDepartmentRepository DepartmentRepository { get ;  }
        public IEmployeeRepository EmployeeRepository { get ;  }

        public UnitOfWork(ApplicationDbContext dbContext)//ask CLR to generate object fron Context
        {
            this.applicationDbContext = dbContext;
            DepartmentRepository = new DepartmentRepository(applicationDbContext);
            EmployeeRepository = new EmployeeRepository(applicationDbContext);
        }
        public async Task<int> Complete()
        {
            return await applicationDbContext.SaveChangesAsync();
        }
    }
}
