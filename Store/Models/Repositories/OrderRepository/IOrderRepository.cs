namespace Store.Models
{
    public interface IOrderRepository
    {
        public IQueryable<Order> Orders {get;}
        public IQueryable<StoreUser> Users {get;}
        public Task SaveOrderAsync (Order order);
        public Task ChangeStatusAsync(Order order);
        
    }
}