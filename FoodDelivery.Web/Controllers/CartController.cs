using FoodDelivery.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodDelivery.Web.Controllers
{
    [Authorize(Policy = "ClientOnly")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        private Guid GetClientId()
        {
            return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        public async Task<IActionResult> Add(Guid dishId, int quantity)
        {
            await _cartService.AddToCart(GetClientId(), dishId, quantity);
            return RedirectToAction("Index", "Menu");
        }

        public async Task<IActionResult> Index()
        {
            var cart = await _cartService.GetCart(GetClientId());
            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(Guid dishId)
        {
            await _cartService.RemoveFromCart(GetClientId(), dishId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Clear()
        {
            await _cartService.Clear(GetClientId());
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Plus(Guid dishId)
        {
            await _cartService.UpdateQuantity(GetClientId(), dishId, 1);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Minus(Guid dishId)
        {
            await _cartService.UpdateQuantity(GetClientId(), dishId, -1);
            return RedirectToAction("Index");
        }
    }
}
