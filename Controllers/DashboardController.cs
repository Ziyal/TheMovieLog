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
    public class DashboardController : Controller {
        private MovieLogContext _context;
    
        public DashboardController(MovieLogContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard() {
            User CurrentUser = _context.Users.SingleOrDefault(person => person.UserId == (int)HttpContext.Session.GetInt32("CurrUserId"));
            ViewBag.User = CurrentUser;
            
            return View("Dashboard");
        }


    

    }
}
