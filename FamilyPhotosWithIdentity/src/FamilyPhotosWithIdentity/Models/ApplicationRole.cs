using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyPhotosWithIdentity.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {
            UrlCode = Guid.NewGuid().ToString();
        }

        [StringLength(100)]
        public string UrlCode { get; set; }
    }
}
