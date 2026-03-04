using backend.Models.Base;

namespace backend.Models.Products
{
    public class ProductImage : BaseModel
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public string ImageUrl { get; set; }
        public int SortOrder { get; set; }
    }
}