using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [Route("delete_list/{ListId}")]
        public IActionResult DeleteList(int ListId) {

            // Remove all movies from list
            List<Movie> MoviesToRemove = _context.Movies.Where(movie => movie.ListId == ListId).ToList();
            foreach(var movie in MoviesToRemove) {
                _context.Remove(movie);
            }

            // Delete list
            List ListToRemove = _context.Lists.SingleOrDefault(list => list.ListId == ListId);
            _context.Remove(ListToRemove);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // [HttpPost]
        // [Route("edit_list/{ListId}")]
        // public IActionResult EditList(int ListId, List model) {

        //     System.Console.WriteLine("***************************");
        //     System.Console.WriteLine(model.Name);
        //     System.Console.WriteLine(model.Description);

        //     return RedirectToAction("Index");
        // }

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

            User CurrentUser = _context.Users.SingleOrDefault(person => person.UserId == (int)HttpContext.Session.GetInt32("CurrUserId"));
            ViewBag.User = CurrentUser;

            List<List> CurrentList = _context.Lists.Where(list => list.ListId == ListId).Include(list => list.Movies).ToList();

            System.Console.WriteLine(CurrentList);

            ViewBag.List = CurrentList;


            return View("List");
        }


        [HttpPost]
        [Route("delete_movie/{MovieId}/{ListId}")]
        public IActionResult DeleteMovie(int MovieId, int ListId) {

            System.Console.WriteLine("**************************");

            List<Movie> MovieToRemove = _context.Movies.Where(movie => movie.ListId == ListId).ToList();
            foreach(var movie in MovieToRemove) {
                if(movie.MovieId == MovieId) {
                    _context.Remove(movie);
                }
            }
            _context.SaveChanges();

            return RedirectToAction("Index");
        }        

        [HttpPost]
        [Route("SelectedMovie")]
        public IActionResult SelectedMovie(Movie model) {

            Movie newMovie = new Movie {
                Id = model.Id,
                Title = model.Title,
                Release = model.Release,
                ListId = model.ListId
            };

            List SelectedList = _context.Lists.SingleOrDefault(list => list.ListId == model.ListId);
            SelectedList.UpdatedAt = DateTime.Now;

            _context.Add(newMovie);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("movie/{id}")]
        public IActionResult Movie(int id) {
            ViewBag.User = _context.Users.SingleOrDefault(person => person.UserId == (int)HttpContext.Session.GetInt32("CurrUserId"));

            ViewBag.ApiId = id;

            return View("Movie");
        }
        

        
    }
}
