using FamilyPhotos.ViewModel.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyPhotos.ViewModel
{
    public class PhotoViewModel
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
        [StringLength(40)]
        public string Title { get; set; }

        [Required] //Kötelező kitölteni a mezőt
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        /// <summary>
        /// Ez pedig csak a browserből történő file feltöltésre szolgál
        /// </summary>
        [FormFileLengthValidation] //Ez lefedi a [Required] attributumot is
        [ContentTypeValidation]
        [Display(Name = "Picture")]
        public IFormFile PictureFromBrowser { get; set; }
    }
}
