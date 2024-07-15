using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace CodeSparks.Data.Models
{
    public class Spark
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public required string Name { get; set; }
        [MaxLength(1000)]
        public required string Description { get; set; }
        public SparkStatus Status { get; set; } // Enum for spark status
        public DateTime CreatedDate { get; set; }
    }

    public enum SparkStatus
    {
        NotStarted,
        InProgress,
        Completed

        //TODO move to database or at least extend and add numbers for future changes and db state
    }
}
