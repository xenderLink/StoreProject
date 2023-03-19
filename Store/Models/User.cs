using Microsoft.AspNetCore.Identity;

namespace Store.Models;

public class StoreUser : IdentityUser
{
    public ICollection<Order>? Orders {get;set;}

    public StoreUser(string userName) => base.UserName = userName;
}