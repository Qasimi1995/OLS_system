﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OLS.Controllers
{
    public class ApplicantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}