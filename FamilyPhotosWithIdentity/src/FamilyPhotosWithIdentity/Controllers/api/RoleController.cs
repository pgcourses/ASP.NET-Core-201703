using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using FamilyPhotosWithIdentity.Models;
using FamilyPhotosWithIdentity.Helpers;
using FamilyPhotosWithIdentity.Models.RoleViewModels;
using DataTables.AspNet.Core;
using DataTables.AspNet.AspNetCore;

namespace FamilyPhotosWithIdentity.Controllers.api
{
    [Produces("application/json")]
    [Route("api/Role")]
    public class RoleController : Controller
    {
        private readonly RoleManager<ApplicationRole> roleManager;

        public RoleController(RoleManager<ApplicationRole> roleManager)
        {
            this.roleManager = roleManager.ThrowIfNull();
        }

        /// <summary>
        /// A lista adatforr�sa: kell neki param�ter, amit a DataTables k�ld
        /// �s a visszat�r�si JSON-nel a DataTables-nek  tudnia kell dolgozni
        /// </summary>
        /// <returns></returns>

        [HttpPost]
        public IActionResult Post(IDataTablesRequest request)
        {
            var roles = roleManager.Roles.ToList();
            var vm = new List<RoleViewModel>();

            //todo: automapper beizz�t�sa
            foreach (var role in roles)
            {
                vm.Add(new RoleViewModel { UrlCode = role.UrlCode, Name = role.Name });
            }



            var filteredVm = string.IsNullOrWhiteSpace(request?.Search.Value)
                                     ? vm
                                     : vm.Where(x => x.Name.Contains(request?.Search.Value))
                                         .ToList();

            var vmPage = filteredVm.Skip(request.Start).Take(request.Length);

            //El�k�sz�let a DataTables v�laszra
            var response = DataTablesResponse.Create(request, vm.Count, filteredVm.Count, vmPage);
            return new DataTablesJsonResult(response);
        }
    }
}