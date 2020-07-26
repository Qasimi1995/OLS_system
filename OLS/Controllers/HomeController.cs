﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OLS.Models;

namespace OLS.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Navigate() {
            if (User.IsInRole("Applicant"))
            {
                return RedirectToAction("Navigate", "Founder");

            }
            else if (User.IsInRole("DPERep"))
            {
                return RedirectToAction("Index", "DPERep");
            }
            else if (User.IsInRole("PED"))
            {
                return RedirectToAction("Index", "DPERep");
            }
            else if (User.IsInRole("DPE"))
            {
                return RedirectToAction("Index", "DPERep");
            }
            else if (User.IsInRole("Admin"))
            {
                return RedirectToAction("RegisterStaff", "Account");
            }

            ViewBag.RoleError = "Role verification erro, please contact administrator";
            return View("../Account/Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
