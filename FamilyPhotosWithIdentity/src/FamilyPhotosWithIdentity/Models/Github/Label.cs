using System.ComponentModel.DataAnnotations.Schema;

namespace FamilyPhotosWithIdentity.Models.Github
{
    public class Label : IEntityWithID
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public string url { get; set; }
        public string name { get; set; }
        public string color { get; set; }
        public bool _default { get; set; }
    }
}
