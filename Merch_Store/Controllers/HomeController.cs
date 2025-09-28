using merch_store.API_Layer;
using merch_store.BusinessLogic.Services;
using merch_store.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace merch_store.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductService _productService;

        public HomeController(ILogger<HomeController> logger, ProductService productService)
        {
            _productService = productService;
            _logger = logger;
        }

        public IActionResult Index(string lang)
        {
            var products = _productService.GetAllProducts();
            lang = Request.Cookies["lang"] ?? "eng"; // получаем язык из куки, по умолчанию eng
            ViewData["Lang"] = lang; // сохраняем язык
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
