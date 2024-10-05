using CinemaApp.Data;
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
    }
}
