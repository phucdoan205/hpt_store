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
                IsActive = true,
                Thumbnail = "",
                Images = new List<ProductImage>()
            };

            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            var folder = $"product-{product.Id}";

            if (dto.ThumbnailFile != null)
            {
                product.Thumbnail =
                    await _storage.UploadFile(dto.ThumbnailFile, folder);
            }

            if (dto.ImageFiles != null)
            {
                int order = 0;

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
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _db.Products
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            await _storage.DeleteFolder($"product-{id}");

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();

            return Ok("Deleted");
        }
    }
}
