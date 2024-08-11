using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSparks.Data.Models
{
    public class Quest
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public int XPReward { get; set; }
        public Guid OwnerUserId { get; set; }
        [ForeignKey("OwnerUserId")]
        public virtual AppUser? Owner { get; set; }
    }

}
