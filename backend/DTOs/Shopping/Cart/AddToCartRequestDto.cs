namespace backend.DTOs.Shopping.Cart
{
    public class AddToCartRequestDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}