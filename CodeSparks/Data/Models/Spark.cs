using Microsoft.CodeAnalysis;

namespace CodeSparks.Data.Models
{
    public class Spark
    {
        public int Id { get; set; }
        public required string Name { get; set; }
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
