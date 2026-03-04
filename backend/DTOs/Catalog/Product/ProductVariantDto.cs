namespace backend.DTOs.Catalog.Product
{
    public class ProductVariantDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid ColorId { get; set; }
        public Guid SizeId { get; set; }
        public string Sku { get; set; }
        public int Quantity { get; set; }
        public decimal PriceModifier { get; set; }
    }
}
