using backend.Models.Users;

namespace backend.Models.Orders
{
    public class Order
    {
        public int Id { get; set; }
        public required string OrderCode { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime OrderDate { get; set; }

        public required string ShippingName { get; set; }
        public required string ShippingAddress { get; set; }
        public required string ShippingPhone { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public required string CouponCode { get; set; }

        public decimal ShippingFee { get; set; }
        public decimal FinalAmount { get; set; }

        public required string PaymentMethod { get; set; }
        public required string PaymentStatus { get; set; }

        public required string Status { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}