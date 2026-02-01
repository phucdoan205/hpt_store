namespace backend.Models.Products
{
    public class Product
    {
        public int Id { get; set; }

        public required string Name { get; set; }
        public required string Slug { get; set; }
        public required string Description { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public required string Thumbnail { get; set; }
        public bool IsActive { get; set; }

        public ICollection<ProductVariant> Variants { get; set; }
        public ICollection<ProductImage> Images { get; set; }
    }
}