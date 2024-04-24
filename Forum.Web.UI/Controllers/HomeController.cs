using Forum.Web.UI.Clients.Authentication;
using Forum.Web.UI.Clients.Users;
using Forum.Web.UI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Refit;
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
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Index), model);
            }

            try
            {
                var user = await _authenticationClient
                    .LoginAsync(new AuthenticateRequest
                    {
                        Username = model.Username,
                        Password = model.Password
                    });

                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(ClaimTypes.Role, user.Role.ToString()!),
                    new Claim(ClaimTypes.NameIdentifier, user.Username!),
                    new Claim(ClaimTypes.Sid, user.Id.ToString()!),
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(new ClaimsPrincipal(identity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                ModelState.AddModelError("", "Invalid username or password");

                return View(nameof(Index), model);
            }

            return RedirectToAction(
                nameof(UsersController.Index),
                "Users");
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
                    // Mapping the CreateUserViewModel to CreateUserRequest as seen in UsersController
                    var createUserRequest = new CreateUserRequest
                    {
                        Email = model.Email,
                        Username = model.Username,
                        Password = model.Password,
                        FirstName = model.FirstName, 
                        LastName = model.LastName,
                        ConfirmPassword = model.ConfirmPassword
                    };

                    var result = await _userClient.CreateAsync(createUserRequest);

                    if (result.Id != null)
                    {
                        return RedirectToAction("Index"); // Redirect to home after successful signup
                    }
                    else
                    {
                        ModelState.AddModelError("", "Registration failed: Unable to create user.");
                    }
                }
                catch (ApiException apiEx) when (apiEx.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    // Handle the case where the username or email is already taken
                    ModelState.AddModelError("", "Registration failed: User with the same email or username already exists.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Registration error: " + ex.Message);
                    ModelState.AddModelError("", "Registration failed: An unexpected error occurred.");
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