using backend.Models.Base;
using backend.Models.Products;

namespace backend.Models.Masters
{
    public class MasterColor : BaseModel
    {
        public string Name { get; set; }
        public string HexCode { get; set; }

        public ICollection<ProductVariant> Variants { get; set; }
    }
}
