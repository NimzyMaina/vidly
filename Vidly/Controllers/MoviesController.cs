using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            //var movies = GetMovies();
            var movies = _context.Movies.Include(m => m.Genre).ToList();
            return View(movies);
        }

        public ActionResult Random()
        {
            var movie = new Movie { Name = "Avengers"};

            var customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "Lolo" },
                new Customer { Id = 2, Name = "Nimzy"}
            };

            var viewModel = new RandomMovieViewModel {Customers = customers, Movie = movie};

            return View(viewModel);

        }

        public ActionResult Edit2(int Id = 10, string optionalstr = "default string",int optionalint = 10)
        {
            return Content($"Id = {Id} & optionalstr = {optionalstr} & optionalint = {optionalint}");
        }

        [Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year,int month)
        {
            return Content($"{year}/{month}");
        }

        private IEnumerable<Movie> GetMovies()
        {
            return new List<Movie>
            {
                new Movie { Id = 1, Name = "Avengers Infinity War"},
                new Movie { Id = 2, Name = "Fast & Furious 8"}
            };
        }

        public ActionResult Detail(int Id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == Id);

            if (movie == null)
                return HttpNotFound();


            return View(movie);
        }


        public ActionResult New()
        {
            var viewModel = new MovieFormViewModel
            {
                Genres = _context.Genres.ToList()                
            };
            return View("MovieForm",viewModel);
        }

        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };
                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.InStock = movie.InStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }

        public ActionResult Edit(int Id)
        {
            var movieInDb = _context.Movies.Single(m => m.Id == Id);

            if (movieInDb == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel(movieInDb)
            {
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }
    }
}