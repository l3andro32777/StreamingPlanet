using Microsoft.AspNetCore.Identity;
using StreamingPlanet.Models;

namespace StreamingPlanet
{
    public static class Configurations
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<CinemaUser>>();
            string[] roleNames = { "Dono", "Gerente", "Default" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist) await roleManager.CreateAsync(new IdentityRole(roleName));
            }
            var manager = new CinemaUser
            {
                Name = "Conta de Gerente",
                UserName = "manager@ips.pt",
                Email = "manager@ips.pt"
            };
            var _user = await userManager.FindByEmailAsync(manager.Email);
            if (_user != null) return;
            var createPowerUser = await userManager.CreateAsync(manager, "Password_123");
            if (createPowerUser.Succeeded)
                await userManager.AddToRoleAsync(manager, "Gerente");
        }
    }
}
