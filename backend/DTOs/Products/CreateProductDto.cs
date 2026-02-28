namespace backend.DTOs.Products
{
    public class CreateProductDto
    {
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string? Thumbnail { get; set; }
        public IFormFile? ThumbnailFile { get; set; }
        public bool IsActive { get; set; } = true;
    }
}