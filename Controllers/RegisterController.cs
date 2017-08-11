using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieLog.Models;

namespace MovieLog.Controllers
{
    public class RegisterController : Controller {
        private MovieLogContext _context;
    
        public RegisterController(MovieLogContext context) {
            _context = context;
        }

        [HttpGet]
        [Route("register")]
        public IActionResult RegisterPage() {
            ViewBag.Errors = TempData["Errors"];
            return View("Register");
        }

        [HttpPost]
        [Route("register_user")]

        public IActionResult RegisterUser(UserViewModel model) {
            List<string> allErrors = new List<string>();

            if(ModelState.IsValid) {
                User CheckUser = _context.Users.SingleOrDefault(person => person.Email == model.Email);

                // CHecks if user already exists
                if(CheckUser != null) {
                    allErrors.Add("Email already in use");
                    TempData["Errors"] = allErrors;
                    return RedirectToAction("Index");
                }

                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                User newUser = new User {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password
                };

                // Hashes password
                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);

                // Saves user to database
                _context.Add(newUser);
                _context.SaveChanges();

                // Puts user in session
                User user = _context.Users.SingleOrDefault(person => person.Email == model.Email);
                HttpContext.Session.SetInt32("CurrUserId", user.UserId);
                return RedirectToAction("Success");

            }
            // If errors
            foreach(var i in ModelState.Values) {
                if(i.Errors.Count > 0) {
                    allErrors.Add(i.Errors[0].ErrorMessage.ToString());
                }
            }
            TempData["Errors"] = allErrors;
            return RedirectToAction("RegisterPage", model);
        }

        [HttpGet]
        [Route("Register_Success")]
        public IActionResult Success() {
            return RedirectToAction("Dashboard", "Dashboard");
        }
    }
}
