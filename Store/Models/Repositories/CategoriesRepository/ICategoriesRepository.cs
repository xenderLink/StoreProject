namespace Store.Models
{
    public interface ICategoriesRepository
    {
        IEnumerable<Category> Categories {get;}
        IQueryable<SubCategory> SubCategories {get;}
    }
}