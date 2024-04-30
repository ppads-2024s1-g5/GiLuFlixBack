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
using GiLuFlixBack.Data;
using System.Linq;
using System;



namespace GiLuFlixBack.Controllers;
public class UserController : Controller
{
    private readonly Context _context;
    private readonly IUserRepository _userRepository;

    public UserController(Context context, IUserRepository userRepository)
    {
        _context = context;
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
                      $"Email: {user.email}\n" +
                      $"Senha: {user.password}\n" +
                      $"Lembrar de mim: {user.RememberMe}\n";

        Console.WriteLine(userInfo);

        // Validate presence of email and password
        if (string.IsNullOrEmpty(user.email) || string.IsNullOrEmpty(user.password))
        {
            ViewBag.Fail = "Email e/ou senha obrigatórios!";
            return View(); // Return login view with error message
        }

        User userFromDb = await _userRepository.SearchByEmail(user.email);
        string userFromDbInfo = $"INFORMAÇÃO RECEBIDA DO BANCO:\n" +
                                $"Id: {userFromDb.id}\n" +
                                $"Email: {userFromDb.email}\n" +
                                $"Senha: {userFromDb.password}\n" +
                                $"Lembrar de mim: {userFromDb.RememberMe}\n";
        
        Console.WriteLine(userFromDbInfo);
        
        if (userFromDb.email == null)
        {
            return ViewBag("Usuário não encontrado!");
        }   

        if (userFromDb.isPasswordCorrect(user.password) == false)
        {
            return ViewBag("Senha incorreta!");
        }        

        List<Claim> claims =
        [
            new Claim(ClaimTypes.NameIdentifier, userFromDb.id.ToString()),
            new Claim(ClaimTypes.Name, userFromDb.name)
        ];
        var authScheme = CookieAuthenticationDefaults.AuthenticationScheme;

        var identity = new ClaimsIdentity(claims, authScheme);

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(authScheme, principal,
            new AuthenticationProperties
            {
                IsPersistent = userFromDb.RememberMe
            });
        
        // REDIRECIONAR O USUARIO
        // if (!String.IsNullOrWhiteSpace(userFromDb.ReturnUrl))
        // {
        //     return Redirect(userFromDb.ReturnUrl);
        // }

        return RedirectToAction("Index","Movies");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToRoute("User.Login");
    }


}
