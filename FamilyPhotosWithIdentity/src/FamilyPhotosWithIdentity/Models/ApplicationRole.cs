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
        public ApplicationRole() : base()
        {
            Init();
        }

        public ApplicationRole(string roleName) : base(roleName)
        {
            Init();
        }

        private void Init()
        {
            UrlCode = Guid.NewGuid().ToString();
        }

        [StringLength(100)]
        public string UrlCode { get; set; }
    }
}
