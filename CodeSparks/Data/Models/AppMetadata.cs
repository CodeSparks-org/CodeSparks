using System.ComponentModel.DataAnnotations;

namespace CodeSparks.Data.Models
{
    public class AppMetadata
    {
        public Guid Id { get; set; }

        [MaxLength(255)]
        public string? Name { get; set; }
        [MaxLength(255)]
        public string? Value { get; set; }
        [MaxLength(255)]
        public string? Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public MetadataStatus? Status { get; set; }

        public AppMetadata()
        {
            CreatedAt = DateTime.UtcNow.ToUniversalTime();
        }
    }

    public enum MetadataStatus
    {
        Ok = 0,
        Hidden = 1,
    }
}