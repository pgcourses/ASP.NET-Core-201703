using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyPhotosWithIdentity.Models.RoleViewModels
{
    public class RoleViewModel
    {
        /// <summary>
        /// Speciális azonosító, csak erre az esetre, 
        /// ez jelenik meg a weboldalon, és az, ami a felhasználó valódi azonosítója, az nem.
        /// </summary>
        public string UrlCode { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
