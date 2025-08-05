using IKEA.DAL.Models.Identity;
using IKEA.PL.ViewModel.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
	
	public class AccountController : Controller
	{
		#region Services
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
			this.userManager = userManager;
			this.signInManager = signInManager;
		}
        #endregion

        #region SignUp
        [HttpGet]
		public IActionResult SignUp()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpVM signUpVM)
		{
			if(!ModelState.IsValid) 
				return BadRequest();

			var user = await userManager.FindByNameAsync(signUpVM.UserName);

			if(user is not null)
			{
				ModelState.AddModelError(nameof(SignUpVM.UserName), "This UserName is already exist");
				return View(signUpVM);
			}

			user = new ApplicationUser()
			{
				FName = signUpVM.FristName,
				LName = signUpVM.LastName,
				UserName = signUpVM.UserName,
				Email = signUpVM.Email,
				IsAgree = signUpVM.IsAgree,
			};

			var result = await userManager.CreateAsync(user, signUpVM.Password);

			if (result.Succeeded)
				return RedirectToAction(nameof(LogIn));

			foreach(var error in result.Errors)
				ModelState.AddModelError(string.Empty, error.Description);

		return View(signUpVM);
		}
		#endregion

		#region LogIn
		[HttpGet]
		public IActionResult LogIn()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> LogIn(LogInVM logInVM)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var user = await userManager.FindByEmailAsync(logInVM.Email);

			if (user is not null)
			{
				var Result = await signInManager.PasswordSignInAsync(user, logInVM.Password,logInVM.RememberMe,true);

				if (Result.IsNotAllowed)
					ModelState.AddModelError(string.Empty, "your account is not confirmed"); 
				
				if (Result.IsLockedOut)
					ModelState.AddModelError(string.Empty, "your account is locked");

				if (Result.Succeeded)
					return RedirectToAction(nameof(HomeController.Index), "Home");
			}
			ModelState.AddModelError(string.Empty, "Invalid Log In Attempt ..!");
			return View(logInVM);

		}
		#endregion

		#region SignOut
		public async Task<IActionResult> SignOut()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction(nameof(LogIn));
		}
		#endregion

		#region ForgetPassword
		[HttpGet]
		public IActionResult ForgetPassword()
		{
			return View();
		}
		#endregion
	}
}
