using backend.Models.Base;
using backend.Models.Products;

namespace backend.Models.Promotions
{
    public class ProductPromotion : BaseModel
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid PromotionId { get; set; }
        public Promotion Promotion { get; set; }
    }
}