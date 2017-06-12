using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieLog.Models;

namespace MovieLog.Controllers
{
    public class ExploreController : Controller {
        private MovieLogContext _context;
    
        public ExploreController(MovieLogContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("explore")]
        public IActionResult Explore() {
            User CurrentUser = _context.Users.SingleOrDefault(person => person.UserId == (int)HttpContext.Session.GetInt32("CurrUserId"));
            ViewBag.User = CurrentUser;

            ViewBag.AllUsers = _context.Users;

            List<List> AllLists = _context.Lists.OrderByDescending(list => list.UpdatedAt).Include(u => u.User).ToList();
            ViewBag.AllLists = AllLists;
            
            return View("Explore");
        }

        [HttpGet]
        [Route("user_profile/{UserId}")]
        public IActionResult UserProfile(int UserId) {
            User CurrentUser = _context.Users.SingleOrDefault(person => person.UserId == (int)HttpContext.Session.GetInt32("CurrUserId"));
            ViewBag.User = CurrentUser;

            // ViewBag.User = _context.Users.SingleOrDefault(person => person.UserId == UserId);

            List<User> SelectedUser = _context.Users.Where(person => person.UserId == UserId).Include(user => user.Lists).ToList();
            ViewBag.CurrentUser = SelectedUser;

            return View("UserProfile");
        }

        [HttpGet]
        [Route("user_list/{ListId}")]
        public IActionResult UserList(int ListId) {
            User CurrentUser = _context.Users.SingleOrDefault(person => person.UserId == (int)HttpContext.Session.GetInt32("CurrUserId"));
            ViewBag.User = CurrentUser;

            List<List> CurrentList = _context.Lists.Where(list => list.ListId == ListId).Include(list => list.User).Include(list => list.Movies).ToList();
            ViewBag.CurrentList = CurrentList;

            return View("UserList");
        }

        [HttpGet]
        [Route("all_users")]
        public IActionResult AllUsers() {

            ViewBag.Users = _context.Users;

            return View("AllUsers");
        }

        [HttpGet]
        [Route("all_lists")]
        public IActionResult AllLists() {

            List<List> AllLists = _context.Lists.OrderByDescending(list => list.UpdatedAt).Include(u => u.User).ToList();
            ViewBag.Lists = AllLists;

            return View("AllUsers");
        }

    

    }
}