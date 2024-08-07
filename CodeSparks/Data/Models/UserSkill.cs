using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSparks.Data.Models
{
    public class UserSkill
    {
        public Guid Id { get; set; }
        
        public Guid SkillId { get; set; }
        [ForeignKey("SkillId")]
        public virtual Skill? Skill { get; set; }
        
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual AppUser? User { get; set; }
        
        public int SkillLevel { get; set; }
    }
}
