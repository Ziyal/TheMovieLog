using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieLog.Models;

namespace MovieLog.Controllers
{
    public class ProfileController : Controller {
        private MovieLogContext _context;
        private IHostingEnvironment hostingEnv;
        
    
        public ProfileController(MovieLogContext context, IHostingEnvironment env) {
            _context = context;
            this.hostingEnv = env;
        }

        [HttpGet]
        [Route("profile/")]
        public IActionResult Profile() {
            // Gets user from session
            User CurrentUser = _context.Users.SingleOrDefault(person => person.UserId == (int)HttpContext.Session.GetInt32("CurrUserId"));
            ViewBag.User = CurrentUser;

            ViewBag.Success = TempData["Success"];
            return View("Profile");
        }

        [HttpGet]
        [Route("profile/edit")]
        public IActionResult EditPage() {
            // Gets user from session
            User CurrentUser = _context.Users.SingleOrDefault(person => person.UserId == (int)HttpContext.Session.GetInt32("CurrUserId"));
            ViewBag.User = CurrentUser;

            ViewBag.Success = TempData["Success"];
            return View("Edit");
        }

        [HttpPost]
        [Route("profile/update_profile/{UserId}")]
        public IActionResult UpdateProfile(int UserId, User model) {

            System.Console.WriteLine("**********************************");

            User CurrentUser = _context.Users.SingleOrDefault(person => person.UserId == UserId);

            CurrentUser.FirstName = model.FirstName;
            CurrentUser.LastName = model.LastName;
            CurrentUser.Email = model.Email;
            CurrentUser.About = model.About;
            CurrentUser.FavoriteMovie = model.FavoriteMovie;
            CurrentUser.UpdatedAt = DateTime.Now;
            _context.SaveChanges();

            TempData["Success"] = "Profile successfully updated";
            return RedirectToAction("Profile");
        }
        public IActionResult UploadProfilePicture() {
            return RedirectToAction("Profile");
        }

        [HttpPost]
        [Route("UploadProfilePicture")]
        public IActionResult UploadProfilePicture(IList<IFormFile> ProfilePicture) {
            // Gets user from session
            User CurrentUser = _context.Users.SingleOrDefault(user => user.UserId == (int)HttpContext.Session.GetInt32("CurrUserId"));
            
            long size = 0;
            var location = "";

            // Saves image to server
            foreach(var file in ProfilePicture) {
                var filename = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
                location = $@"/images/avatars/{filename}";
                filename = hostingEnv.WebRootPath + $@"\images\avatars\{filename}";
                size += file.Length;
                using (FileStream fs = System.IO.File.Create(filename)){
                file.CopyTo(fs);
                fs.Flush();
                }
            }

            CurrentUser.ProfilePicture = location;
            _context.SaveChanges();

            TempData["Success"] = "Profile photo successfully updated";
            return RedirectToAction("EditPage");
        }
    }
}
