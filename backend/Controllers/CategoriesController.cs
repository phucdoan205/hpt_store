using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using backend.DTOs.Catalog.Category;
using backend.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public CategoriesController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var categories = _db.Categories
                    .Where(x => !x.IsDeleted)
                    .OrderBy(x => x.Name)
                    .ToList();

                var result = _mapper.Map<List<CategoryResponseDto>>(categories);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var category = _db.Categories.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
                if (category == null)
                    return NotFound(new { success = false, message = "Category not found" });

                var result = _mapper.Map<CategoryResponseDto>(category);
                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequestDto dto)
        {
            try
            {
                if (dto == null || string.IsNullOrWhiteSpace(dto.Name))
                    return BadRequest(new { success = false, message = "Category name is required" });

            
                var existingCategory = _db.Categories.FirstOrDefault(x => x.Name.ToLower() == dto.Name.ToLower() && !x.IsDeleted);
                if (existingCategory != null)
                    return BadRequest(new { success = false, message = "Category already exists" });

                var category = new Category
                {
                    Name = dto.Name,
                    Slug = GenerateSlug(dto.Name),
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                };

                _db.Categories.Add(category);
                await _db.SaveChangesAsync();

                var result = _mapper.Map<CategoryResponseDto>(category);
                return CreatedAtAction(nameof(GetById), new { id = category.Id }, new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateCategoryRequestDto dto)
        {
            try
            {
                if (dto == null || string.IsNullOrWhiteSpace(dto.Name))
                    return BadRequest(new { success = false, message = "Category name is required" });

                var category = _db.Categories.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
                if (category == null)
                    return NotFound(new { success = false, message = "Category not found" });

                var existingCategory = _db.Categories.FirstOrDefault(x => x.Name.ToLower() == dto.Name.ToLower() && x.Id != id && !x.IsDeleted);
                if (existingCategory != null)
                    return BadRequest(new { success = false, message = "Category name already exists" });

                category.Name = dto.Name;
                category.Slug = GenerateSlug(dto.Name);
                category.UpdatedAt = DateTime.UtcNow;

                _db.Categories.Update(category);
                await _db.SaveChangesAsync();

                var result = _mapper.Map<CategoryResponseDto>(category);
                return Ok(new { success = true, message = "Category updated successfully", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var category = _db.Categories.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
                if (category == null)
                    return NotFound(new { success = false, message = "Category not found" });

                
                var hasProducts = _db.Products.Any(x => x.CategoryId == id);
                if (hasProducts)
                    return BadRequest(new { success = false, message = "Cannot delete category that has products" });

                category.IsDeleted = true;
                category.UpdatedAt = DateTime.UtcNow;

                _db.Categories.Update(category);
                await _db.SaveChangesAsync();

                return Ok(new { success = true, message = "Category deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> Restore(int id)
        {
            try
            {
                var category = _db.Categories.FirstOrDefault(x => x.Id == id && x.IsDeleted);
                if (category == null)
                    return NotFound(new { success = false, message = "Deleted category not found" });

                category.IsDeleted = false;
                category.UpdatedAt = DateTime.UtcNow;

                _db.Categories.Update(category);
                await _db.SaveChangesAsync();

                var result = _mapper.Map<CategoryResponseDto>(category);
                return Ok(new { success = true, message = "Category restored successfully", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        private string GenerateSlug(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;

            return name.ToLower()
                .Replace(" ", "-")
                .Replace("&", "and")
                .Replace("'", "")
                .Replace("\"", "");
        }
    }
}
