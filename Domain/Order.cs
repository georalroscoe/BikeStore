using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Domain;

public class Order
{

    private Order()
    {
        OrderItems = new List<OrderItem>();
    }


    public Order( byte orderStatus, int storeId, int staffId) : this()
    {
        OrderDate = DateTime.Now;
        RequiredDate = OrderDate.AddDays(5);
        OrderStatus = orderStatus;
        StoreId = storeId;
        StaffId = staffId;


    }

    public void FillOrder(Stock stock, int itemId, int productId, decimal listPrice, int quantity, decimal discount)
    {
        bool hadQuantity = stock.TakeOrderProducts(quantity);
        if (!hadQuantity)
        {
            return;
        }
        OrderItem orderItem = new OrderItem(itemId, productId, quantity, listPrice, discount);
        OrderItems.Add(orderItem);
        return;
    }

    

    public int OrderId { get; private set; }

    public int? CustomerId { get; private set; }

    public byte OrderStatus { get; private set; }

    public DateTime OrderDate { get; private set; }

    public DateTime RequiredDate { get; private set; }

    public DateTime? ShippedDate { get; private set; }

    public int StoreId { get; private set; }

    public int StaffId { get; private set; }

    public virtual Customer? Customer { get; private set; }

    public virtual ICollection<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();

    public virtual Staff Staff { get; private set; } = null!;

    public virtual Store Store { get; private set; } = null!;
}
