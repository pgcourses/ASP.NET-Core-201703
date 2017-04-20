using FamilyPhotos.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyPhotos.Controllers
{
    public class ErrorsController : Controller
    {
        //public IActionResult StatusCodePagesWithRedirects(int id)
        public IActionResult StatusCodePagesWithRedirects(int statusCode)
        {
            //keressük ki, hogy mi volt az eredeti kérés:
            return View(statusCode);
        }

        public IActionResult StatusCodePagesWithReExecute(int statusCode)
        {
            var model = new StatusCodePagesWithReExecuteModel();
            model.StatusCode = statusCode;
            var feature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            if (feature!=null)
            {
                model.OriginalPath = feature.OriginalPath;
                model.OriginalPathBase = feature.OriginalPathBase;

                var featureReExecute = feature as StatusCodeReExecuteFeature;
                if (featureReExecute != null)
                {
                    model.OriginalQueryString =featureReExecute.OriginalQueryString;
                }
            }

            return View(model);
        }

        public IActionResult ExceptionHandler()
        {
            return View();
        }
    }
}
