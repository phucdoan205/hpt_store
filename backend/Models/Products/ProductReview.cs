using backend.Models.Base;
using backend.Models.Users;
using backend.Models.Orders;


namespace backend.Models.Products
{
    public class ProductReview : BaseModel
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}