using System;
using System.Collections.Generic;

namespace SampleCommerce.Models;

public partial class Address
{
    public int Id { get; set; }

    public string Country { get; set; } = null!;

    public string Receiver { get; set; } = null!;

    public string? Phone { get; set; }

    public string StreetAdress { get; set; } = null!;

    public string ComplementAddress { get; set; } = null!;

    public string ZipCode { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Province { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();
}
