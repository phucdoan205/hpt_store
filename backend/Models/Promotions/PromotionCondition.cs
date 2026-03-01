using backend.Models.Base;

namespace backend.Models.Promotions
{
    public class PromotionCondition : BaseModel
    {
        public Guid PromotionId { get; set; }
        public Promotion Promotion { get; set; }

        public string Field { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }
    }
}