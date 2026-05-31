using System;
using System.Collections.Generic;

namespace SampleCommerce.Models;

public partial class UserAddress
{
    public Guid UserId { get; set; }

    public int AddressId { get; set; }

    public bool IsPreferred { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
