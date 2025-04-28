using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.ViewModel
{
    public class DepartmentVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Code Is Required")]
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        [Display(Name = "Date Of Creation")]
        public DateOnly CreationDate { get; set; }
    }
}
