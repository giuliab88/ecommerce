namespace SampleCommerce.DTOs.Product
{
    public class DtoProductUpdate
    {
        public string? Name { get; set; } = null!;
        public string? Brand { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public bool? IsActive { get; set; }
    }
}
