using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult ProcessFile(object sourceFile)
        {
            if (Request.Form.Files.Count == 0)
                return Json("Error: No File Uploaded.");

            var fileContents = Request.Form.Files.First();
            if (fileContents == null || fileContents.Length == 0)
                return Json("Error: Unable to read file.");

            Business.IncidentData myIncident = null; 
            using (var ms = new MemoryStream())
            {
                fileContents.CopyTo(ms);
                myIncident = new Business.IncidentData(byteArray:ms.ToArray());
            }
            return Json(myIncident);
        }
    }
}