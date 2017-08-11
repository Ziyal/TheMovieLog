using Microsoft.AspNetCore.Mvc;

namespace MovieLog.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index() {
            // Renders index
            return View();
        }

        [HttpGet]
        [RouteAttribute("logout")]
        public IActionResult Logout() {
            // Removes user from session
            HttpContext.Session.Clear();

            return View("Index");
        }

        [HttpGet]
        [RouteAttribute("about")]
        public IActionResult About() {
            // Renders about page
            return View("About");
        }
        
    }
}
