using System;
using System.Collections.Generic;

namespace Domain;

public class Order
{

    public Order(int orderId, int? customerId, byte orderStatus, DateTime orderDate, DateTime requiredDate, DateTime? shippedDate, int storeId, int staffId)
    {
        OrderId = orderId;
        CustomerId = customerId;
        OrderStatus = orderStatus;
        OrderDate = orderDate;
        RequiredDate = requiredDate;
        ShippedDate = shippedDate;
        StoreId = storeId;
        StaffId = staffId;
        
    }

    public int OrderId { get; private set; }

    public int? CustomerId { get; private set; }

    public byte OrderStatus { get; private set; }

    public DateTime OrderDate { get; private set; }

    public DateTime RequiredDate { get; private set; }

    public DateTime? ShippedDate { get; private set; }

    public int StoreId { get; private set; }

    public int StaffId { get; private set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Staff Staff { get; set; } = null!;

    public virtual Store Store { get; set; } = null!;
}
