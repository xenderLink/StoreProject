using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.Models;

namespace Store.Data;

public class StoreDbContext : IdentityDbContext<StoreUser>
{
    public StoreDbContext(DbContextOptions<StoreDbContext> options)
    : base (options) {}

    public DbSet<Category> categories {get;set;}
    public DbSet<SubCategory> subcategories {get;set;}
    public DbSet<Product> products {get;set;}
    public DbSet<Order> orders {get;set;}
    public DbSet<Image> images {get;set;}
    public DbSet<ProductImage> product_images{get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp"); 

        modelBuilder.Entity<SubCategory>()
                    .HasMany(p=>p.Products)
                    .WithOne(t=>t.Type)
                    .HasForeignKey(k=>new {k.typeId})
                    .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<SubCategory>()
                    .HasOne(c=>c.Category)
                    .WithMany(s=>s.SubCategories)
                    .HasForeignKey(k=>new {k.categoryId})
                    .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<CartLine>()
                    .HasKey(cl=>new {cl.orderId, cl.producId});

        modelBuilder.Entity<CartLine>()
                    .HasOne(o=>o.Order)
                    .WithMany(c=>c.Cart)
                    .HasForeignKey(o=>o.orderId);

        modelBuilder.Entity<CartLine>()
                    .HasOne(p=>p.Product)
                    .WithMany(l=>l.Lines)
                    .HasForeignKey(p=>p.producId);

        modelBuilder.Entity<ProductImage>()
                    .HasKey(pi=>new {pi.productId, pi.imageId});

        modelBuilder.Entity<ProductImage>()
                    .HasOne(p=>p.Product) 
                    .WithMany(pi=>pi.ProductImages)
                    .HasForeignKey(k=> new {k.productId});

        modelBuilder.Entity<ProductImage>()
                    .HasOne(i=>i.Image) 
                    .WithMany(pi=>pi.ProductImages)
                    .HasForeignKey(k=> new {k.imageId});

        modelBuilder.Entity<Order>()
                    .HasOne(u=>u.User)
                    .WithMany(o=>o.Orders)
                    .HasForeignKey(k=> new {k.userId});

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity <StoreUser> (entity=> 
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