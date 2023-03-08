namespace Store.Models
{
    public interface IImageRepository
    {
        public IQueryable<Image> Images {get;}
        public IQueryable<productImage> productImages {get;}
    }
}