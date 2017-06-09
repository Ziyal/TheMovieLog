using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieLog.Models;

namespace MovieLog.Controllers
{
    public class LoginController : Controller {
        private MovieLogContext _context;
    
        public LoginController(MovieLogContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("login")]
        public IActionResult LoginPage() {
            ViewBag.Errors = TempData["Errors"];
            return View("Login");
        }

        [HttpPost]
        [Route("login_user")]
        public IActionResult Login(User model) {
            List<string> allErrors = new List<string>();

            // If no errors
            if(ModelState.IsValid) {
                User user = _context.Users.SingleOrDefault(person => person.Email == model.Email);

                // Check if user exists
                if(user != null && model.Password != null) {
                    var Hasher = new PasswordHasher<User>();

                    // Check if passwords match
                    if(0 != Hasher.VerifyHashedPassword(user, user.Password, model.Password)) {
                        HttpContext.Session.SetInt32("CurrUserId", user.UserId);
                        return RedirectToAction("Success");
                    }
                    else {
                        allErrors.Add("Incorrect password");
                        TempData["Errors"] = allErrors;
                    }
                }
                else {
                    allErrors.Add("Incorrect email");
                    TempData["Errors"] = allErrors;
                }
            }
            // Add errors to display
            foreach(var i in ModelState.Values) {
                if(i.Errors.Count > 0) {
                    allErrors.Add(i.Errors[0].ErrorMessage.ToString());
                }
            }
            TempData["Errors"] = allErrors;
            return RedirectToAction("LoginPage");
        }

        [HttpGet]
        [Route("login_success")]
        public IActionResult Success() {
            return RedirectToAction("Dashboard", "Dashboard");
        }
    }
}
