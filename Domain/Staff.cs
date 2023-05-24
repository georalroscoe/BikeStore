using System;
using System.Collections.Generic;

namespace Domain;

public class Staff
{
    private Staff() { 
    Orders = new List<Order>();
        InverseManager = new List<Staff>();
    }
    public Staff(string firstName, string lastName, string email, string? phone, byte active, int storeId, int? managerId) : this()
    {
        
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Active = active;
        StoreId = storeId;
        ManagerId = managerId;

    }

    public int StaffId { get; private set; }

    public string FirstName { get; private set; } = null!;

    public string LastName { get; private set; } = null!;

    public string Email { get; private set; } = null!;

    public string? Phone { get; private set; }

    public byte Active { get; private set; }

    public int StoreId { get; private set; }

    public int? ManagerId { get; private set; }

    public virtual ICollection<Staff> InverseManager { get; private set; } = new List<Staff>();

    public virtual Staff? Manager { get; private set; }

    public virtual ICollection<Order> Orders { get; private set; } = new List<Order>();

    public virtual Store Store { get; private set; } = null!;
}
