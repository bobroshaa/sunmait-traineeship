﻿using ClothingStore.Domain.Enums;

namespace ClothingStore.Domain.Entities;

public class UserAccount
{
    public int ID { get; set; }
    public string? Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsActive { get; set; }

    public virtual Address Address { get; set; }
    
    public virtual ICollection<CustomerOrder> CustomerOrders { get; set; }
    public virtual ICollection<Review> Reviews { get; set; }
}