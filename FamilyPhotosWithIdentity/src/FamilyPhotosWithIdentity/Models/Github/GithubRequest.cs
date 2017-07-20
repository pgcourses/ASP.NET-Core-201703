namespace FamilyPhotosWithIdentity.Models.Github
{
    public class GithubRequest : IEntityWithID
    {
        public int id { get; set; }

        public string zen { get; set; }
        public string action { get; set; }
        public int hook_id { get; set; }
        public Hook hook { get; set; }
        public Issue issue { get; set; }
        public Repository repository { get; set; }
        public Organization organization { get; set; }
        public User sender { get; set; }
    }
}
