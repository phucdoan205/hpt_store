using backend.Models.Base;

namespace backend.Models.Content
{
    public class Article : BaseModel
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Summary { get; set; }

        public string Content { get; set; }
        public string Thumbnail { get; set; }

        public Guid CategoryId { get; set; }
        public ArticleCategory Category { get; set; }

        public bool IsPublished { get; set; }
        public DateTime? PublishedAt { get; set; }
    }
}