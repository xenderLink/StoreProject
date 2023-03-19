namespace Store.Models
{
    public interface IImageRepository
    {
        public IQueryable<Image> Images {get;}
        public IQueryable<ProductImage> ProductImages {get;}
    }
}