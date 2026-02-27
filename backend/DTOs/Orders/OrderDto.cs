namespace backend.DTOs.Orders
{
    public class OrderDto
    {
        public int Id { get; set; }
        public required string OrderCode { get; set; }
        public decimal FinalAmount { get; set; }
        public required string Status { get; set; }
        public DateTime OrderDate { get; set; }

        public List<OrderDetailDto> Items { get; set; }
    }
}
