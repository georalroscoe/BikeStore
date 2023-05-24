using System;
using System.Collections.Generic;

namespace Domain;

public class Stock
{
    private Stock() { }
    public Stock(int productId, int quantity) : this()
    {
       
        ProductId = productId;
        Quantity = quantity;

    }

    public int StoreId { get; private set; }

    public int ProductId { get; private set; }

    public int Quantity { get; private set; }
    
    public byte[] TimeStamp { get; private set; }

    public virtual Product Product { get; private set; } = null!;

    public virtual Store Store { get; private set; } = null!;

    public bool TakeProduct(int quantity)
    {
        if (quantity >= Quantity)
        {
            Quantity -= quantity;
            return true;
        }
        else
        {
            return false;
        }
    }
}
