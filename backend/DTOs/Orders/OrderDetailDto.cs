namespace backend.DTOs.Orders
{
    public class OrderDetailDto
    {
        public int ProductVariantId { get; set; }
        public required string SnapshotProductName { get; set; }
        public required string SnapshotSku { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
