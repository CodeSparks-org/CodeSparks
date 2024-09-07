namespace CodeSparks.Services
{
    public interface ISocialNetworkService
    {
        IEnumerable<SocialNetwork> GetSocialNetworks();
    }

    public class SocialNetworkService : ISocialNetworkService
    {
        public IEnumerable<SocialNetwork> GetSocialNetworks()
        {
            return
            [
                new() { Name = "Github", IconUrl = "/img/social/github.png"},
                new() { Name = "Linkedin", IconUrl = "/img/social/linkedin.png"}
            ];
        }
    }

    public class SocialNetwork
    {
        public required string Name { get; set; }
        public required string IconUrl { get; set; }
    }
}