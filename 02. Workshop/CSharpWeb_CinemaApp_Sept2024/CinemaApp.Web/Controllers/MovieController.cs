﻿using System.Globalization;
using CinemaApp.Data;
using CinemaApp.Data.Models;
using CinemaApp.Web.ViewModels.Cinema;
using CinemaApp.Web.ViewModels.Movie;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static CinemaApp.Common.EntityValidationConstants.Movie;


namespace CinemaApp.Web.Controllers
{
    public class MovieController : BaseController
    {
        private readonly CinemaDbContext dbContext;

        public MovieController(CinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Movie> allMovies = await this.dbContext
                .Movies
                .ToArrayAsync();

            return View(allMovies);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddMovieFromModel inputModel)
        {
            // TODO: Add form model + validation
            if (this.ModelState.IsValid == false)
            {
                //Render the same form with user entered values + model errors
                return View(inputModel);
            }

            bool isReleaseDateValid = DateTime.TryParseExact(inputModel.ReleaseDate,
                ReleaseDateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime releseDate);

            if (!isReleaseDateValid)
            {
                this.ModelState.AddModelError(nameof(inputModel.ReleaseDate),
                    "Invalid Release Date. The Release Date must be in the following format: MM/yyyy");
            }

            Movie movie = new Movie
            {
                Title = inputModel.Title,
                Genre = inputModel.Genre,
                ReleaseDate = releseDate,
                Director = inputModel.Director,
                Duration = inputModel.Duration,
                Description = inputModel.Description
            };

            await this.dbContext.Movies.AddAsync(movie);
            await this.dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            Guid movieGuid = Guid.Empty;
            bool isGuidValid = this.IsGuidIdValid(id, ref movieGuid);
            if (!isGuidValid)
            {
                return RedirectToAction(nameof(Index));
            }

            Movie? movie = await this.dbContext.Movies
                .FirstOrDefaultAsync(x => x.Id == movieGuid);

            if (movie == null)
            {
                //Non-existing movie guid
                return RedirectToAction(nameof(Index));
            }

            //if(!this.dbContext.Movies.Any(x => x.Id == guidId))
            //{
            //    return RedirectToAction(nameof(Index));
            //}

            return View(movie);
        }

        [HttpGet]
        public async Task<IActionResult> AddToProgram(string id)
        {
            Guid movieGuid = Guid.Empty;
            bool isGuidValid = this.IsGuidIdValid(id, ref movieGuid);
            if (!isGuidValid)
            {
                return RedirectToAction(nameof(Index));
            }

            Movie? movie = await this.dbContext.Movies
                .FirstOrDefaultAsync(x => x.Id == movieGuid);
            if (movie == null)
            {
                return RedirectToAction(nameof(Index));
            }

            AddMovieToCinemaInputModel viewModel = new AddMovieToCinemaInputModel
            {
                Id = id!,
                MovieTitle = movie.Title,
                Cinemas = await this.dbContext.Cinemas
                .Include(cm => cm.CinemaMovies)
                .ThenInclude(m => m.Movie)
                    .Select(c => new CinemaCheckBoxItemInputModel
                    {
                        Id = c.Id.ToString(),
                        Name = c.Name,
                        IsSelected = c.CinemaMovies
                        .Any(cm => cm.MovieId == movieGuid)
                    })
                    .ToArrayAsync()
            };

            return View(viewModel);
        }
    }
}
