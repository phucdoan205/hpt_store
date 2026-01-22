public class Order
{
    public int Id { get; set; }
    public string OrderCode { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public DateTime OrderDate { get; set; }

    public string ShippingName { get; set; }
    public string ShippingAddress { get; set; }
    public string ShippingPhone { get; set; }

    public decimal TotalAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public string CouponCode { get; set; }

    public decimal ShippingFee { get; set; }
    public decimal FinalAmount { get; set; }

    public string PaymentMethod { get; set; }
    public string PaymentStatus { get; set; }

    public string Status { get; set; }

    public ICollection<OrderDetail> OrderDetails { get; set; }
}
