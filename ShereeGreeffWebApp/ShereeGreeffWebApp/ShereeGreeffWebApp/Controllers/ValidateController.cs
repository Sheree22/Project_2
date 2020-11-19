using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShereeGreeffWebApp.Models;

namespace ShereeGreeffWebApp.Controllers
{
    public class ValidateController : Controller
    {
        //GET: Validate
        public IActionResult Index()
        {
            return View();
        }
       /* [HttpPost]
        public IActionResult Index(ValidateClass vc)
        {
            return View();
        }*/

    }
}
