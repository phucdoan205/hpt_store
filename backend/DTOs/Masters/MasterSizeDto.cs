namespace backend.DTOs.Masters
{
    public class MasterSizeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateMasterSizeDto
    {
        public string Name { get; set; }
    }

    public class UpdateMasterSizeDto
    {
        public string Name { get; set; }
    }
}