using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSparks.Data.Models
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        
        public required string Title { get; set; }
        public required string Content { get; set; }


        public bool IsPublished { get; set; }
        public DateTime PublishedDate { get; set; }

        
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual AppUser? User { get; set; }

        public ICollection<Tag> Tags { get; set; } = [];
    }
}
