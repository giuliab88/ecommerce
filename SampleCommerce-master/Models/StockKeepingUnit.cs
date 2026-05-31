namespace SampleCommerce.Models;

public partial class StockKeepingUnit
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string Features { get; set; } = null!;
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public virtual Product Product { get; set; } = null!;
}
