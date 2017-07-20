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
using Microsoft.AspNetCore.Authorization;

namespace FamilyPhotosWithIdentity.Controllers.api
{
    [Produces("application/json")]
    [Route("api/Role")]
    //[Authorize("RequiredElevatedAdminRights")]
    //[Authorize("RequiredElevatedAdminRightsForAPI")]
    [Authorize("RequiredElevatedAdminRigthsCombined")]
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
                                     ;

            var sortColumns = request.Columns
                                     .Where(c => c.Sort != null)
                                     .OrderBy(c=> c.Sort.Order)
                                     .ToList();

            //Linq Expression haszn�lat�val lehetne �ltal�noss� tenni
            foreach (var column in sortColumns)
            {
                if (column.Sort.Direction==SortDirection.Ascending)
                {
                    if (column.Field.Equals("Name", StringComparison.OrdinalIgnoreCase))
                    {
                        filteredVm = filteredVm.OrderBy(c => c.Name);
                    }
                    if (column.Field.Equals("UrlCode", StringComparison.OrdinalIgnoreCase))
                    {
                        filteredVm = filteredVm.OrderBy(c => c.UrlCode);
                    }
                }
                else
                {
                    if (column.Field.Equals("Name", StringComparison.OrdinalIgnoreCase))
                    {
                        filteredVm = filteredVm.OrderByDescending(c => c.Name);
                    }
                    if (column.Field.Equals("UrlCode", StringComparison.OrdinalIgnoreCase))
                    {
                        filteredVm = filteredVm.OrderByDescending(c => c.UrlCode);
                    }
                }
            }

            var vmPage = filteredVm.Skip(request.Start)
                                   .Take(request.Length)
                                   .ToList();

            //El�k�sz�let a DataTables v�laszra
            var response = DataTablesResponse.Create(request, vm.Count, filteredVm.Count(), vmPage);
            return new DataTablesJsonResult(response);
        }
    }
}