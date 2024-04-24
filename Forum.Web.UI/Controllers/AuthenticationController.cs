using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.UI.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult Login()
        {
            // Display login form
            return View();
        }

        // POST: Authentication/Login
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            try
            {
                // Authenticate user logic here
                // Redirect to appropriate page upon successful authentication
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                // Authentication failed, return to login page with error message
                ViewBag.ErrorMessage = "Invalid username or password";
                return View();
            }
        }

        // GET: Authentication/Logout
        public ActionResult Logout()
        {
            // Perform logout logic here
            // Redirect to login page after logout
            return RedirectToAction("Login");
        }

        // GET: Authentication/Register
        public ActionResult Register()
        {
            // Display registration form
            return View();
        }

        // POST: Authentication/Register
        [HttpPost]
        public ActionResult Register(string username, string password, string confirmPassword)
        {
            try
            {
                // Register user logic here
                // Redirect to login page upon successful registration
                return RedirectToAction("Login");
            }
            catch
            {
                // Registration failed, return to registration page with error message
                ViewBag.ErrorMessage = "Registration failed. Please try again.";
                return View();
            }
        }
    }
}
