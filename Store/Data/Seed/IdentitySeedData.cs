using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Store.Data;
public enum Role
{
    Admin,
    Moderator,
    Basic
}
public static class IdentitySeedData
{
    private const string Admin = "Admin";
    private const string adminPassword = "MyAdmin174#";
    private const string Moderator = "Moderator";
    private const string moderatorPassword = "MyModerator174#" ;
    private const string BasicUser = "TestUser";
    private const string basicUserPassword = "MyTest174#";

    public static async Task SeedIdentityDataAsync(IApplicationBuilder app)
    {
        StoreIdentityDbContext context = app.ApplicationServices
                                            .CreateScope()
                                            .ServiceProvider
                                            .GetRequiredService<StoreIdentityDbContext>();
        
        UserManager<IdentityUser> userManager = app.ApplicationServices
                                                   .CreateScope()
                                                   .ServiceProvider
                                                   .GetRequiredService<UserManager<IdentityUser>>();

        RoleManager<IdentityRole> roleManager = app.ApplicationServices
                                                   .CreateScope()
                                                   .ServiceProvider
                                                   .GetRequiredService<RoleManager<IdentityRole>>();

        
        if ( context.Database.GetPendingMigrations().Any() ) 
                     context.Database.Migrate();
        
        else return;
     
        foreach (Role role in (Role[]) Enum.GetValues( typeof(Role) ))
        {
            if ( !await roleManager.RoleExistsAsync(role.ToString() ))
                        await roleManager.CreateAsync 
                        ( new IdentityRole ( role.ToString() ));
        }

        var admin =  await userManager.FindByNameAsync(Admin);

        if ( admin == null )
        {
            admin = new IdentityUser(Admin);
            admin.Email = "admin@mail.com";

            var result = await userManager.CreateAsync(admin, adminPassword);
            
            if ( result.Succeeded )
            {
                await userManager.AddToRoleAsync(admin, Role.Admin.ToString() );
            }
        }

        var moderator = await userManager.FindByNameAsync(Moderator);

        if ( moderator == null )
        {
            moderator = new IdentityUser(Moderator);
            moderator.Email = "moderator@mail.com";

            var result = await userManager.CreateAsync(moderator, moderatorPassword);
            
            if (result.Succeeded)
            await userManager.AddToRoleAsync(moderator, Role.Moderator.ToString() );
        }

        var user = await userManager.FindByNameAsync(BasicUser);

        if (user == null)
        {
            user = new IdentityUser(BasicUser);
            user.Email = "basicuser@mail.com";

            var result = await userManager.CreateAsync(user, basicUserPassword);
            
            if (result.Succeeded)
            await userManager.AddToRoleAsync(user, Role.Basic.ToString());
        }   
    }
}