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
    public Store(string storeName, string? phone, string? email, string? street, string? city, string? state, string? zipCode) : this ()
    {
       
        StoreName = storeName;
        Phone = phone;
        Email = email;
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;

    }

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

    public Stock? AddStock(int productId, int quantity)
    {
        // Check if a stock with the same store and product already exists
        bool stockExists = Stocks.Any(s => s.ProductId == productId);

        if (!stockExists)
        {
            // Create and add the new stock
            Stock stock = new Stock(StoreId, productId, quantity);
            Stocks.Add(stock);
            return stock;
        }

        // Return null or handle the case where the stock already exists
        return null;
    }

    public Staff AddStaff(string firstName, string lastName, string email, string? phone, byte active)
    {
        Staff staff = new Staff(firstName, lastName, email, phone, active, StoreId);
        Staff.Add(staff);
        return staff;
    }
}
