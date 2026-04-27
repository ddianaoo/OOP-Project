using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Web.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
