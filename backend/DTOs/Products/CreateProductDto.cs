using Microsoft.AspNetCore.Http;

namespace backend.DTOs.Products
{
    public class CreateProductDto
    {
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public IFormFile? ThumbnailFile { get; set; }
        public IFormFile[]? ImageFiles { get; set; }
    }
}