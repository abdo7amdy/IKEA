using IKEA.BLL.Dto_s.Employees;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Persistance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.EmployeeServices
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IEmployeeRepository Repository;

        public EmployeeServices(IEmployeeRepository employeeRepository)
        {
            Repository = employeeRepository;
        }

        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            var Employees = Repository.GetAll();

            var FilteredEmployees = Employees.Where(E => E.IsDeleted == false);
            var AfterFilteration = FilteredEmployees.Select(E => new EmployeeDto()
            {
                Id = E.id,
                Name = E.Name , 
                Age = E.Age ,
                Salary = E.Salary,
                IsActive = E.IsActive ,
                Email = E.Email ,
                Gender = E.Gender ,
                EmployeeType = E.EmpolyeeType

            });
            return AfterFilteration.ToList();
        }
        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = Repository.GetById(id);
            if (employee != null)
            {
                return new EmployeeDetailsDto()
                {
                    id = employee.id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Salary = employee.Salary,
                    IsActive = employee.IsActive,
                    Email = employee.Email ,
                    PhoneNumber = employee.PhoneNumber ,
                    HiringDate = employee.HiringDate ,
                    Gender = employee.Gender ,
                    EmpolyeeType = employee.EmpolyeeType ,
                    LastModifiedBy = employee.LastModifiedBy,
                    LastModifiedOn = employee.LastModifiedOn,
                    Address = employee.Address ,
                    CreatedBy = employee.CreatedBy ,
                    CreatedOn = employee.CreatedOn
                };
            }
            return null;

        }
        public int CreateEmployee(CreatedEmployeeDto EmployeeDto)
        {
            var employee = new Employee()
            {
                Name = EmployeeDto.Name,
                Age = EmployeeDto.Age,
                Salary = EmployeeDto.Salary,
                IsActive = EmployeeDto.IsActive,
                Email = EmployeeDto.Email,
                PhoneNumber = EmployeeDto.PhoneNumber,
                HiringDate = EmployeeDto.HiringDate,
                Address = EmployeeDto.Address,
                Gender = EmployeeDto.Gender,
                EmpolyeeType = EmployeeDto.EmpolyeeType,
                CreatedBy = 1,
                LastModifiedBy = 1,
                CreatedOn = DateTime.Now,
                LastModifiedOn = DateTime.Now,                
            };
            return Repository.Add(employee);
        }
        public int UpdateEmployee(UpdatedEmployeeDto EmployeeDto)
        {
            var employee = new Employee()
            {
                id = EmployeeDto.id ,
                Name = EmployeeDto.Name,
                Age = EmployeeDto.Age,
                Salary = EmployeeDto.Salary,
                IsActive = EmployeeDto.IsActive,
                Email = EmployeeDto.Email,
                PhoneNumber = EmployeeDto.PhoneNumber,
                HiringDate = EmployeeDto.HiringDate,
                Address = EmployeeDto.Address,
                Gender = EmployeeDto.Gender,
                EmpolyeeType = EmployeeDto.EmpolyeeType,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,
            };
            return Repository.Update(employee);
        } 
        public bool DeleteEmployee(int id)
        {
            var employee = Repository.GetById(id);

            if (employee != null)
                return Repository.Delete(employee) > 0;
            else
                return false;
        }
        
    }
}
