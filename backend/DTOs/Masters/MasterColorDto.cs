namespace backend.DTOs.Masters
{
    public class MasterColorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string HexCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateMasterColorDto
    {
        public string Name { get; set; }
        public string HexCode { get; set; }
    }

    public class UpdateMasterColorDto
    {
        public string Name { get; set; }
        public string HexCode { get; set; }
    }
}