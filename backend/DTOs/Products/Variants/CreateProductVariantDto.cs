namespace backend.DTOs.Products.Variants
{
    public class CreateProductVariantDto
    {
        public required int ProductId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public required string Sku { get; set; }
        public int Quantity { get; set; }
        public decimal PriceModifier { get; set; }
    }
}