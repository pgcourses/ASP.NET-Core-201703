using FamilyPhotos.ViewModel.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyPhotos.Models
{
    public class PhotoModel
    {
        public int Id { get; set; }

        /// <summary>
        /// Beépített validációk
        /// 
        /// Required
        /// Compare
        /// EmailAddress
        /// Phone
        /// Range
        /// StringLength
        /// Url
        /// RegularExpression
        /// 
        /// + saját validálás készítése
        /// </summary>

        [Required] //Kötelező kitölteni a mezőt
        public string Title { get; set; }

        [Required] //Kötelező kitölteni a mezőt
        public string Description { get; set; }

        /// <summary>
        /// Ezt fogjuk majd adatbázisba menteni
        /// </summary>
        public byte[] Picture { get; set; }

        /// <summary>
        /// Ez pedig csak a browserből történő file feltöltésre szolgál
        /// </summary>
        [FormFileLengthValidation] //Ez lefedi a [Required] attributumot is
        [ContentTypeValidation]
        public IFormFile PictureFromBrowser { get; set; }

        //MVC5/ASP.NET 4.6-ban nincs IFormFile, helyette ez van
        //public HttpPostedFileWrapper PictureFromBrowser { get; set; }

        public string ContentType { get; set; }
    }
}
