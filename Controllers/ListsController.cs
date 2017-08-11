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
    
        public ListsController(MovieLogContext context) {
            _context = context;
        }        

        [HttpGet]
        [Route("lists")]
        public IActionResult Index() {
            // Gets user from session
            User CurrentUser = _context.Users.SingleOrDefault(person => person.UserId == (int)HttpContext.Session.GetInt32("CurrUserId"));
            ViewBag.User = CurrentUser;

            // All lists for user
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

        [HttpPost]
        [Route("edit_list/{ListId}")]
        public IActionResult EditList(int ListId) {
            // Gets user from session
            User CurrentUser = _context.Users.SingleOrDefault(person => person.UserId == (int)HttpContext.Session.GetInt32("CurrUserId"));
            ViewBag.User = CurrentUser;

            // Queries for matching list
            ViewBag.List = _context.Lists.SingleOrDefault(list => list.ListId == ListId);

            return View("EditList");
        }

        [HttpPost]
        [Route("update_list/{ListId}")]
        public IActionResult UpdateList(int ListId, List model) {
            // IN PROGRESS

            System.Console.WriteLine("***************************");

            List CurrentList = _context.Lists.SingleOrDefault(list => list.ListId == ListId);
            CurrentList.Name = model.Name;
            CurrentList.Description = model.Description;

            // _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("add_list")]
        public IActionResult AddList(List model){
            // Creates new list
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
            // Gets user from session
            User CurrentUser = _context.Users.SingleOrDefault(person => person.UserId == (int)HttpContext.Session.GetInt32("CurrUserId"));
            ViewBag.User = CurrentUser;

            // Queries for list from route params
            List<List> CurrentList = _context.Lists.Where(list => list.ListId == ListId).Include(list => list.Movies).ToList();
            ViewBag.List = CurrentList;

            return View("List");
        }

        [HttpPost]
        [Route("lists/delete_movie/{MovieId}/{ListId}")]
        public IActionResult DeleteMovie(int MovieId, int ListId) {

            // Queries for movie from list in route params
            List<Movie> MovieToRemove = _context.Movies.Where(movie => movie.ListId == ListId).ToList();

            // Removes movie with ListId from list
            foreach(var movie in MovieToRemove) {
                if(movie.MovieId == MovieId) {
                    _context.Remove(movie);
                }
            }
            _context.SaveChanges();

            return RedirectToAction("DisplayList", ListId);
        }        

        [HttpPost]
        [Route("SelectedMovie")]
        public IActionResult SelectedMovie(Movie model) {

            // Saves movie to list
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

            return RedirectToAction("DisplayList", newMovie.ListId);
        }

        [HttpGet]
        [Route("movie/{id}")]
        public IActionResult Movie(int id) {
            // Gets user from session
            ViewBag.User = _context.Users.SingleOrDefault(person => person.UserId == (int)HttpContext.Session.GetInt32("CurrUserId"));

            ViewBag.ApiId = id;

            return View("Movie");
        }
    }
}
