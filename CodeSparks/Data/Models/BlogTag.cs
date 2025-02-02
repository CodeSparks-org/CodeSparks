using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSparks.Data.Models
{
    public class BlogTag
    {
        public Guid Id { get; set; }


        [ForeignKey("Tag")]
        public Guid TagId { get; set; }
        public required Tag Tag { get; set; }


        [ForeignKey("BlogPost")]
        public Guid BlogPostId { get; set; }
        public required BlogPost BlogPost { get; set; }
    }
}