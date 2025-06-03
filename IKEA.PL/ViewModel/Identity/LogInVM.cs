using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.ViewModel.Identity
{
	public class LogInVM
	{
		[EmailAddress]
		public string Email { get; set; } = null!; 
		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;

		[Display(Name = "Remmember Me")]
		public bool RemmemberMe { get; set; }
	}
}
