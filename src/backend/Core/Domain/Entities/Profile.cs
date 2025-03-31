namespace Backend.Core.Domain.Entities
{
    public class Profile
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Permissions { get; set; } = string.Empty; // JSON
    }
} 