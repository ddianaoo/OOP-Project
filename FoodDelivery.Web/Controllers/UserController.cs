using FoodDelivery.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Web.Models;
namespace FoodDelivery.Web.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class UserController : Controller
    {
        private readonly UserService _service;

        public UserController(UserService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var clients = await _service.GetClients();
            var couriers = await _service.GetCouriers();

            var model = new UserListViewModel
            {
                Clients = clients,
                Couriers = couriers
            };

            return View(model);
        }
    }
}
