using Microsoft.AspNetCore.Identity;
using School.Models;
using System.Security.Claims;

namespace School.Services.Interfaces
{
    public interface IAuthService
    {
        Task SignInUserAsync(ApplicationUser user, bool isPersistent = false);
        Task<SignInResult> SignInWithEmailAndPasswordAsync(string email, string password, bool rememberMe);
        Task SignOutUserAsync();
        bool IsUserAuthenticated(ClaimsPrincipal user);
    }
}
