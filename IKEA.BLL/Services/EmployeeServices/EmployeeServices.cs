using IKEA.BLL.Dto_s.Employees;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Persistance.Repositories.Employees;
using IKEA.DAL.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.EmployeeServices
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IUnitOfWork unitOfWork;
        public EmployeeServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<EmployeeDto> GetAllEmployees(string search)
        {
            var Employees = unitOfWork.EmployeeRepository.GetAll();

            var FilteredEmployees = Employees.Where(E => !E.IsDeleted  && (string.IsNullOrEmpty(search) || E.Name.ToLower().Contains(search.ToLower()) ));
            var AfterFilteration = FilteredEmployees.Include(E=>E.Department).Select(E => new EmployeeDto()
            {
                Id = E.id,
                Name = E.Name , 
                Age = E.Age ,
                Salary = E.Salary,
                IsActive = E.IsActive ,
                Email = E.Email ,
                Gender = E.Gender ,
                EmployeeType = E.EmpolyeeType,
                Department = E.Department.Name ?? "N/A"

            });
            return AfterFilteration.ToList();
        }
        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = unitOfWork.EmployeeRepository.GetById(id);
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
                DepartmentId = EmployeeDto.DepartmenId,
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
            unitOfWork.EmployeeRepository.Add(employee);
            return unitOfWork.Complete();
        }
        public int UpdateEmployee(UpdatedEmployeeDto EmployeeDto)
        {
            var employee = new Employee()
            {
                DepartmentId = EmployeeDto.DepartmenId,
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
            unitOfWork.EmployeeRepository.Update(employee);
            return unitOfWork.Complete();
        } 
        public bool DeleteEmployee(int id)
        {
            var employee = unitOfWork.EmployeeRepository.GetById(id);

            if (employee != null)
                unitOfWork.EmployeeRepository.Delete(employee) ;
            var result = unitOfWork.Complete();
            if (result > 0)
                return true;
            else
                return false;
        }
        
    }
}
