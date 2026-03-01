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
            string thumbnailUrl = "";

            if (dto.ThumbnailFile != null)
            {
                thumbnailUrl =
                    await _storage.UploadFile(dto.ThumbnailFile);
            }

            var product = new Product
            {
                Name = dto.Name,
                Slug = dto.Slug,
                Description = dto.Description,
                Price = dto.Price,
                CategoryId = dto.CategoryId,
                Thumbnail = thumbnailUrl,
                IsActive = true
            };

            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            return Ok(product);
        }
    }
}