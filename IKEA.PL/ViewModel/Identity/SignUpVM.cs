using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.ViewModel.Identity
{
	public class SignUpVM
	{
		[Display(Name = "Frist Name")]
		public string FristName { get; set; } = null!;

		[Display(Name = "Last Name")]
		public string LastName { get; set; } = null!;

		[Display(Name = "User Name")]
		public string UserName { get; set; } = null!;

		[EmailAddress]
		public string Email { get; set; } = null!;

		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;

		[DataType(DataType.Password)]
		[Display(Name = "Confirm Password")]
		[Compare("Password",ErrorMessage ="Confirmed password does not match with Password ..!")]
		public string PasswordConfirmed { get; set; } = null!;

		[Display(Name = "Is Agree")]
		public bool IsAgree { get; set; } 

    }
}
