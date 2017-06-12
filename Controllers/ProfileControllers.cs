using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieLog.Models;

namespace MovieLog.Controllers
{
    public class ProfileController : Controller {
        private MovieLogContext _context;
    
        public ProfileController(MovieLogContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("profile/")]
        public IActionResult Dashboard() {
            User CurrentUser = _context.Users.SingleOrDefault(person => person.UserId == (int)HttpContext.Session.GetInt32("CurrUserId"));
            ViewBag.User = CurrentUser;

            ViewBag.Success = TempData["Success"];
            
            return View("Profile");
        }

        [HttpGet]
        [Route("profile/edit")]
        public IActionResult EditPage() {
            User CurrentUser = _context.Users.SingleOrDefault(person => person.UserId == (int)HttpContext.Session.GetInt32("CurrUserId"));
            ViewBag.User = CurrentUser;

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

            TempData["Success"] = "Profile successfuly updated";
            return RedirectToAction("Dashboard");
        }

        // [HttpPost]
        // [Route("upload_profile_image")]
        // public IActionResult UploadProfile(IList<IFormFile> file) {

        //     IFormFile UploadedImage = file.FirstOrDefault();

        //     if(UploadedImage == null || UploadedImage.ContentType.ToLower().StartsWith("image/")) {
        //         using(ImageDBContext dbContext = new ImageDBContext()) {
        //             MemoryStream ms = new MemoryStream();
        //             UploadedImage.OpenReadStream().CopyTo(ms); 
 
        //             System.Drawing.Image image = System.Drawing.Image.FromStream(ms); 
 
        //             Models.Image imageEntity = new Models.Image() 
        //             { 
        //                 ImageId = Guid.NewGuid(),
        //                 SetName(UploadedImage.Name), 
        //                 Data = ms.ToArray(), 
        //                 Width = image.Width, 
        //                 Height = image.Height, 
        //                 ContentType = UploadedImage.ContentType 
        //             }; 
 
        //             _context.Images.Add(imageEntity); 
 
        //             _context.SaveChanges(); 
        //         }
        //     }

        //     TempData["Success"] = "Profile photo successfuly updated";
        //     return RedirectToAction("Dashboard");
        // }

        
        [HttpPost]
        [Route("upload_profile_image")]
        public IActionResult UploadProfile(IList<IFormFile> ProfilePicture) {

            System.Console.WriteLine("***********************");
            System.Console.WriteLine(ProfilePicture);
            
            TempData["Success"] = "Profile photo successfuly updated";
            return RedirectToAction("Dashboard");
        }


        


    

    }
}
