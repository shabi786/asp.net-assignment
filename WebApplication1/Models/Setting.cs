namespace WebApplication1.Models
{
    public class Setting
    {
        public long Id { get; set; }
        public string? Key { get; set; }
        public string? Value { get; set; }
        public string? Value2 { get; set; }
        public string? Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastModified { get; set; }
        public bool IsDeleted { get; set; }
    }
}
