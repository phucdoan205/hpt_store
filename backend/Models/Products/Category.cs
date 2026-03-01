using backend.Models.Base;

namespace backend.Models.Products
{
    public class Category : BaseModel
    {
        public string Name { get; set; }
        public string Slug { get; set; }

        public Guid? ParentId { get; set; }
        public Category? Parent { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}