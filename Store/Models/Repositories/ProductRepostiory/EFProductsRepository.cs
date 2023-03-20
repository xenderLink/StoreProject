using Microsoft.EntityFrameworkCore;
using Store.Data;

namespace Store.Models;

public class EFProductsRepository : IProductsRepository
{
    private StoreDbContext context;

    public EFProductsRepository(StoreDbContext ctx) => context = ctx;

    public IQueryable <Product> Products => context.products;

    public async Task CreateProductAsync (Product p)
    {
        await context.products.AddAsync(p);
        await context.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(Product p)
    {
        context.products.Update(p);
        await context.SaveChangesAsync();
    }
    
    public async Task DeleteProductAsync (Product p)
    {
        context.products.Remove(p);
        await context.SaveChangesAsync();               
    }
}