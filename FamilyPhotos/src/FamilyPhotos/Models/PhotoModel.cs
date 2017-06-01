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
        /// Beépített validációk (Data Validation)
        /// 
        /// Required
        /// StringLength
        /// 
        /// + saját validálás készítése
        /// </summary>

        [Required] //Kötelező kitölteni a mezőt
        [StringLength(40)]
        public string Title { get; set; }

        [Required] //Kötelező kitölteni a mezőt
        public string Description { get; set; }

        /// <summary>
        /// Ezt fogjuk majd adatbázisba menteni
        /// </summary>
        public byte[] Picture { get; set; }

        [StringLength(255)]
        public string ContentType { get; set; }
    }
}
