using FoodDelivery.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FoodDelivery.Web.Controllers
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
            if (User.HasClaim("UserType", "Client")) {
                return RedirectToAction("Index", "Menu");
            } else if (User.HasClaim("UserType", "Admin"))
            {
                return RedirectToAction("Index", "Dish");
            } else
            {
               return RedirectToAction("Index", "CourierOrder");
            }
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
