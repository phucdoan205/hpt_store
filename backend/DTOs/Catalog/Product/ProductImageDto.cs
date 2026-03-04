namespace backend.DTOs.Catalog.Product
{
    public class ProductImageDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ImageUrl { get; set; }
        public int SortOrder { get; set; }
    }
}
