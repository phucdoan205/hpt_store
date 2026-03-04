using backend.Models.Base;

namespace backend.Models.Content
{
    public class ArticleCategory : BaseModel
    {
        public string Name { get; set; }
        public string Slug { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}