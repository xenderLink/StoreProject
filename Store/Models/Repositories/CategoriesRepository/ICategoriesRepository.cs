namespace Store.Models
{
    public interface ICategoriesRepository
    {
        IEnumerable<Category> Categories {get;}
        IQueryable<ChildCategory> Childs {get;}
    }
}