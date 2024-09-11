using CodeSparks.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace CodeSparks.ViewModels
{
    public class SparkInput
    {
        [MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(1000)]
        public required string Description { get; set; }

        public bool IsPublic { get; set; }

        public SparkCategory Category { get; set; }

        [Url]
        [MaxLength(250)]
        public string? Url { get; set; }

        public string? HashtagList { get; set; }
    }
}
