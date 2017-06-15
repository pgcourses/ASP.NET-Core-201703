using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using FamilyPhotosWithIdentity.Models;

namespace FamilyPhotosWithIdentity.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<ApplicationRole> roleManager;

        public RoleController(RoleManager<ApplicationRole> roleManager)
        {
            //todo: roleManager DI null ellenõrzés
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            //todo: adatok betöltése és szolgáltatása
            return View();
        }
    }
}