using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolManagementWebApp;
using SchoolManagementWebApp.Data;
using SchoolManagementWebApp.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    options.SignIn.RequireConfirmedAccount = false) 
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddTransient<IEmailSender,EmailSender>();
builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews();
var app = builder.Build();

var roleManager = app.Services.CreateScope().ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
await new RoleSeeder().SeedRoles(roleManager); 
var userManager = app.Services.CreateScope().ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
await CreateInitialAdmin(app.Services);

async Task CreateInitialAdmin(IServiceProvider serviceProvider)
{
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }
    var adminConfig = builder.Configuration.GetSection("AdminUser");
    var email = adminConfig["Email"];
    var password = adminConfig["Password"];

    var adminUser = await userManager.FindByEmailAsync(email);
    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FullName = "System Administrator"
        };
        var result = await userManager.CreateAsync(adminUser, password);

    }
}
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
