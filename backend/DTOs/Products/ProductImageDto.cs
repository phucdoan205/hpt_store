namespace backend.DTOs.Products
{
    public class ProductImageDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public required string ImageUrl { get; set; }
        public int SortOrder { get; set; }
    }
}
