namespace Store.Models.ViewModels;

public class PagingInfo
{
    public int TotalProducts {get;set;} //Всего продуктов в базе
    public int ProductsPerPage {get;set;} //Количество продуктов на страничке
    public int CurrentPage {get;set;} //Текущая страничка
   
    public int TotalPages() => (int) Math.Ceiling( (decimal)TotalProducts/ProductsPerPage); //Всего страниц = Количество продуктов/Количество продуктов на страничке
    
    public static int TotalPages(int TotalProducts, int ProductsPerPage) => (int) Math.Ceiling( (decimal)TotalProducts/ProductsPerPage);
}
