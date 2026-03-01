using backend.Models.Base;

namespace backend.Models.Orders
{
    public class OrderDetail : BaseModel
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public Guid ProductVariantId { get; set; }

        public string SnapshotProductName { get; set; }
        public string SnapshotThumbnail { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}