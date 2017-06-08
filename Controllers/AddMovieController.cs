using System;
using System.Collections.Generic;
using System.Linq;
using UserDashboard.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieLog.Models;
// using System.Web;
// using System.Web.UI;
// using System.Web.UI.WebControls;
// using System.Web.Services;

namespace MovieLog.Controllers
{
    public class AddMovieController : Controller {
        private MovieLogContext _context;
    
        public AddMovieController(MovieLogContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("add_page")]
        public IActionResult AddPage() {
            User CurrentUser = _context.Users.SingleOrDefault(person => person.UserId == (int)HttpContext.Session.GetInt32("CurrUserId"));
            ViewBag.User = CurrentUser;
            
            return View("AddMovie");
        }

        // [WebMethod]
        // [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        // public static String SelectedMovie() {

        // }


    

    }
}
