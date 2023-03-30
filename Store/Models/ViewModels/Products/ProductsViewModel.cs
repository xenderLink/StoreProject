using Microsoft.AspNetCore.Mvc.Rendering;

namespace Store.Models.ViewModels;

public class ProductsViewModel
{
    public IEnumerable<Product> Products {get;set;} = Enumerable.Empty<Product>();
    public Dictionary <int, string> Categories {get;set;} 
    public PagingInfo PagingInfo {get;set;} = new ();
}
