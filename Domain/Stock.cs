using System;
using System.Collections.Generic;

namespace Domain;

public class Stock
{

    public Stock(int storeId, int productId, int? quantity)
    { 
        StoreId = storeId;
        ProductId = productId;
        Quantity = quantity;
      
    }

    public int StoreId { get; private set; }

    public int ProductId { get; private set; }

    public int? Quantity { get; private set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Store Store { get; set; } = null!;
}
