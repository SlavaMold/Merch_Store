using System.Diagnostics;
using Merch_Store.Models;
using Microsoft.AspNetCore.Mvc;

namespace Merch_Store.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string lang)
        {
            lang = Request.Cookies["lang"] ?? "eng"; // �������� ���� �� ����, �� ��������� eng
            ViewData["Lang"] = lang; // ��������� ����
            return View();
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
