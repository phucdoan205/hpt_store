using backend.Models.Base;
using backend.Models.Products;

namespace backend.Models.Masters
{
    public class MasterSize : BaseModel
    {
        public string Name { get; set; }

        public ICollection<ProductVariant> Variants { get; set; }
    }
}