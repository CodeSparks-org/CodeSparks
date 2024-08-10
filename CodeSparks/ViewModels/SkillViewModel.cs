namespace CodeSparks.ViewModels
{
    public class SkillViewModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public double Rating { get; set; } // Fake rating
        public int Progress { get; set; } // Fake progress
        public List<QuestViewModel> RelatedQuests { get; set; } = new List<QuestViewModel>();
        public List<SkillViewModel> SuggestedSkills { get; set; } = new List<SkillViewModel>();
    }
}
