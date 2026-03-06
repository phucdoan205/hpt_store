using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using backend.DTOs.Products;
using backend.DTOs.Catalog.Product;
using backend.Models.Products;
using backend.Services;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private readonly SupabaseStorageService _storage;

        public ProductsController(
            AppDbContext db,
            IMapper mapper,
            SupabaseStorageService storage)
        {
            _db = db;
            _mapper = mapper;
            _storage = storage;
        }

       [HttpGet]
      public IActionResult GetAll()
        {
            var products = _db.Products
                .Where(x => x.IsActive)
                 .ToList();

          var result = _mapper.Map<List<ProductResponseDto>>(products);

            return Ok(result);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CreateProductDto dto)
        {
            if (dto == null)
                return BadRequest();

            var product = new Product
            {
                Name = dto.Name,
                Slug = dto.Slug,
                Description = dto.Description,
                Price = dto.Price,
                CategoryId = dto.CategoryId,
                IsActive = dto.IsActive ?? true,
                Thumbnail = "",
                Images = new List<ProductImage>()
            };

            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            var folder = $"product-{product.Id}";

            if (dto.ThumbnailFiles != null && dto.ThumbnailFiles.Length > 0)
            {
                int torder = 0;
                foreach (var file in dto.ThumbnailFiles)
                {
                    var url = await _storage.UploadFile(file, folder);

                    if (torder == 0)
                        product.Thumbnail = url;

                    product.Images.Add(new ProductImage
                    {
                        ProductId = product.Id,
                        ImageUrl = url,
                        SortOrder = torder++
                    });
                }
            }

            if (dto.ImageFiles != null)
            {
                int order = product.Images.Count;
                foreach (var file in dto.ImageFiles)
                {
                    var url = await _storage.UploadFile(file, folder);

                    product.Images.Add(new ProductImage
                    {
                        ProductId = product.Id,
                        ImageUrl = url,
                        SortOrder = order++
                    });
                }
            }


            await _db.SaveChangesAsync();

            return Ok(product);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, [FromQuery] bool hard = false)
        {
            var product = await _db.Products
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            if (hard)
            {
                await _storage.DeleteFolder($"product-{id}");

                _db.Products.Remove(product);
                await _db.SaveChangesAsync();

                return Ok("Deleted");
            }

            product.IsActive = false;
            await _db.SaveChangesAsync();

            return Ok("SoftDeleted");
        }

        [HttpPatch("{id:guid}/restore")]
        public async Task<IActionResult> Restore(Guid id)
        {
            try
            {
                var product = await _db.Products
                    .FirstOrDefaultAsync(p => p.Id == id && !p.IsActive);

                if (product == null)
                    return NotFound(new { success = false, message = "Deleted product not found" });

                product.IsActive = true;
                product.UpdatedAt = DateTime.UtcNow;

                _db.Products.Update(product);
                await _db.SaveChangesAsync();

                var result = _mapper.Map<ProductResponseDto>(product);
                return Ok(new { success = true, message = "Product restored successfully", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
    }
}
