using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Diagnostics;
using FamilyPhotos.Models;

namespace FamilyPhotos.Controllers
{
    public class ErrorsController : Controller
    {
        public IActionResult StatusCodeWithRedirects(int id)
        {
            //keressük ki hogy mi volt az eredeti kérés

            return View(id);
        }

        public IActionResult UseStatusCodePagesWithReExecute (int statusCode)
        {
            var model = new UseStatusCodePagesWithReExecuteModel();
            model.StatusCode = statusCode;
            var feature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            if (feature!=null)
            {
                model.OriginalPath = feature.OriginalPath;
                model.OriginalPathBase = feature.OriginalPathBase;

                var featureReExecute = feature as StatusCodeReExecuteFeature;
                if (featureReExecute != null)
                {
                    model.OriginalQueryString = featureReExecute.OriginalQueryString;
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
