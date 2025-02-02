using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSparks.Data.Models
{
    public class SparkTag
    {
        public Guid Id { get; set; }

        
        [ForeignKey("Tag")]
        public Guid TagId { get; set; }
        public required Tag Tag { get; set; }


        [ForeignKey("Spark")]
        public Guid SparkId { get; set; }
        public required Spark Spark { get; set; }
    }
}
