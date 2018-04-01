using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {

        public ActionResult Index()
        {
            var movies = GetMovies();
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

        public ActionResult Edit(int Id = 10, string optionalstr = "default string",int optionalint = 10)
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

    }
}