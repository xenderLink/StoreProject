namespace Store.Models.ViewModels;

public class CatalogViewModel
{
    public IEnumerable <Category> Categories {get;set;} = Enumerable.Empty<Category>();
    public IEnumerable <SubCategory> SubCategories {get;set;} = Enumerable.Empty<SubCategory>();
} 