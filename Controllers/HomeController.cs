using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MovieLog.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [RouteAttribute("logout")]
        public IActionResult Logout() {
            HttpContext.Session.Clear();
            return View("Index");
        }
    }
}
