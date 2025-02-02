using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSparks.Data.Models
{
    public class Tag
    {
        public Guid Id { get; set; }

        public required string Name { get; set; } = string.Empty;
    }
}