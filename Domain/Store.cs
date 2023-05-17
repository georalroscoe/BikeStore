using System;
using System.Collections.Generic;

namespace Domain;

public class Store
{
    private Store() {
    Orders = new List<Order>();
    Staff = new List<Staff>();
    Stocks = new List<Stock>();
    }
    //public Store(int storeId, string storeName, string? phone, string? email, string? street, string? city, string? state, string? zipCode)
    //{
    //    StoreId = storeId;
    //    StoreName = storeName;
    //    Phone = phone;
    //    Email = email;
    //    Street = street;
    //    City = city;
    //    State = state;
    //    ZipCode = zipCode;

    //}

    public int StoreId { get; private set; }

    public string StoreName { get; private set; } = null!;

    public string? Phone { get; private set; }

    public string? Email { get; private set; }

    public string? Street { get; private set; }

    public string? City { get; private set; }

    public string? State { get; private set; }

    public string? ZipCode { get; private set; }

    public virtual ICollection<Order> Orders { get; private set; } = new List<Order>();

    public virtual ICollection<Staff> Staff { get; private set; } = new List<Staff>();

    public virtual ICollection<Stock> Stocks { get; private set; } = new List<Stock>();
}
