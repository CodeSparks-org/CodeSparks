using Microsoft.CodeAnalysis;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSparks.Data.Models
{
    public class Spark
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public required string Name { get; set; }
        
        [MaxLength(1000)]
        
        public required string Description { get; set; }
        
        [ScaffoldColumn(false)]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        
        public bool IsPublic { get; set; }
        
        public SparkCategory Category { get; set; }

        [ForeignKey("Project")]
        public Guid? ProjectId { get; set; }
        public virtual Project? Project { get; set; }
    
        [Url]
        [MaxLength(250)]
        public string? Url { get; set; }
        public virtual ICollection<SparkComment> Comments { get; set; } = new List<SparkComment>();
        public virtual ICollection<SparkUserStatus> UserStatuses { get; set; } = new List<SparkUserStatus>();
        public virtual ICollection<Hashtag> Hashtags { get; set; } = new List<Hashtag>();
    }

    public enum SparkCategory
    {
        Uncategorized = 0,
        Idea = 1,
        [Display(Name ="Coding")]
        Coding = 2,
        Learning = 3,
        Beginner = 4,
    }
}
