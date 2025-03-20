using IKEA.DAL.Persistance.Repositories.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.DepartmentServices
{
    internal class DepartmentServices : IDepartmentServices
    {
        // Repository Pattern
        // Implementation Of Services 
        private IDepartmentRepository repository;
        public DepartmentServices(IDepartmentRepository _repository)
        {
            repository = _repository;
        }
    }
}
