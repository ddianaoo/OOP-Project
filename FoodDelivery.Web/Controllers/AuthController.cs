using Microsoft.AspNetCore.Mvc;
using FoodDelivery.Domain.Interfaces;
using FoodDelivery.Domain.Entities;
using FoodDelivery.Web.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using FoodDelivery.Domain.Enums;

public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        try
        {
            User user;

            if (model.Role == UserRole.Client)
            {
                user = new Client(
                    model.Email,
                    model.Password,
                    model.FirstName,
                    model.LastName,
                    model.BirthDate.Value,
                    model.Phone
                );
            }
            else
            {
                user = new Courier(
                    model.Email,
                    model.Password,
                    model.FirstName,
                    model.LastName,
                    model.BirthDate.Value,
                    model.Phone
                );
            }

            var result = _authService.Register(user);

            if (!result)
            {
                ViewBag.Error = "Такий користувач вже існує";
                return View(model);
            }

            return RedirectToAction("Login");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(model);
        }
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = _authService.Login(model.Email, model.Password);

        if (user == null)
        {
            ModelState.AddModelError(nameof(model.Password), "Невірна пошта або пароль");
            return View(model);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim("UserType", user.GetType().Name) // Admin / Client / Courier
        };

        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme
        );

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal
        );

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }
}