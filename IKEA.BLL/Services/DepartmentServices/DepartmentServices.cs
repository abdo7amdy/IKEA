using IKEA.BLL.Dto_s.Departments;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Persistance.Repositories.Departments;
using IKEA.DAL.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
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

        
        private readonly IUnitOfWork unitOfWork;

        public DepartmentServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<DepartmentDto>> GetAllDepartments()
        {
            var Departments = unitOfWork.DepartmentRepository.GetAll()
                                        .Where(D => D.IsDeleted == false)
                                        .Select(dept => new DepartmentDto()
            {
                Id = dept.id,
                Name = dept.Name,
                Code = dept.Code,
                CreationDate = dept.CreationDate
                

            }).ToListAsync();

            return await Departments ;

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

        public async Task<DepartmentDetailsDto>? GetDepartmentById(int id)
        {
            var department =await unitOfWork.DepartmentRepository.GetById(id);

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
        public async Task<int> CreateDepartment(CreatedDepartmentDto DepartmentDto)
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
            unitOfWork.DepartmentRepository.Add(CreatedDepartment);
            return await unitOfWork.Complete();
        }
        public async Task<int> UpdateDepartment(UpdatedDepartmenDto DepartmentDto)
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

            unitOfWork.DepartmentRepository.Update(UpdatedDepartment);
            return await unitOfWork.Complete();
        }
        public async Task<bool> DeleteDepartment(int id)
        {
            var deprtment =await unitOfWork.DepartmentRepository.GetById(id);

            if (deprtment != null)
                unitOfWork.DepartmentRepository.Delete(deprtment);
             
            if (await unitOfWork.Complete() > 0)
                return true;
            else
                return false;
        }    
        
        // Implementation Of Services 
    }
}
