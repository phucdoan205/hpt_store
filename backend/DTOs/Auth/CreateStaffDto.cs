public class CreateStaffDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }

    public bool IsLocked { get; set; }
}
