namespace backend.Models.Orders
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductVariantId { get; set; }
        public int ProductId { get; set; }

        public required string SnapshotProductName { get; set; }
        public required string SnapshotSku { get; set; }
        public required string SnapshotThumbnail { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}