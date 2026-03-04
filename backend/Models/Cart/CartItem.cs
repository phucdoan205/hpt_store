using backend.Models.Base;
using backend.Models.Products;

namespace backend.Models.Cart
{
    public class CartItem : BaseModel
    {
        public Guid CartId { get; set; }
        public Cart Cart { get; set; }

        public Guid ProductVariantId { get; set; }
        public ProductVariant ProductVariant { get; set; }

        public int Quantity { get; set; }

        // // Snapshot để tránh sai lệch nếu giá thay đổi
        // public decimal UnitPrice { get; set; }
    }
}