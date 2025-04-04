using IKEA.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Dto_s.Employees
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string? Name { get; set; } = null!;
        public int? Age { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string Gender { get; set; } = null!;
        public string EmpolyeeType { get; set; } = null!;

    }
}
