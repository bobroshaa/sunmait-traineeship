﻿using ClothingStore.Entities.Enums;

namespace ClothingStore.Entities;

public class Address
{
    public int UserID { get; set; }
    public Country Country {get; set;}
    public string District { get; set; }
    public string City { get; set; }
    public string Postcode { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }

    public virtual UserAccount User { get; set; }
}