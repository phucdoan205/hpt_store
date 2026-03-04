using backend.Models.Base;

namespace backend.Models.Users
{
    public class UserAddress : BaseModel
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public string ContactName { get; set; }
        public string ContactPhone { get; set; }

        public string AddressLine { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }

        public bool IsDefault { get; set; }
    }
}