using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using GiLuFlixBack.Repository;
using System.Security.Claims;
using GiLuFlixBack.Models;
using Microsoft.AspNetCore.Authorization;



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

    [HttpGet]
    public async Task<IActionResult> UserPage(int id) {
        User user = await _userRepository.GetUserById(id);
        return View(user);
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

        string role = await _userRepository.GetUserRole(user.Email);
        Console.WriteLine("ROLE DO USUARIO ENCONTRADO " + role.ToString());

        List<Claim> claims =
        [
            new Claim(ClaimTypes.NameIdentifier, userFromDb.Id.ToString()),
            new Claim(ClaimTypes.Name, userFromDb.Name),
            new Claim(ClaimTypes.Role, role)
        ];
        var authScheme = CookieAuthenticationDefaults.AuthenticationScheme;

        var identity = new ClaimsIdentity(claims, authScheme);

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(authScheme, principal,
            new AuthenticationProperties
            {
                IsPersistent = userFromDb.RememberMe
            });
        return RedirectToAction("Index", "Movies");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Login", "User");
    }

    [HttpGet]
    // [Authorize(Roles = "admin")]
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

        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> RequestFriendship([FromForm] int requestedId)
    {
        // GET USER ID (logged user)
        var loggedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Console.WriteLine("ID USER SESSION" + loggedUserId);
        Console.WriteLine("ID RECEBIDO DO FORM "+ requestedId);
        
        try
        {   
            var response = await _userRepository.requestFriendship(Convert.ToInt32(loggedUserId),requestedId);
            ViewBag.SuccessMessage = "Requisição enviada!";
            return RedirectToAction("DetailsById","User", new { id = requestedId });
        }
        catch (InvalidOperationException ex)
        {
            ViewBag.ErrorMessage = ex.Message;
            return View();
        }
    }

    [HttpPost]
    public async Task<IActionResult> acceptFriendship([FromForm] int requesterId)

    {

        var loggedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        Console.WriteLine("INFO recebida" + requesterId + loggedUserId);
        try
        {
            await _userRepository.acceptFriendship(requesterId, Convert.ToInt32(loggedUserId));
            await _userRepository.deleteFriendshipRequest(requesterId, Convert.ToInt32(loggedUserId));
            ViewBag.SuccessMessage = "Requisição enviada!";
            return RedirectToAction("DetailsById","User", new { id = loggedUserId });
        }
        catch (InvalidOperationException ex)
        {
            ViewBag.ErrorMessage = ex.Message;
            return View();
        }
    }
}
