using IKEA.DAL.Persistance.Repositories.Departments;
using IKEA.DAL.Persistance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Persistance.UnitOfWork
{
    public interface IUnitOfWork//:IDisposable
    {
        public IDepartmentRepository DepartmentRepository { get; }
        public IEmployeeRepository EmployeeRepository {  get; }

        public Task<int> Complete();
    }
}
