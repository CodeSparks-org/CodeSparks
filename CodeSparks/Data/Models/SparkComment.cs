using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSparks.Data.Models
{
    public class SparkComment
    {
        public Guid Id { get; set; }
        public required string Text { get; set; }
        public DateTime DatePosted { get; set; }


        [ForeignKey("User")]
        public Guid? UserId { get; set; } // if null - comment is anonymous
        public AppUser? User { get; set; }


        [ForeignKey("Spark")]
        public Guid SparkId { get; set; }
        public virtual Spark? Spark { get; set; }
    }
}
