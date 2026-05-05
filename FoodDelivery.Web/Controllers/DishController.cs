using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FoodDelivery.Web.Models;

namespace FoodDelivery.Web.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class DishController : Controller
    {
        private readonly DishService _service;

        public DishController(DishService service)
        {
            _service = service;
        }

        // =========================
        // LIST
        // =========================
        public async Task<IActionResult> Index()
        {
            var dishes = await _service.GetAll();
            return View(dishes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DishCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string? imagePath = null;
            if (model.ImageFile != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);

                var path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/images",
                    fileName
                );

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                imagePath = "/images/" + fileName;
            }

            var dish = new Dish(
                model.Name,
                model.Description,
                model.Price.Value,
                imagePath
            );

            var result = await _service.Create(dish);

            if (!result)
            {
                ModelState.AddModelError("Name", "Страва з такою назвою вже існує");
                return View(model);
            }

            return RedirectToAction("Index");
        }

        // =========================
        // EDIT
        // =========================
        public async Task<IActionResult> Edit(Guid id)
        {
            var dish = await _service.GetById(id);
            if (dish == null) return NotFound();

            var model = new DishEditViewModel
            {
                Id = dish.Id,
                Name = dish.Name,
                Description = dish.Description,
                Price = dish.Price,
                ImageUrl = dish.ImageUrl
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(DishEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var existingDish = await _service.GetById(model.Id);
            if (existingDish == null)
                return NotFound();

            string? imagePath = existingDish.ImageUrl;

            if (model.ImageFile != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);

                var path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/images",
                    fileName
                );

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                imagePath = "/images/" + fileName;
            }

            var result = await _service.Update(
                model.Id,
                model.Name,
                model.Description,
                model.Price.Value,
                imagePath
            );

            if (!result)
            {
                ModelState.AddModelError("Name", "Страва з такою назвою вже існує або не знайдена");
                return View(model);
            }

            return RedirectToAction("Index");
        }
        // =========================
        // DELETE
        // =========================
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
