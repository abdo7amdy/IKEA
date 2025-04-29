using IKEA.DAL.Common.Enums;
using IKEA.DAL.Models.Departments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Models.Employees
{
	public class Employee:ModelBase
	{
		[Required(ErrorMessage ="Name Is Required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Age Is Required")]
        public int? Age { get; set; }
        [Required(ErrorMessage = "Address Is Required")]
        public string? Address { get; set; }
		public decimal Salary { get; set; }
		public bool IsActive { get; set; }
        [Required(ErrorMessage = "Email Is Required")]
        public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
		public DateOnly HiringDate { get; set; }
		public Gender Gender { get; set; }
		public EmpolyeeType EmpolyeeType { get; set; }
		public int? DepartmentId { get; set; }
		// Navigational Property [One]
		public virtual Department? Department { get; set; }
        public string? ImageName { get; set; }

    }
}
