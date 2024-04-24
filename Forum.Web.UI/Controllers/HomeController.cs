using Forum.Web.UI.Clients.Authentication;
using Forum.Web.UI.Clients.Users;
using Forum.Web.UI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Security.Claims;

namespace Forum.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserClient _userClient;
        private readonly IAuthenticationClient _authenticationClient;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            IUserClient userClient,
            IAuthenticationClient authenticationClient,
            ILogger<HomeController> logger)
        {
            _userClient = userClient;
            _authenticationClient = authenticationClient;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View(new CreateUserViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignUp(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _userClient.CreateAsync(new CreateUserRequest
                    {
                        Email = model.Email,
                        Username = model.Username,
                        Password = model.Password,
                    });

                    if (result.Id != null)
                    {
                        return RedirectToAction("Index"); // Redirect to home after successful signup
                    }
                    else
                    {
                        ModelState.AddModelError("", "Registration failed: Unable to create user.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Registration error: " + ex.Message);
                    ModelState.AddModelError("", "Registration failed: " + ex.Message);
                }
            }
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}