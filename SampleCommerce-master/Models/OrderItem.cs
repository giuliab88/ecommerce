using System;
using System.Collections.Generic;

namespace SampleCommerce.Models;

public partial class OrderItem
{
    public int OrderId { get; set; }

    public Guid SkuId { get; set; }

    public int Quantity { get; set; }

    public decimal? MomentPrice { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual StockKeepingUnit Sku { get; set; } = null!;
}
