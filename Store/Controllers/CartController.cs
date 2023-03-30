using Microsoft.AspNetCore.Mvc;

using Store.Models;
using Store.Models.ViewModels;

namespace Store.Controllers;

public class CartController : Controller
{
    private IProductsRepository repository;
    private Cart cart {get;set;}

    public CartController (IProductsRepository rep, Cart cartService)
    {
        repository = rep;
        cart = cartService;

    }
    public IActionResult Index ()
    {
     
        CartViewModel CartVM = new()
        {
            Cart =  this.cart,
            GrandTotal = this.cart.Sum()
        };

        ViewBag.Controller = RouteData?.Values["controller"];

        return View(CartVM);
    }

    public IActionResult Add (long? productId)
    {
        Product? product = repository.Products.FirstOrDefault(x=>x.productId==productId);

        if (product == null)
        return NotFound();

        cart.Add(product);

        return Redirect (Request.Headers["Referer"].ToString());

    }

    public IActionResult Decrease(long productId)
    {

        cart.Decrease(productId);        

        return RedirectToAction("Index");

    }

    public IActionResult Remove(long productId)
    {

        cart.RemoveAll(productId);

        return RedirectToAction("Index");
    
    }
    
    public IActionResult Clear()
    {
        cart.Clear();

        return Redirect(Request.Headers["Referer"].ToString());
    }
}
