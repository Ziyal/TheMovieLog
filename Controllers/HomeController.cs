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

        [HttpGet]
        [RouteAttribute("about")]
        public IActionResult About() {
            return View("About");
        }
        
    }
}
