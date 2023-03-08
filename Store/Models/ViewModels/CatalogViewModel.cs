
namespace Store.Models.ViewModels;

public class CatalogViewModel
{
    public IEnumerable <Category> Categories {get;set;} = Enumerable.Empty<Category>();
    public IEnumerable <ChildCategory> subCategories {get;set;} = Enumerable.Empty<ChildCategory>();
} 