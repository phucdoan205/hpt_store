namespace backend.DTOs.Auth
{
    public class LoginRequestDto
    {
        public string EmailOrUsername { get; set; }
        public string Password { get; set; }
    }
}