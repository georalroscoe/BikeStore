using System;
using System.Collections.Generic;

namespace Domain;

public class Staff
{
    public Staff(int staffId, string firstName, string lastName, string email, string? phone, byte active, int storeId, int? managerId)
    {
        StaffId = staffId;
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

    public virtual ICollection<Staff> InverseManager { get; set; } = new List<Staff>();

    public virtual Staff? Manager { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Store Store { get; set; } = null!;
}
