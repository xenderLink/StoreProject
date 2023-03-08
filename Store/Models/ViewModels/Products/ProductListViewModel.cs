namespace Store.Models.ViewModels;
public class ProductListViewModel
{
    public IEnumerable<ProductListItem> Products {get; set;} = Enumerable.Empty<ProductListItem>();

    public PagingInfo PagingInfo {get;set;} = new ();
}
public class ProductListItem
{
    public long productId {get;set;}
    public string? Name {get;set;}
    public decimal? Price {get;set;}
    public string? Path {get;set;}
    public string? Description {get;set;}
}