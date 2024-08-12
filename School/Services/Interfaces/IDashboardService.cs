using Microsoft.AspNetCore.Identity;
using School.Models;
using School.ModelViews;

public interface IDashboardService
{
    DashboardViewModel GetDashboardData();
    EditViewModel GetEditViewModel(ApplicationUser user);
    Task<ApplicationUser> GetUserByIdAsync(string id);
    Task<IdentityResult> DeleteUserAsync(ApplicationUser user);
    Task<IdentityResult> UpdateUserAsync(ApplicationUser user, string newPassword);
    Task<IdentityResult> CreateUserAsync(CreateUserViewModel model);
}

