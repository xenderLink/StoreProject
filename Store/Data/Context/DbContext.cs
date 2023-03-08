using Microsoft.EntityFrameworkCore;
using Store.Models;

namespace Store.Data;
public class StoreDbContext : DbContext
{
    public StoreDbContext(DbContextOptions<StoreDbContext> options)
    : base (options) {}

    public DbSet<Category>? categories {get;set;}
    public DbSet<ChildCategory>? childs {get;set;}
    public DbSet<Product>? products {get;set;}
    public DbSet<Order>? orders {get;set;}
    public DbSet<Image>? images {get;set;}

    public DbSet<productImage> productImages{get;set; }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {

    modelBuilder.HasPostgresExtension("uuid-ossp"); 
    
    modelBuilder.Entity<ChildCategory>()
     .HasOne(c=>c.category)
     .WithMany(c=>c.childs)
     .HasForeignKey(c=>new {c.categoryId})
     .OnDelete(DeleteBehavior.NoAction); 

     modelBuilder.Entity<ChildCategory>()
     .HasMany(p=>p.products)
     .WithOne(p=>p.type)
     .HasForeignKey(p=>new{p.typeId})
     .OnDelete(DeleteBehavior.NoAction);

     modelBuilder.Entity<CartLine>()
     .HasKey(cl => new{cl.orderId, cl.producId});

     modelBuilder.Entity<CartLine>()
     .HasOne(o=>o.Order)
     .WithMany(c=>c.Cart)
     .HasForeignKey(o=>o.orderId);

     modelBuilder.Entity<CartLine>()
     .HasOne(p=>p.Product)
     .WithMany(l=>l.Lines)
     .HasForeignKey(p=>p.producId);

     modelBuilder.Entity<productImage>()
     .HasKey(pi=>new{pi.productId, pi.imageId});

     modelBuilder.Entity<productImage>()
     .HasOne(p=>p.Product)
     .WithMany(p=>p.ProductImages)
     .HasForeignKey(p=>new{p.productId});

     modelBuilder.Entity<productImage>()
     .HasOne(i=>i.Image)
     .WithMany(i=>i.ProductImages)
     .HasForeignKey(i=>new{i.imageId});
    
   }
    
}


