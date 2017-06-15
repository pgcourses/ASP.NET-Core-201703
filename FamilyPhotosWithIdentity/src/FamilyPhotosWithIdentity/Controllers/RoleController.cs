using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using FamilyPhotosWithIdentity.Models;
using FamilyPhotosWithIdentity.Helpers;
using Microsoft.AspNetCore.Authorization;
using FamilyPhotosWithIdentity.Models.RoleViewModels;

namespace FamilyPhotosWithIdentity.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly RoleManager<ApplicationRole> roleManager;

        public RoleController(RoleManager<ApplicationRole> roleManager)
        {
            this.roleManager = roleManager.ThrowIfNull();
        }

        public IActionResult Index()
        {
            var roles = roleManager.Roles.ToList();
            var vm = new List<RoleViewModel>();

            //todo: automapper beizzítása
            foreach (var role in roles)
            {
                vm.Add(new RoleViewModel { UrlCode = role.UrlCode, Name = role.Name });
            }

            return View(vm);
        }
    }
}