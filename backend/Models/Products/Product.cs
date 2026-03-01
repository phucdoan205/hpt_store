using backend.Models.Base;

namespace backend.Models.Products
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public string Thumbnail { get; set; }
        public bool IsActive { get; set; }

        // Navigation
        public ICollection<ProductVariant> Variants { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public ICollection<ProductReview> Reviews { get; set; }
    }
}