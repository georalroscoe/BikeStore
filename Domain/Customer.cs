using System;
using System.Collections.Generic;

namespace Domain;

public class Customer
{
    private Customer() { 
    Orders = new List<Order>();
            
      }
    public Customer(  string firstName, string lastName, string? phone, string email, string? street, string? city, string? state, string? zipCode) : this()
    {
      
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;

    }

    public Order CreateOrder(OrderContainer orderContainer)
    {
        var newOrder = new Order(1, orderContainer.StoreId, orderContainer.StaffId, CustomerId);
        Orders.Add(newOrder);
        newOrder.FillOrder(orderContainer.Stocks, orderContainer.OrderItems);
        return newOrder;
    }

    public Order AddOrder(int storeId, int staffId)
    {

        var order = new Order(0, storeId, staffId, CustomerId);
        Orders.Add(order);
        return order;
    }


    public int CustomerId { get; private set; }

    public string FirstName { get; private set; } = null!;

    public string LastName { get; private set; } = null!;

    public string? Phone { get; private set; }

    public string Email { get; private set; } = null!;

    public string? Street { get; private set; }

    public string? City { get; private set; }

    public string? State { get; private set; }

    public string? ZipCode { get; private set; }

    public virtual ICollection<Order> Orders { get; private set; } = new List<Order>();
}
