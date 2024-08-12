using Microsoft.AspNetCore.Identity;
using School.Models;
using School.ModelViews;
using System.Security.Claims;

namespace School.Services.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserAsync(ClaimsPrincipal user);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        List<ApplicationUser> GetAllUsers();
        Task<IdentityResult> RegisterUser(RegisterViewModel model);
        Task<IdentityResult> DeleteUserAsync(ApplicationUser user);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
    }
}
