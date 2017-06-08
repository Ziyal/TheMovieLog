using System;
using System.Collections.Generic;
using System.Linq;
using UserDashboard.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieLog.Models;

namespace MovieLog.Controllers
{
    public class RegisterController : Controller {
        private MovieLogContext _context;
    
        public RegisterController(MovieLogContext context)
        {
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

        public IActionResult RegisterUser(User model) {
            List<string> allErrors = new List<string>();

            if(ModelState.IsValid) {
                User CheckUser = _context.Users.SingleOrDefault(person => person.Email == model.Email);

                if(CheckUser != null) {
                    allErrors.Add("Email already in use");
                    TempData["Errors"] = allErrors;
                    return RedirectToAction("Index");
                }

                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                User newUser = model;

                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);

                _context.Add(newUser);
                _context.SaveChanges();

                User user = _context.Users.SingleOrDefault(person => person.Email == model.Email);
                HttpContext.Session.SetInt32("CurrUserId", CheckUser.UserId);
                return RedirectToAction("Success");

            }
            foreach(var i in ModelState.Values) {
                if(i.Errors.Count > 0) {
                    allErrors.Add(i.Errors[0].ErrorMessage.ToString());
                }
            }
            TempData["Errors"] = allErrors;
            return RedirectToAction("Register", model);
        }

        [HttpGet]
        [Route("Register_Success")]
        public IActionResult Success() {
            return RedirectToAction("Dashboard", "Dashbaord");
        }

    

    }
}
