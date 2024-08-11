using System.ComponentModel.DataAnnotations;

namespace CodeSparks.Data.Models
{
    public class Project
    {
        public Guid Id { get; set; }

        [MaxLength(60)]
        public required string Name { get; set; }

        [MaxLength(250)]
        public required string Description { get; set; }

        public string? Author { get; set; }

        [Url]
        [MaxLength(120)]
        public string? RepositoryUrl { get; set; }

        public virtual ICollection<Spark> Sparks { get; set; } = new List<Spark>();
    }
}
