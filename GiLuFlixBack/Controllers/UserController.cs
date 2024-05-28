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
    private readonly IReviewRepository _reviewRepository;

    public UserController(IUserRepository userRepository, IReviewRepository ReviewRepository)
    {
        _userRepository = userRepository;
        _reviewRepository = ReviewRepository;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromForm] User user)
    {
        // Validate presence of Email and Password
        if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
        {
            ViewBag.Fail = "Email e/ou senha obrigatórios!";
            return View(); // Return login view with error message
        }

        User userFromDb = await _userRepository.SearchByEmail(user.Email);
        
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


    [HttpGet]
    public async Task<IActionResult> Dashboard()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> DetailsById(int id)
    {
        if (id == null)
        {
            return NotFound();
        }

        User user = await _userRepository.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }

        var reviews = await _reviewRepository.GetAllUserReviews(id);
        user.Reviews = reviews;

        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> requestFriendship([FromForm] int requesterId, int requestedId)
    {
        Console.WriteLine("INFO recebida", requesterId, requestedId);
        try
        {   
            _userRepository.requestFriendship(requesterId,requestedId);
            ViewBag.SuccessMessage = "Requisição enviada!";
            return RedirectToAction("DetailsById","User", new { id = requestedId });
        }
        catch (InvalidOperationException ex)
        {
            ViewBag.ErrorMessage = ex.Message;
            return View();
        }
    }
}
