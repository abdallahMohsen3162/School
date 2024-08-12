using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using School.Data;
using School.Models;
using AutoMapper;
using School.Services;
using School.Controllers;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddAutoMapper(typeof(Program).Assembly);



builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<DashboardService>();


builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(option =>
{
    option.Password.RequiredLength = 6;
});

string baseUrl = "/Accounts";
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"{baseUrl}/Login";
    options.LogoutPath = $"{baseUrl}Logout";

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Ensure this is added
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    await ApplicationDbInitializer.SeedAsync(userManager,
        builder.Configuration["AdminPassword"],
        builder.Configuration["AdminEmail"]);
}

app.Run();