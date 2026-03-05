using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using backend.DTOs.Products;
using backend.DTOs.Products.Variants;
using backend.Models.Products;
using OfficeOpenXml;
using EFCore.BulkExtensions;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/products/[action]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;


        public ProductsController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            // _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [HttpGet]
        public IActionResult GetAllActiveProducts()
        {
            try
            {
                var products = _db.Products.Where(p => p.IsActive).ToList();
                var result = _mapper.Map<List<ProductDto>>(products);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message.ToString());
            }
        }

        [HttpGet("{slug}")]
        public IActionResult GetProductBySlug(String slug)
        {
            // Vào db lấy ra danh sách toàn bộ sản phẩm, sau đó lọc ra 1 sản phẩm duy nhất theo tiêu chí: 
            var product = _db.Products.SingleOrDefault(p => p.Slug == slug);
            if (product != null && product.IsActive)
            {
                var result = _mapper.Map<ProductDto>(product);
                return Ok(result);
            }
            else if (product != null && !product.IsActive)
            {
                return Ok
                ("SP da ngung kinh doanh");
            }
            else
            {
                return Ok("Sp kh ton tai");
            }

        }
        [HttpGet("{ID}")]
        public IActionResult GetProductById(int ID)
        {
            var product = _db.Products.SingleOrDefault(p => p.Id == ID);
            if (product != null && product.IsActive)
            {
                var result = _mapper.Map<ProductDto>(product);
                return Ok(result);
            }
            else if (product != null && !product.IsActive)
            {
                return Ok("SP da tam ngung ban");
            }
            else
            {
                return Ok("SP kh ton tai");
            }
        }

        [HttpPost]
        public IActionResult AddProduct(CreateProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            product.IsActive = true;
            _db.Products.Add(product);
            _db.SaveChanges();
            return Ok(true);
        }

        [HttpPost]
        public IActionResult UpdateProduct(UpdateProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            var updateProduct = _db.Products.SingleOrDefault(row => row.Id == product.Id); // lấy sản phẩm cần sửa ra
            if (updateProduct != null) // Nếu sản phẩm có tồn tại trong database
            {
                _mapper.Map(dto, updateProduct);
                _db.SaveChanges();
                return Ok(true);
            }
            else
            {
                return Ok("Sản phẩm không tồn tại");
            }
        }

        [HttpGet]
        public IActionResult DownloadProductTemplate()
        {
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Products &  Variants");

            // PRODUCT HEADERS
            // [dòng, cột]
            worksheet.Cells[1, 1].Value = "ProductName";
            worksheet.Cells[1, 2].Value = "Slug";
            worksheet.Cells[1, 3].Value = "Description";
            worksheet.Cells[1, 4].Value = "BasePrice";
            worksheet.Cells[1, 5].Value = "CategoryId";
            worksheet.Cells[1, 6].Value = "Thumbnail";

            // VARIANT HEADERS
            worksheet.Cells[1, 7].Value = "ColorId";
            worksheet.Cells[1, 8].Value = "SizeId";
            worksheet.Cells[1, 9].Value = "Sku";
            worksheet.Cells[1, 10].Value = "Quantity";
            worksheet.Cells[1, 11].Value = "PriceModifier";

            //EXAMPLES
            worksheet.Cells[2, 1].Value = "Áo thun nam";
            worksheet.Cells[2, 2].Value = "ao-thun-nam";
            worksheet.Cells[2, 3].Value = "Áo thun nam chuẩn hàng Auth";
            worksheet.Cells[2, 4].Value = "150000";
            worksheet.Cells[2, 5].Value = "5";
            worksheet.Cells[2, 6].Value = "abc";

            worksheet.Cells[2, 7].Value = "1";
            worksheet.Cells[2, 8].Value = "1";
            worksheet.Cells[2, 9].Value = "12345";
            worksheet.Cells[2, 10].Value = "10";
            worksheet.Cells[2, 11].Value = "10000";

            //Format header
            using (var range = worksheet.Cells[1, 1, 1, 11])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
            }

            worksheet.Cells.AutoFitColumns();

            var stream = new MemoryStream();// Tạo luồng xử lý trên RAM
            package.SaveAs(stream);
            stream.Position = 0;

            string fileName = "Product-Import-Template.xlsx";
            // content-type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        [HttpPost]
        public async Task<IActionResult> ImportProductsFromExcel(IFormFile file)
        {
            // Kiểm tra có file được gửi vào API hay không
            if (file == null || file.Length == 0)
            {
                return BadRequest("Không có file được chọn");
            }

            // Kiểm tra file có đúng định dạng là .xlsx (Excel) hay không
            if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Chỉ hỗ trợ file .xlsx");
            }

            // Tạo dictionary để gom nhóm các product và variant lại. Gom nhóm bằng cách: key = Slug (unique), value = Product+ list Variants
            var productDict = new Dictionary<string, (Product Product, List<ProductVariant> Variants)>();

            // Tạo stream trên RAM để chuẩn bị xử lý file Excel
            using var stream = new MemoryStream();
            // Nạp file Excel vào stream
            await file.CopyToAsync(stream);
            stream.Position = 0;

            // Tạo một instance để xử lý file Excel
            using var package = new ExcelPackage(stream);
            // Tạo biến worksheet, gán sheet đầu tiên của file Excel vào biến
            var worksheet = package.Workbook.Worksheets.FirstOrDefault();

            // Kiểm tra biến worksheet có null hay không. Nếu là null thì không có sheet nào trong file Excel Nghĩa là file Excel bị lỗi.
            if (worksheet == null)
            {
                return BadRequest("File Excel không hợp lệ");
            }

            // Tạo biến rowCount, gán số lượng dòng trong worksheet vào biến
            int rowCount = worksheet.Dimension.Rows;
            // Tạo biến currentProduct để theo dỗi product hiện tại nếu dòng có dòng variant thêm
            Product currentProduct = null;

            // Bắt đầu đọc từ dòng thứ 2 của worksheet. Dòng 1 là tiêu đề nên không đọc
            for (int row = 2; row <= rowCount; row++)
            {
                // Tạo biến productName, lấy từ cột ProductName (cột 1), Trim() để loại bỏ khoảng trắng ở 2 đầu 
                string productName = worksheet.Cells[row, 1].GetValue<string>()?.Trim();
                string slug = worksheet.Cells[row, 2].GetValue<string>()?.Trim();

                // Kiểm tra productName và slug có null hoặc rỗng hay không. Nếu null hoặc rỗng thì không tạo sản phẩm mà chỉ tạo variant
                if (!string.IsNullOrEmpty(productName) && !string.IsNullOrEmpty(slug))
                {
                    if (productDict.ContainsKey(slug))
                    {
                        continue;
                    }

                    var product = new Product
                    {
                        Name = productName,
                        Slug = worksheet.Cells[row, 2].GetValue<string>(),
                        Description = worksheet.Cells[row, 3].GetValue<string>(),
                        Price = worksheet.Cells[row, 4].GetValue<int>(),
                        CategoryId = worksheet.Cells[row, 5].GetValue<int>(),
                        Thumbnail = worksheet.Cells[row, 6].GetValue<string>(),
                        IsActive = true
                    };

                    var variants = new List<ProductVariant>();
                    productDict[slug] = (product, variants);
                    currentProduct = product;
                }
                else if (currentProduct == null)
                {
                    continue;
                }

                var variant = new ProductVariant
                {
                    ColorId = worksheet.Cells[row, 7].GetValue<int?>() ?? 0,
                    SizeId = worksheet.Cells[row, 8].GetValue<int?>() ?? 0,
                    Sku = worksheet.Cells[row, 9].GetValue<string>()?.Trim(),
                    Quantity = worksheet.Cells[row, 10].GetValue<int>(),
                    PriceModifier = worksheet.Cells[row, 11].GetValue<int>()
                };
                if (currentProduct != null)
                {
                    var (_, variants) = productDict[currentProduct.Slug];
                    variants.Add(variant);
                }
            }

            var productsToInsert = productDict.Values.Select(p => p.Product).ToList();
            var allVariants = new List<ProductVariant>();

            foreach (var (product, variants) in productDict.Values)
            {
                foreach (var variant in variants)
                {
                    variant.Product = product;
                    allVariants.Add(variant);
                }
            }

            // Thêm danh sách products và danh sách vào variants vào database
            // Dùng transaction để thực hiện. Nếu xảy ra lỗi trong lúc thêm có thể dùng transaction để hoàn tác
            await using var transaction = await _db.Database.BeginTransactionAsync();

            try
            {
                // Hiện product không có ID. Thêm product vào database trước. EF sẽ tự động gán ID cho product
                await _db.BulkInsertAsync(productsToInsert, new BulkConfig
                {
                    SetOutputIdentity = true,
                    PreserveInsertOrder = true
                });

                // Sau khi thêm thì product đã có Id, gán Id đó vào productId của variant

                foreach (var variant in allVariants)
                {
                    if (variant.Product == null || variant.Product.Id <= 0)
                    {
                        throw new Exception("Product Id chưa được sinh ra cho variant");
                    }
                    variant.ProductId = variant.Product.Id;
                    variant.Product = null;
                }

                // Thêm variant vào database
                await _db.BulkInsertAsync(allVariants);

                // Xác nhận đã thực hiện transaction
                await transaction.CommitAsync();
                return Ok(new { Message = $"Đã import thành công {productsToInsert.Count} sản phẩm và {allVariants.Count} variant." });
            }
            catch (Exception ex)
            {
                // Hoàn tác transaction nếu có lỗi xảy ra
                await transaction.RollbackAsync();
                return StatusCode(500, $"Lỗi import: {ex.Message}\nStack: {ex.StackTrace}");
            }
        }

        // PRODUCT VARIANTS

        [HttpGet("{productId}")]
        public IActionResult GetVariantsByProductId(int productId)
        {
            try
            {
                var variants = _db.ProductVariants.Where(p => p.ProductId == productId).ToList();
                if (variants != null)
                {
                    var dto = _mapper.Map<List<ProductVariantDto>>(variants);
                    return Ok(dto);
                }
                else
                {
                    return Ok("Không tìm thấy.");
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.ToString());
            }
        }

        [HttpGet("{productSlug}")]
        public IActionResult GetVariantsByProductSlug(string productSlug)
        {
            var variants = _db.ProductVariants.Where(v => v.Product.Slug == productSlug).ToList();
            if (variants != null)
            {
                var result = _mapper.Map<ProductVariantDto>(variants);
                return Ok(result);
            }
            else
            {
                return Ok("Sản phẩm không tồn tại");
            }
        }

        [HttpPost]
        public IActionResult AddProductVariant(CreateProductVariantDto dto)
        {
            try
            {
                // _mapper.ConfigurationProvider.AssertConfigurationIsValid();
                var variant = _mapper.Map<ProductVariant>(dto);
                _db.ProductVariants.Add(variant);
                _db.SaveChanges();
                return Ok(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Ok(ex.Message.ToString());
            }
        }

        [HttpPost]
        public IActionResult UpdateProductVariant(UpdateProductVariantDto dto)
        {
            try
            {
                var updateVariant = _db.ProductVariants.SingleOrDefault(v => v.ProductId == dto.ProductId && v.Id == dto.Id);
                if (updateVariant != null)
                {
                    _mapper.Map(dto, updateVariant);
                    _db.SaveChanges();

                    return Ok(true);
                }
                return NotFound("Không tìm thấy");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
