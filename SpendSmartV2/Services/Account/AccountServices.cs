using Microsoft.AspNetCore.Identity;
using SpendSmartV2.Interface;
using SpendSmartV2.Models;

namespace SpendSmartV2.Services.Account
{
    public class AccountServices : IAccountServices
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private IdentityUser user;

        public AccountServices(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<SignInResult> Login(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
        }

        public async Task Logout()
        {
             await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> Register(RegisterViewModel model)
        {
            user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
            };

            return await _userManager.CreateAsync(user, model.Password);
        }

        public async Task SignIn()
        {
            await _signInManager.SignInAsync(user, false);
        }

    }
}
