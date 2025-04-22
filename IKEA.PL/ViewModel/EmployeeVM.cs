using IKEA.DAL.Common.Enums;

namespace IKEA.PL.ViewModel
{
    public class EmployeeVM
    {
        public int id { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmpolyeeType EmpolyeeType { get; set; }
        public int? DepartmenId { get; set; }

    }
}
