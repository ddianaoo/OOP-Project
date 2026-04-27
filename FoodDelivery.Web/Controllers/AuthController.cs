using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Web.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
