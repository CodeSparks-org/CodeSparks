using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSparks.Data.Models
{
    public class QuestProgress
    {
        public Guid Id { get; set; }
        public Guid QuestId { get; set; }
        [ForeignKey("QuestId")]
        public virtual Quest? Quest { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual AppUser? User { get; set; }
        public QuestStatus Status { get; set; }
        public DateTime RegisteredAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime? Deadline { get; set; }
    }

    public enum QuestStatus
    {
        InProgress = 0,
        Completed = 1,
        Registered = 2,
    }
}
