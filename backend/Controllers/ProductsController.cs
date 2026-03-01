using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using backend.DTOs.Products;
using backend.Models.Products;
using backend.Services;

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

            var result = _mapper.Map<List<ProductDto>>(products);

            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateProductDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data");

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

            /* ========= THUMBNAIL ========= */

            if (dto.ThumbnailFile != null)
            {
                var thumbUrl = await _storage.UploadFile(dto.ThumbnailFile);
                product.Thumbnail = thumbUrl;
            }

            /* ========= MULTIPLE IMAGES ========= */

            if (dto.ImageFiles != null && dto.ImageFiles.Any())
            {
                int order = 0;

                foreach (var file in dto.ImageFiles)
                {
                    var imageUrl = await _storage.UploadFile(file);

                    product.Images.Add(new ProductImage
                    {
                        ImageUrl = imageUrl,
                        SortOrder = order++
                    });
                }
            }

            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            return Ok(product);
        }
    }
}