using Store.Infrasctructure;
using System.Text.Json.Serialization;

///<sumary>
/// Для Dependency Injection
///</sumary>

namespace Store.Models;

public class SessionCart : Cart
{
    [JsonIgnore]
    public ISession? Session {get;set;}
    public static Cart GetCart(IServiceProvider services)
    {
        ISession? session = services?.GetRequiredService<IHttpContextAccessor>()
                            .HttpContext?.Session;
        
        SessionCart cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();
        cart.Session = session;

        return cart;
    }
    public override void Add(Product? product)
    {
        base.Add(product);
        Session?.SetJson("Cart", this);
    }

    public override void Decrease(long productId)
    {
        base.Decrease(productId);
        Session?.SetJson("Cart", this);
    }

    public override void RemoveAll(long productId)
    {
        base.RemoveAll(productId);
        Session?.SetJson("Cart", this);
    }

    public override void Clear()
    {
        base.Clear();
        Session?.Remove("Cart");
    }
}