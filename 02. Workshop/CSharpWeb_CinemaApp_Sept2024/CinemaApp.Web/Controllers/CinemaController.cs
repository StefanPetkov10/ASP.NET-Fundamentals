using CinemaApp.Data;
using CinemaApp.Data.Models;
using CinemaApp.Web.ViewModels.Cinema;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index()
        {
            IEnumerable<CinemaIndexViewModel> allCinemas = this.dbContext
                .Cinemas
                .Select(c => new CinemaIndexViewModel()
                {
                    Id = c.Id.ToString(),
                    Name = c.Name,
                    Location = c.Location
                })
                .ToList();

            return View(allCinemas);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AddCinemaFromModel inputModel)
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

            this.dbContext.Cinemas.Add(cinema);
            this.dbContext.SaveChanges();

            return this.RedirectToAction(nameof(Index));
        }
    }
}
