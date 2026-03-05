using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using backend.DTOs.Content;
using backend.Models.Content;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/article-categories")]
    public class ArticleCategoriesController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public ArticleCategoriesController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _db.ArticleCategories.ToList();
            var result = _mapper.Map<List<ArticleCategoryDto>>(categories);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var category = _db.ArticleCategories.Find(id);
            if (category == null)
                return NotFound();

            var result = _mapper.Map<ArticleCategoryDto>(category);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(CreateArticleCategoryDto dto)
        {
            if (dto == null)
                return BadRequest();

            var category = _mapper.Map<ArticleCategory>(dto);
            _db.ArticleCategories.Add(category);
            _db.SaveChanges();

            var result = _mapper.Map<ArticleCategoryDto>(category);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, UpdateArticleCategoryDto dto)
        {
            if (dto == null)
                return BadRequest();

            var category = _db.ArticleCategories.Find(id);
            if (category == null)
                return NotFound();

            _mapper.Map(dto, category);
            _db.SaveChanges();

            var result = _mapper.Map<ArticleCategoryDto>(category);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var category = _db.ArticleCategories.Find(id);
            if (category == null)
                return NotFound();

            _db.ArticleCategories.Remove(category);
            _db.SaveChanges();

            return NoContent();
        }
    }
}