public class Product
{
    public int Id { get; set; }

    public string Name { get; set; }
    public string Slug { get; set; }
    public string Description { get; set; }

    public decimal Price { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public string Thumbnail { get; set; }
    public bool IsActive { get; set; }

    public ICollection<ProductVariant> Variants { get; set; }
    public ICollection<ProductImage> Images { get; set; }
}
