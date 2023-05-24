using System;
using System.Collections.Generic;

namespace Domain;

public class Customer
{
    private Customer() { 
    Orders = new List<Order>();
            
      }
    public Customer( string firstName, string lastName, string? phone, string email, string? street, string? city, string? state, string? zipCode) : this()
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

    public Order AddOrder(int staffId, int storeId)
    {

        var order = new Order(0, staffId, storeId);
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
