namespace backend.DTOs.Shopping.Cart
{
    public class CartResponseDto
    {
        public Guid CartId { get; set; }

        public List<CartItemResponseDto> Items { get; set; }

        public decimal GrandTotal { get; set; }
    }
}