using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public ProductsController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
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
        public IActionResult Create(CreateProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            product.IsActive = true;

            _db.Products.Add(product);
            _db.SaveChanges();

            return Ok(product);
        }
    }
}
