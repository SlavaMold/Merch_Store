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

        public IActionResult Index(string lang, string? category, string? type, string? band)
        {
            lang = Request.Cookies["lang"] ?? "eng"; // получаем язык из куки, по умолчанию eng
            ViewData["Lang"] = lang; // сохраняем язык

            // получаем фильтрованные товары
            var products = _productService.GetFilteredProducts(category, type, band, lang);
            ViewBag.SelectedCategory = category?.ToLower();
            ViewBag.SelectedType = type?.ToLower();
            ViewBag.SelectedBand = band;

            return View(products);
        }

        [HttpGet]
        public IActionResult ProductsPartial(string? category, string? type, string? band, string lang)
        {

            var products = _productService.GetFilteredProducts(category, type, band, lang);
            return PartialView("_ProductsGrid", products);
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
