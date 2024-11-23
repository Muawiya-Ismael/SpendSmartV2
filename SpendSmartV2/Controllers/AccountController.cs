

using SpendSmartV2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpendSmartV2.Interface;

namespace SpendSmartV2.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IAccountServices _accountServices;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IAccountServices accountServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountServices = accountServices;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var user = new IdentityUser
                //{
                //    UserName = model.Email,
                //    Email = model.Email,
                //};

                //var result = await _userManager.CreateAsync(user, model.Password);
                var result = await _accountServices.Register(model);

                if (result.Succeeded)
                {
                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    await _accountServices.SignIn();

                    return RedirectToAction("index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Invalid");

            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                var result = await _accountServices.Login(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid Credentials");

            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            //await _signInManager.SignOutAsync();
            await _accountServices.Logout();

            return RedirectToAction("Login");
        }
    }
}
