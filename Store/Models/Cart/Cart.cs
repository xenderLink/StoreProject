using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models;

public class Cart 
{
    public List<CartLine>? Lines {get;set;} = new List<CartLine>();

    public virtual void Add(Product? product)
    {
        CartLine? line = Lines.FirstOrDefault(p=>p.Product?.productId==product?.productId);

        if(line==null)
        {
            Lines.Add(new CartLine{
                Product = product, Quantity = 1
            });
        }
        else
        {
            line.Quantity+=1;
        }
    }

    public virtual void Decrease(long productId)
    {
        CartLine? line = Lines.FirstOrDefault(p=>p.Product?.productId==productId);

        if(line?.Quantity>1)
        {
            line.Quantity-=1;
        }
    }

    public virtual void RemoveAll(long productId) => Lines.RemoveAll(p=>p.Product?.productId==productId);
    
    public virtual decimal Sum() => Lines.Sum(p=>Convert.ToDecimal(p?.Product?.price*p?.Quantity)); //Цена*на количество продуктов

    public virtual void Clear() => Lines.Clear();
}

[Table("cartline")]
public class CartLine
{
    [Column("order_id")]
    public long? orderId;
    public virtual Order? Order {get;set;}

    [Column("product_id")]
    public long? producId;
    public virtual Product? Product {get;set;}

    [Column("quantity")]
    public int Quantity {get; set;}
}