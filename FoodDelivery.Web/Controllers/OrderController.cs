using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Interfaces;
using FoodDelivery.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodDelivery.Web.Controllers
{
    [Authorize(Policy = "ClientOnly")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;

        public OrderController(IOrderService orderService, ICartService cartService)
        {
            _orderService = orderService;
            _cartService = cartService;
        }

        private Guid GetClientId()
        {
            return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var cart = await _cartService.GetCart(GetClientId());

            ViewBag.Cart = cart;

            return View(new CreateOrderViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderViewModel model)
        {
            var cart = await _cartService.GetCart(GetClientId());

            if (!ModelState.IsValid)
            {
                ViewBag.Cart = cart;
                return View(model);
            }

            var orderItems = cart.Items
                .Select(x => new OrderItem(x.DishId, x.Quantity))
                .ToList();

            var order = new Order(
                GetClientId(),
                model.Address,
                orderItems
            );

            await _orderService.CreateAsync(order);
            await _cartService.Clear(GetClientId());

            return RedirectToAction("My");
        }
        public async Task<IActionResult> My()
        {
            var orders = await _orderService.GetByClientIdAsync(GetClientId());
            return View(orders);
        }

    }
}
