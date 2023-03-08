using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Store.Data;
public class StoreIdentityDbContext : IdentityDbContext<IdentityUser>
{
    public StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> options)
     : base(options) {}

     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    
        modelBuilder.Entity <IdentityUser> (entity=> 
        {
            entity.ToTable("users");
        });
        modelBuilder.Entity <IdentityUserRole <string>> (entity=>
        {
            entity.ToTable("user_roles");
        });
        modelBuilder.Entity <IdentityUserClaim <string>> (entity=>
        {
            entity.ToTable("user_claims");
        });
        modelBuilder.Entity <IdentityUserLogin <string>> (entity=>
        {
            entity.ToTable("user_logins");
        });
        modelBuilder.Entity <IdentityUserToken <string>> (entity=>
        {
            entity.ToTable("user_tokens");
        });
        modelBuilder.Entity <IdentityRole> (entity=>
        {
            entity.ToTable("roles");
        });
        modelBuilder.Entity <IdentityRoleClaim <string>> (entity=>
        {
            entity.ToTable("role_claims");
        });
    } 
}

