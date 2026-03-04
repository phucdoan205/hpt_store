using backend.Models.Base;

namespace backend.Models.Users
{
    public class Notification : BaseModel
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public string Title { get; set; }
        public string Message { get; set; }

        public string Type { get; set; }
        public bool IsRead { get; set; }
    }
}