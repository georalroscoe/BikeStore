using System;
using System.Collections.Generic;

namespace Domain;

public class Customer
{
    public Customer(int customerId, string firstName, string lastName, string? phone, string email, string? street, string? city, string? state, string? zipCode)
    {
        CustomerId = customerId;
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
       
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

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
