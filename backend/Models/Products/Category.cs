using System;
namespace backend.Models.Products
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Slug { get; set; }

        public int? ParentId { get; set; }
        public Category? Parent { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; } = false;

        public ICollection<Product> Products { get; set; }
    }
}