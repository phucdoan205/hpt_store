namespace backend.DTOs.Catalog.Product
{
    public class CreateProductRequestDto
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        public Guid CategoryId { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsActive { get; set; } = true;
    }
}