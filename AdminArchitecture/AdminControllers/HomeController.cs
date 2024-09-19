using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Threading.Tasks;
using AdminEmployeeTool.Models;
using System.Diagnostics;

namespace AdminEmployeeTool.Controllers
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

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Homepage()
        {
            return View("Home");
        }

        public IActionResult ShowInitiaves()
        {
            return View("Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ChangeLanguage(string culture)
        {
            // Set culture
            Response.Cookies.Append(
                "Culture",
                culture,
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            // Redirect to the previous page
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
