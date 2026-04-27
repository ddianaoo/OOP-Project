using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Web.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
