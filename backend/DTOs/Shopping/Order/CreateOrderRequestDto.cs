namespace backend.DTOs.Shopping.Order
{
    public class CreateOrderRequestDto
    {
        public string ShippingAddress { get; set; }

        public string? CouponCode { get; set; }

        public string PaymentMethod { get; set; }
        // COD, MOMO, ZALOPAY
    }
}