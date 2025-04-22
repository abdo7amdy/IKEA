using IKEA.BLL.Dto_s.Departments;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Persistance.Repositories.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.DepartmentServices
{
    public class DepartmentServices : IDepartmentServices
    { // Controller => Services => Repositories => Contexts => Options

        // Repository Pattern

        
        private IDepartmentRepository repository;

        public DepartmentServices(IDepartmentRepository _repository)
        {
            repository = _repository;
        }
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var Departments = repository.GetAll().Where(D => D.IsDeleted == false).Select(dept => new DepartmentDto()
            {
                Id = dept.id,
                Name = dept.Name,
                Code = dept.Code,
                CreationDate = dept.CreationDate
                

            }).ToList();

            return Departments ;

            //List<DepartmentDto> departmentDtos = new List<DepartmentDto>();

            //foreach (var dept in Departments)
            //{
            //    DepartmentDto departmentDto = new DepartmentDto()
            //    {
            //        Id = dept.id ,
            //        Name = dept.Name ,
            //        Code = dept.Code ,
            //        CreationDate = dept.CreationDate
            //    };
            //    departmentDtos.Add(departmentDto);
            //}

        }

        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = repository.GetById(id);

            if (department is not null)
                return new DepartmentDetailsDto()
                {
                    id = department.id,
                    Name = department.Name,
                    Code = department.Code,
                    Description = department.Description,
                    CreationDate = department.CreationDate,
                    IsDeleted = department.IsDeleted,
                    LastModifiedBy = department.LastModifiedBy,
                    LastModifiedOn = department.LastModifiedOn,
                    CreatedBy = department.CreatedBy,
                    CreatedOn = department.CreatedOn,

                };

            return null;
            
        }
        public int CreateDepartment(CreatedDepartmentDto DepartmentDto)
        {
            var CreatedDepartment = new Department()
            {
                Code = DepartmentDto.Code,
                Name = DepartmentDto.Name,
                Description = DepartmentDto.Description,
                CreationDate = DepartmentDto.CreationDate,
                CreatedBy = 1,
                CreatedOn = DateTime.Now ,
                LastModifiedBy =1 ,
                LastModifiedOn = DateTime.Now 
            };
            return repository.Add(CreatedDepartment);
        }
        public int UpdateDepartment(UpdatedDepartmenDto DepartmentDto)
        {
            var UpdatedDepartment = new Department()
            {
                id = DepartmentDto.Id,
                Code = DepartmentDto.Code,
                Name = DepartmentDto.Name,
                Description = DepartmentDto.Description,
                CreationDate = DepartmentDto.CreationDate,
                LastModifiedBy = 1 ,
                LastModifiedOn = DateTime.Now
            };

            return repository.Update(UpdatedDepartment);
        }
        public bool DeleteDepartment(int id)
        {
            var deprtment = repository.GetById(id);

            if (deprtment != null)
                return repository.Delete(deprtment)>0;
            else
                return false;
        }    
        
        // Implementation Of Services 
    }
}
