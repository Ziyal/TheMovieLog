using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieLog.Models;

namespace MovieLog.Controllers
{
    public class ListsController : Controller
    {

        private MovieLogContext _context;
    
        public ListsController(MovieLogContext context)
        {
            _context = context;
        }        

        [HttpGet]
        [Route("lists")]
        public IActionResult Index() {

            User CurrentUser = _context.Users.SingleOrDefault(person => person.UserId == (int)HttpContext.Session.GetInt32("CurrUserId"));
            ViewBag.User = CurrentUser;

            ViewBag.Lists = _context.Lists.Where(list => list.UserId == CurrentUser.UserId);


            return View("Lists");
        }

        [HttpPost]
        [Route("add_list")]
        public IActionResult AddList(List model){
            List newList = new List {
                Name = model.Name,
                Description = model.Description,
                UserId = (int)HttpContext.Session.GetInt32("CurrUserId")
            };

            _context.Add(newList);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("lists/{ListId}")]

        public IActionResult DisplayList(int ListId) {

            

            return View("List");
        }

        

        
    }
}
