using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Web.Controllers
{
    [Authorize(Policy = "ClientOnly")]
    public class MenuController : Controller
    {
        private readonly DishService _dishService;

        public MenuController(DishService dishService)
        {
            _dishService = dishService;
        }

        public async Task<IActionResult> Index()
        {
            var dishes = await _dishService.GetAll();
            return View(dishes);
        }
    }
}
