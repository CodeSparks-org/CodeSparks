using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSparks.Data.Models
{
    public class SparkUserStatus
    {
        public Guid Id { get; set; }


        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual AppUser? User { get; set; }


        [ForeignKey("Spark")]
        public Guid SparkId { get; set; }
        public virtual Spark? Spark { get; set; }


        public SparkStatus Status { get; set; } = SparkStatus.NotStarted;

        public DateTime? StartedDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        public DateTime? ReviewDate { get; set; }

        
        [ForeignKey("ReviewedBy")]
        public Guid? ReviewedById { get; set; }
        public virtual AppUser? ReviewedBy { get; set; }
    }

    public enum SparkStatus
    {
        NotStarted = 0,
        InProgress = 1,
        InReview = 2,
        Completed = 3,
        Failed = 4,
    }
}
