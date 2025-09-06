using Microsoft.AspNetCore.Identity;


namespace BOOKSTORE.Controllers
{
    public class SeedStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                using var scope = app.ApplicationServices.CreateScope();
                var services = scope.ServiceProvider;
                var config = services.GetRequiredService<IConfiguration>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                SeedAsync(roleManager, userManager, config).GetAwaiter().GetResult();
                next(app);
            };
        }

        private static async Task SeedAsync(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            string[] roles = new[] { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role)) // «‰ »Â ··≈‘«—… !
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }


            var adminEmail = config["Admin:Email"] ?? "admin@bookshop.local";
            var adminPassword = config["Admin:Password"] ?? "Admin@12345";

            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (admin == null)
            {
                admin = new ApplicationUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
                var result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}