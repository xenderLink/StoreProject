using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Store.Models;



[Authorize(Roles = "Admin,Moderator")]
public class CrudOrder : Controller
{
    private IOrderRepository repository;
    public CrudOrder(IOrderRepository rep) =>
    repository = rep;

    [Route("/admin/orders")]
    public async Task <IActionResult> Index ()
    {
        var orders = await repository.Orders.ToListAsync();

        return View("~/Views/Crud/Orders/Index.cshtml", orders);
    }

    public async Task <IActionResult> ChangeStatus(long? id)
    {
        Order? order = await repository.Orders.FirstOrDefaultAsync(o=>o.OrderId==id);

        return Redirect (Request.Headers["Referer"].ToString());
    }

}