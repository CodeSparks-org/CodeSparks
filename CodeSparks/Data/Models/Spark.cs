using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

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
        public DateTime CreatedDate { get; set; }
        public bool IsPublic { get; set; }
        public SparkCategory Category { get; set; }
    }

    public enum SparkStatus
    {
        NotStarted,
        InProgress,
        Completed
    }

    public enum SparkCategory
    {
        Uncategorized = 0,
        Idea = 1,
        CodeSparksTask = 2,
        Learning = 3,
    }
}
