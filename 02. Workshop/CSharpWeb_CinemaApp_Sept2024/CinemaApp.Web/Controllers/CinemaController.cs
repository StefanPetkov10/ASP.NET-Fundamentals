using CinemaApp.Data;
using CinemaApp.Data.Models;
using CinemaApp.Web.ViewModels.Cinema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Web.Controllers
{
    public class CinemaController : Controller
    {
        private readonly CinemaDbContext dbContext;
        public CinemaController(CinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<CinemaIndexViewModel> allCinemas = await this.dbContext
               .Cinemas
               .Select(c => new CinemaIndexViewModel()
               {
                   Id = c.Id.ToString(),
                   Name = c.Name,
                   Location = c.Location
               })
               .OrderBy(c => c.Location)
               .ToArrayAsync();

            return View(allCinemas);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddCinemaFromModel inputModel)
        {
            if (this.ModelState.IsValid == false)
            {
                return View(inputModel);
            }

            Cinema cinema = new Cinema
            {
                Name = inputModel.Name,
                Location = inputModel.Location
            };

            await this.dbContext.Cinemas.AddAsync(cinema);
            await this.dbContext.SaveChangesAsync();

            return this.RedirectToAction(nameof(Index));
        }
    }
}
