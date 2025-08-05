using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.ViewModel
{
    public class ForgetPasswordVM
    {
        [EmailAddress(ErrorMessage ="Email is invalid !")]
        [Required(ErrorMessage ="Email is required ")]
        public string Email { get; set; }
    }
}
