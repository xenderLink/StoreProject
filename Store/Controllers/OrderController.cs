using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Extensions; 
using Microsoft.AspNetCore.Mvc;

using Store.Models;

namespace Store.Controllers;

public class OrderController : Controller
{
    private IOrderRepository repository;
    private Cart cart;

    public OrderController(IOrderRepository rep, Cart cartService)
    {
        repository = rep;
        cart = cartService;
    }

    [HttpGet]
    public IActionResult Checkout()
    {
        if(User?.Identity?.IsAuthenticated == true && !User.IsInRole("Basic") )
           return Redirect("/");
        
        return View(new Order());
    } 

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout(Order order)
    {
        
        if(cart.Lines.Count() == 0)
           ModelState.AddModelError("Cart", "В корзине нет товаров!");
        
        if(order.number?.Length != 0)
        {
            string errorNumber = "Номер должен быть в формате: +7(9**)(***)-(**)-(**) или 8(9**)(***)-(**)-(**)";

            switch(order.number?.Length)
            {
                case 12 :
                {
                    if(!Regex.IsMatch(order.number, @"(\+)(7)(9)[0-9]+"))
                       ModelState.AddModelError("number", errorNumber);
                    break;
                }
                
                case 11 :
                {
                    if(!Regex.IsMatch(order.number, @"(8)(9)[0-9]+"))
                       ModelState.AddModelError("number", errorNumber);
                    break;
                }
            }
        }

        if(ModelState.IsValid)
        {
            order.Cart = cart.Lines;

            if(User?.Identity?.IsAuthenticated == true)
            {
                order.userId = await repository.Users.Where(u=>u.UserName==User.Identity.Name).Select(i=>i.Id).FirstOrDefaultAsync();
            }

            await repository.SaveOrderAsync(order);
            cart.Clear();
            return RedirectToAction("Completed", new {OrderId = order.OrderId});
        }

        return View(new Order());
    }


    public ViewResult Completed(long OrderId)
    {
        return View(new Order{OrderId = OrderId});
    }
}
