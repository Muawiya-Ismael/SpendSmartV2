using Microsoft.AspNetCore.Identity;
using SpendSmartV2.Models;

namespace SpendSmartV2.Interface
{
    public interface IAccountServices
    {
        Task<IdentityResult> Register(RegisterViewModel model);
        Task<SignInResult> Login(LoginViewModel model);
        Task Logout();
        Task SignIn();
    }
}
