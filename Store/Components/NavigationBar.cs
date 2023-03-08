using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Store.Models.ViewModels;

namespace Store.Components;

public class NavigationBar : ViewComponent
{
    private Cart cart; 
    public NavigationBar (Cart cartService) =>
    cart = cartService;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var result = await Task.Run( ()=>Invoke() );
        return result;
    }
    private IViewComponentResult Invoke()
    {
        NavCartViewModel? cartModel;

        if (cart?.Lines?.Count == 0)
        cartModel = new();

        else
        {
            cartModel = new ()
            {
                NumberOfProducts = cart.Lines.Sum(s =>s.Quantity),
                TotalAmount = cart.Sum()
            };
            var t = cartModel.GetType();
            var b = t.IsValueType; 
        }

        return View (cartModel);
    }
}