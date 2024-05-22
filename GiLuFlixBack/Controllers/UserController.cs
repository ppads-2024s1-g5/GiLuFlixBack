using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using GiLuFlixBack.Repository;
using System.Threading.Tasks;
using System.Security.Claims;
using GiLuFlixBack.Models;
using System.Linq;
using System;



namespace GiLuFlixBack.Controllers;
public class UserController : Controller
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromForm] User user)
    {

        string userInfo = $"INFORMAÇÃO RECEBIDA DO FORMULARIO:\n" +
                      $"Email: {user.Email}\n" +
                      $"Senha:Q {user.Password}\n" +
                      $"Lembrar de mim: {user.RememberMe}\n";

        Console.WriteLine(userInfo);

        // Validate presence of Email and Password
        if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
        {
            ViewBag.Fail = "Email e/ou senha obrigatórios!";
            return View(); // Return login view with error message
        }

        User userFromDb = await _userRepository.SearchByEmail(user.Email);
        string userFromDbInfo = $"INFORMAÇÃO RECEBIDA DO BANCO:\n" +
                                $"Id: {userFromDb.Id}\n" +
                                $"Email: {userFromDb.Email}\n" +
                                $"Senha: {userFromDb.Password}\n" +
                                $"Lembrar de mim: {userFromDb.RememberMe}\n";
        
        Console.WriteLine(userFromDbInfo);
        
        if (userFromDb.Email == null)
        {
            return ViewBag("Usuário não encontrado!");
        }   

        if (userFromDb.isPasswordCorrect(user.Password) == false)
        {
            return ViewBag("Senha incorreta!");
        }        

        List<Claim> claims =
        [
            new Claim(ClaimTypes.NameIdentifier, userFromDb.Id.ToString()),
            new Claim(ClaimTypes.Name, userFromDb.Name)
        ];
        var authScheme = CookieAuthenticationDefaults.AuthenticationScheme;

        var identity = new ClaimsIdentity(claims, authScheme);

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(authScheme, principal,
            new AuthenticationProperties
            {
                IsPersistent = userFromDb.RememberMe
            });
        return RedirectToAction("Index","Movies");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToRoute("User.Login");
    }



    public async Task<IActionResult> Dashboard()
    {
        return View();
    }



}
