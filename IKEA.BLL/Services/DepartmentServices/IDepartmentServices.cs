using IKEA.BLL.Dto_s.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.DepartmentServices
{
    public interface IDepartmentServices
    {
        //Services Signature
        // DTO => Data Transfer Object 
        IEnumerable<DepartmentDto> GetAllDepartments ();

        DepartmentDetailsDto? GetDepartmentById (int id);

        int CreateDepartment(CreatedDepartmentDto DepartmentDto);
        int UpdateDepartment(UpdatedDepartmenDto DepartmentDto);
        bool DeleteDepartment(int id);


    }
}
