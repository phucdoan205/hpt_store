using backend.Models.Masters;
using backend.Models.Base;

namespace backend.Models.Products
{
    public class ProductVariant : BaseModel
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid ColorId { get; set; }
        public MasterColor Color { get; set; }

        public Guid SizeId { get; set; }
        public MasterSize Size { get; set; }

        public string Sku { get; set; }

        public int Quantity { get; set; }
        public decimal PriceModifier { get; set; }
    }
}