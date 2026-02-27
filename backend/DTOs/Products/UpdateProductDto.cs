namespace backend.DTOs.Products
{
    public class UpdateProductDto
    {
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public required string Thumbnail { get; set; }
        public bool IsActive { get; set; }
    }
}
