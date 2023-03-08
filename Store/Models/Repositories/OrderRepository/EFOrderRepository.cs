using Microsoft.EntityFrameworkCore;
using Store.Data;

namespace Store.Models;

public class EFOrderRepository : IOrderRepository
{
    private StoreDbContext context;

    public EFOrderRepository(StoreDbContext ctx)
    {
        context = ctx;
    }

    public IQueryable<Order> Orders => 
    context.orders
    .Include(c=>c.Cart)
    .ThenInclude(p=>p.Product);

    public async Task SaveOrderAsync(Order order)
    {
        context.AttachRange(order.Cart.Select(p=>p.Product));

        if(order.OrderId == 0)
        {
            await context.AddAsync(order);
        }

        await context.SaveChangesAsync();
    }

    public async Task ChangeStatusAsync(Order order)
    {
        context.orders.Update(order);
        await context.SaveChangesAsync();
    }
}