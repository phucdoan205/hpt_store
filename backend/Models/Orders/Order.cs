using backend.Models.Users;
using backend.Models.Base;

namespace backend.Models.Orders
{
    public class Order : BaseModel
    {
        public string OrderCode { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public DateTime OrderDate { get; set; }

        public string ShippingName { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingPhone { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal ShippingFee { get; set; }
        public decimal FinalAmount { get; set; }

        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public string Status { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}