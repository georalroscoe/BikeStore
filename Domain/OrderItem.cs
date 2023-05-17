﻿using System;
using System.Collections.Generic;

namespace Domain;

public partial class OrderItem
{
    public OrderItem(int orderId, int itemId, int productId, int quantity, decimal listPrice, decimal discount)
    {
        OrderId = orderId;
        ItemId = itemId;
        ProductId = productId;
        Quantity = quantity;
        ListPrice = listPrice;
        Discount = discount;
        
    }

    public int OrderId { get; private set; }

    public int ItemId { get; private set; }

    public int ProductId { get; private set; }

    public int Quantity { get; private set; }

    public decimal ListPrice { get; private set; }

    public decimal Discount { get; private set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
