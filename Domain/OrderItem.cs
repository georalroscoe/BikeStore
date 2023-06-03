using System;
using System.Collections.Generic;

namespace Domain;

public partial class OrderItem
{

    private OrderItem() { }
    public OrderItem(int itemId, int productId, int quantity,  decimal discount, decimal listPrice) : this()
    {
       
        ItemId = itemId;
        ProductId = productId;
        Quantity = quantity;
       
        Discount = discount;
        ListPrice = listPrice;

    }

    public int OrderId { get; private set; }

    public int ItemId { get; private set; }

    public int ProductId { get; private set; }

    public int Quantity { get; private set; }

    public decimal ListPrice { get; private set; }

    public decimal Discount { get; private set; }

    public virtual Order Order { get; private set; } = null!;

    public virtual Product Product { get; private set; } = null!;
}
