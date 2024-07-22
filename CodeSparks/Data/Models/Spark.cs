using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSparks.Data.Models
{
    public class Spark
    {
        public long Id { get; set; }

        [MaxLength(100)]
        public required string Name { get; set; }
        
        [MaxLength(1000)]
        
        public required string Description { get; set; }
        
        public SparkStatus Status { get; set; }
        
        [ScaffoldColumn(false)]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        
        public bool IsPublic { get; set; }
        
        public SparkCategory Category { get; set; }

        [ForeignKey("Project")]
        public long? ProjectId { get; set; }
        public virtual Project? Project { get; set; }

        [Url]
        [MaxLength(100)]
        public string? Url { get; set; }
    }

    public enum SparkStatus
    {
        NotStarted = 0,
        InProgress = 1,
        Completed = 2,
    }

    public enum SparkCategory
    {
        Uncategorized = 0,
        Idea = 1,
        Coding = 2,
        Learning = 3,
        Beginner = 4,
    }
}
