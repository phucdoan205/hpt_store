namespace backend.DTOs.Shopping.Order
{
    public class OrderDetailDto
    {
        public Guid Id { get; set; }
        public Guid ProductVariantId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
