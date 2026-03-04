using backend.Models.Base;
using backend.Models.Users;

namespace backend.Models.Cart
{
    public class Cart : BaseModel
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public ICollection<CartItem> Items { get; set; }
    }
}