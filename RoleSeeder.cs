using Microsoft.AspNetCore.Identity;

namespace SchoolManagementWebApp
{
    public class RoleSeeder
    {
        public  async Task SeedRoles(RoleManager<IdentityRole> RoleManager)
        {
            string[] roleNames = { "Admin", "Principal","Teacher", "Student" };
            foreach(var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}
