using CodeSparks.Data.Models;

namespace CodeSparks.ViewModels
{
    public class QuestViewModel
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public int XPReward { get; set; }
        public QuestStatus Status { get; set; }
        public double Rating { get; set; } // Fake rating
        public int Progress { get; set; } // Fake progress
    }

}
