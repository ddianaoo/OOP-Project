using FoodDelivery.Domain.Interfaces;
using FoodDelivery.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodDelivery.Web.Controllers
{
    [Authorize(Policy = "CourierOnly")]
    public class CourierOrderController : Controller
    {
        private readonly IOrderService _orderService;

        public CourierOrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var courierId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var available = await _orderService.GetAvailableOrders();
            var myOrders = await _orderService.GetMyActiveOrders(courierId);

            var model = new CourierOrdersViewModel
            {
                AvailableOrders = available,
                MyOrders = myOrders
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Accept(Guid orderId)
        {
            var courierId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var result = await _orderService.Accept(orderId, courierId);

            if (!result)
                TempData["Error"] = "Не вдалося прийняти замовлення";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> InProgress(Guid orderId)
        {
            var courierId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _orderService.SetInProgress(orderId, courierId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delivered(Guid orderId)
        {
            var courierId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _orderService.Deliver(orderId, courierId);
            return RedirectToAction("Index");
        }
    }
}