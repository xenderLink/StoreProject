using Microsoft.EntityFrameworkCore;
using Store.Data;

namespace Store.Models;

public class EFImageRepository  : IImageRepository
{
    private StoreDbContext context;
   
    public EFImageRepository (StoreDbContext ctx) => context = ctx;
   
    public IQueryable<Image> Images => context.images;

    public IQueryable<ProductImage> ProductImages => context.product_images;
}