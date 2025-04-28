using AutoMapper;
using IKEA.BLL.Dto_s.Departments;
using IKEA.PL.ViewModel;

namespace IKEA.PL.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<DepartmentVM, CreatedDepartmentDto>().ReverseMap();
            CreateMap<DepartmentDetailsDto, DepartmentVM > ().ReverseMap();
            CreateMap<DepartmentVM, UpdatedDepartmenDto>().ReverseMap();

        }
    }
}
