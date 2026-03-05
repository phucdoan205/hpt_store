namespace backend.DTOs.Content
{
    public class ArticleCategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateArticleCategoryDto
    {
        public string Name { get; set; }
        public string Slug { get; set; }
    }

    public class UpdateArticleCategoryDto
    {
        public string Name { get; set; }
        public string Slug { get; set; }
    }
}