using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSparks.Data.Models
{
    public class Skill
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        
        public long OwnerUserId { get; set; }
        [ForeignKey("OwnerUserId")]
        public virtual AppUser? Owner { get; set; }
    }
}
