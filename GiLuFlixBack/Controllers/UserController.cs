using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GiLuFlixBack.Data;
using GiLuFlixBack.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;


namespace GiLuFlixBack.Controllers;
public class UserController : Controller
{
    private readonly Context _context;
    public UserController(Context context)
    {
        _context = context;
    }
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(
        [FromForm] User user)
    {
        // simulando uma validação
        // if (user.email != "admin" ||
        //     user.GetPassword() != "admin")
        // {
        //     ViewBag.Fail = true;
        //     return View();
        // }

        // simulando um usuario no sistema
        var user_v = new
        {
            Id = Guid.NewGuid(),
            Name = "Administrador"
        };

        List<Claim> claims =
        [
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user_v.Name)
        ];
        var authScheme = CookieAuthenticationDefaults.AuthenticationScheme;

        var identity = new ClaimsIdentity(claims, authScheme);

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(authScheme, principal,
            new AuthenticationProperties
            {
                IsPersistent = user.RememberMe
            });

        if (!String.IsNullOrWhiteSpace(user.ReturnUrl))
        {
            return Redirect(user.ReturnUrl);
        }

        return RedirectToAction("Index","Movies");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToRoute("User.Login");
    }


}
