namespace backend.DTOs.Shopping.Order
{
    public class OrderResponseDto
    {
        public Guid OrderId { get; set; }

        public string OrderStatus { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}