using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using backend.DTOs.Content;
using backend.Models.Content;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/articles")]
    public class ArticlesController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public ArticlesController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var articles = _db.Articles
                .Include(a => a.Category)
                .ToList();

            var result = _mapper.Map<List<ArticleDto>>(articles);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var article = _db.Articles
                .Include(a => a.Category)
                .FirstOrDefault(a => a.Id == id);

            if (article == null)
                return NotFound();

            var result = _mapper.Map<ArticleDto>(article);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(CreateArticleDto dto)
        {
            if (dto == null)
                return BadRequest();

            var article = _mapper.Map<Article>(dto);
            _db.Articles.Add(article);
            _db.SaveChanges();

            var result = _mapper.Map<ArticleDto>(article);
            return CreatedAtAction(nameof(GetById), new { id = article.Id }, result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, UpdateArticleDto dto)
        {
            if (dto == null)
                return BadRequest();

            var article = _db.Articles.Find(id);
            if (article == null)
                return NotFound();

            _mapper.Map(dto, article);
            _db.SaveChanges();

            var result = _mapper.Map<ArticleDto>(article);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var article = _db.Articles.Find(id);
            if (article == null)
                return NotFound();

            _db.Articles.Remove(article);
            _db.SaveChanges();

            return NoContent();
        }
    }
}