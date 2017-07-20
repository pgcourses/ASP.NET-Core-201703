using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyPhotosWithIdentity.Models.Github
{
    public class Hook : IEntityWithID
    {
        public int id { get; set; }

        public string type { get; set; }
        public string name { get; set; }
        public bool active { get; set; }
        public Config config { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime created_at { get; set; }
        public string url { get; set; }
        public string test_url { get; set; }
        public string ping_url { get; set; }
        public Last_Response last_response { get; set; }

        //Mivel az EntityFramework Code First nem tud ilyen listát kezelni, így 
        //ezt nem írjuk ki az adatbázisba, hanem készítünk egy mezőt helyette
        [NotMapped]
        public string[] events { get; set; }

        public string EventsInDb
        {
            get { return string.Join(",", events); }
            set { events = value.Split(','); }
        }

    }
}
