namespace CodeSparks.Data.Models
{
    public class Badge
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public Guid UserId { get; set; }
    }

}
