using backend.Models.Base;

namespace backend.Models.Orders
{
    public class OrderStatusHistory : BaseModel
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public string PreviousStatus { get; set; }
        public string NewStatus { get; set; }

        public string? Note { get; set; }
        public Guid? UpdatedBy { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}