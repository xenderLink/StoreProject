using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.Models.ViewModels;

public class AddToCart : ViewComponent
{
    private Cart cart;
    private long? productId;

    public AddToCart(Cart cartService) =>
    cart = cartService;
    
    public async Task<IViewComponentResult> InvokeAsync(long? id)
    {
      productId = id;  
      var result = await Task.Run( ()=>Invoke() ); 
      return result;
    }
    private IViewComponentResult Invoke()
    {

      InCartViewModel VM;
       
      if ( cart.Lines.FirstOrDefault(p=>p.Product.productId==productId)!=null )
      {
        VM = new()
        {
          inCart = true
        };
      }
       
      else
      {
        VM = new()
        {
          Id = productId
        };
      }
       
       return View(VM);
    }
}