public class OrderDetail
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; }

    public int ProductVariantId { get; set; }

    public string SnapshotProductName { get; set; }
    public string SnapshotSku { get; set; }
    public string SnapshotThumbnail { get; set; }

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
