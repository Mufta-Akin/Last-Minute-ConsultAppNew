using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Consultation.Web.ViewModels;
using Consultation.Data.Services;

namespace Consultation.Web.Controllers
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

        public IActionResult HomeIndex()
        {
            var about = new HomeIndexViewModel
            {

            };
            return View();
        }

        [Authorize]
        public IActionResult Secure()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            var about = new AboutViewModel
            {
                Title = "About",
                Message = "Partially Automated Online Consultation Using Data Matching Algorithm.",
                Formed = DateTime.Now
            };
            return View(about);
        }

        public IActionResult Welcome()
        {
            var welcome = new AboutViewModel
            {
                Title = "Welcome",
                Message = "Partially Automated Online Consultation Using Data Matching Algorithm.",
                Formed = DateTime.Now
            };
            return View(welcome);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
