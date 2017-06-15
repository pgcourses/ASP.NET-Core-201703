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
    //[Authorize(Roles="Administrators")] //ez a régi módszer, ugyanúgy használható
    //[Authorize(Policy="RequiredElevatedAdminRights")] ez ugyanaz, mint a következõ, mert ez az alapértelmezett
    [Authorize("RequiredElevatedAdminRights")]
    public class RoleController : Controller
    {
        private readonly RoleManager<ApplicationRole> roleManager;

        public RoleController(RoleManager<ApplicationRole> roleManager)
        {
            this.roleManager = roleManager.ThrowIfNull();
        }

        public IActionResult Index()
        {
            return View(new List<RoleViewModel>());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new RoleViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel vm)
        {
            if (!ModelState.IsValid) { return View(vm); }

            var roleToCreate = new ApplicationRole(vm.Name);
            var identityResult = await roleManager.CreateAsync(roleToCreate);


            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", $"{error.Code}: {error.Description}");
                }
                return View(vm);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Példa az urlCode feloldására
        /// </summary>
        /// <param name="urlCode"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(string urlCode)
        {
            var role = roleManager.Roles
                                  .SingleOrDefault(x => x.UrlCode == urlCode);

            if (role==null)
            {
                return NotFound(urlCode);
            }

            var vm = new RoleViewModel { UrlCode = urlCode, Name = role.Name };
            return View(vm);
        }

        /// <summary>
        /// Példa az urlCode feloldására
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel vm)
        {
            if (!ModelState.IsValid) { return View(vm); }

            var roleToModify = roleManager.Roles
                                  .SingleOrDefault(x => x.UrlCode == vm.UrlCode);

            if (roleToModify==null)
            {
                return NotFound(vm.UrlCode);
            }

            roleToModify.Name = vm.Name;
            var identityResult = await roleManager.UpdateAsync(roleToModify);

            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", $"{error.Code}: {error.Description}");
                }
                return View(vm);
            }
            return RedirectToAction("Index");
        }
    }
}