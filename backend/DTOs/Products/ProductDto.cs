namespace backend.DTOs.Products
{
    public class ProductDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public decimal Price { get; set; }
        public required string Thumbnail { get; set; }
        public string Description { get; set; }
    }

}
