using FoodDelivery.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Web.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class AdminOrderController : Controller
    {
        private readonly IOrderService _orderService;

        public AdminOrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllAsync();
            return View(orders);
        }
    }
}