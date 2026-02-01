namespace backend.DTOs.Orders
{
    public class CreateOrderDto
    {
        public required string ShippingName { get; set; }
        public required string ShippingPhone { get; set; }
        public required string ShippingAddress { get; set; }
        public required string PaymentMethod { get; set; }
    }

}
