using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyPhotos.Models
{
    public class PhotoModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Ezt fogjuk majd adatbázisba menteni
        /// </summary>
        public byte[] Picture { get; set; }

        /// <summary>
        /// Ez pedig csak a browserből történő file feltöltésre szolgál
        /// </summary>
        public IFormFile PictureFromBrowser { get; set; }

        //MVC5/ASP.NET 4.6-ban nincs IFormFile, helyette ez van
        //public HttpPostedFileWrapper PictureFromBrowser { get; set; }

        public string ContentType { get; set; }
    }
}
