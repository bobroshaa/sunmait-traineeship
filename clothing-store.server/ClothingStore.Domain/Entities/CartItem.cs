﻿using ClothingStore.Domain.Enums;

namespace ClothingStore.Domain.Entities;

public class CartItem
{
    public int ID { get; set; }
    public int Quantity { get; set; }
    public CartItemStatus Status { get; set; }
    public bool IsActive { get; set; }
    
    public int ProductID { get; set; }
    public virtual Product Product { get; set; }

    public int UserID { get; set; }
    public virtual UserAccount User { get; set; }
}