﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ShereeGreeffWebApp.Controllers
{
    public class ManagerController : Controller
    {
        [Authorize(Roles = "Manager")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
