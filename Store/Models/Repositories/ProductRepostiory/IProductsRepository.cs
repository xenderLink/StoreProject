namespace Store.Models
{
    public interface IProductsRepository
    {
        IQueryable<Product> Products {get;}
        public Task CreateProductAsync (Product p);
        public Task UpdateProductAsync (Product p);
        public Task DeleteProductAsync (Product p);

    }
}