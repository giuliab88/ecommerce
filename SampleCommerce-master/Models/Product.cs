namespace SampleCommerce.Models;
public partial class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Brand { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool? IsActive { get; set; }
    public string? ImageUrl { get; set; }
    public int TotalReviews { get; set; }
    public double MediaReviews { get; set; }
    public Guid SellerId { get; set; }
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    public virtual User Seller { get; set; } = null!;
    public virtual ICollection<StockKeepingUnit> StockKeepingUnits { get; set; } = new List<StockKeepingUnit>();
}
