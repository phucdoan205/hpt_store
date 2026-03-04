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

            if (dto.UseColors || dto.UseSizes)
            {
                var allowedSizes = new[] { "S", "M", "L", "XL", "XXL", "XXXL" };

                List<backend.Models.Masters.MasterSize> sizes = new();
                List<backend.Models.Masters.MasterColor> colors = new();

                if (dto.UseSizes && dto.SizeNames != null)
                {
                    var normalized = dto.SizeNames.Select(s => s.Trim().ToUpper()).Distinct();
                    var valid = normalized.Where(s => allowedSizes.Contains(s));

                    sizes = await _db.MasterSizes
                        .Where(ms => valid.Contains(ms.Name.ToUpper()))
                        .ToListAsync();

                    foreach (var name in valid)
                    {
                        if (!sizes.Any(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase)))
                        {
                            var newSize = new backend.Models.Masters.MasterSize { Name = name };
                            _db.MasterSizes.Add(newSize);
                            sizes.Add(newSize);
                        }
                    }
                }

                if (dto.UseColors && dto.ColorIds != null)
                {
                    colors = await _db.MasterColors
                        .Where(c => dto.ColorIds.Contains(c.Id))
                        .ToListAsync();

                    if (colors.Count != dto.ColorIds.Length)
                    {
                        return BadRequest("One or more selected colors do not exist.");
                    }
                }

                await _db.SaveChangesAsync();

                if (dto.UseColors && !dto.UseSizes)
                {
                    var oneSize = await _db.MasterSizes.FirstOrDefaultAsync(ms => ms.Name == "ONE_SIZE");
                    if (oneSize == null)
                    {
                        oneSize = new backend.Models.Masters.MasterSize { Name = "ONE_SIZE" };
                        _db.MasterSizes.Add(oneSize);
                        await _db.SaveChangesAsync();
                    }
                    sizes = new List<backend.Models.Masters.MasterSize> { oneSize };
                }

                if (dto.UseSizes && !dto.UseColors)
                {
                    var defaultColor = await _db.MasterColors.FirstOrDefaultAsync(c => c.Name == "DEFAULT_COLOR");
                    if (defaultColor == null)
                    {
                        defaultColor = new backend.Models.Masters.MasterColor { Name = "DEFAULT_COLOR", HexCode = "#000000" };
                        _db.MasterColors.Add(defaultColor);
                        await _db.SaveChangesAsync();
                    }
                    colors = new List<backend.Models.Masters.MasterColor> { defaultColor };
                }

                foreach (var color in colors)
                {
                    foreach (var size in sizes)
                    {
                        var variant = new backend.Models.Products.ProductVariant
                        {
                            ProductId = product.Id,
                            ColorId = color.Id,
                            SizeId = size.Id,
                            Sku = $"{product.Slug}-{color.Name}-{size.Name}".Replace(" ", "-"),
                            Quantity = 0,
                            PriceModifier = 0m
                        };

                        _db.ProductVariants.Add(variant);
                    }
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
    }
}
