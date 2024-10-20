using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeminarHub.Data;
using SeminarHub.Models;

namespace SeminarHub.Controllers
{
    public class SeminarController : BaseController
    {
        private readonly SeminarHubDbContext data;

        public SeminarController(SeminarHubDbContext context)
        {
            data = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var events = await data.Seminars
               .AsNoTracking()
               .Select(s => new AllSeminarViewModel
               {
                   Id = s.Id,
                   Topic = s.Topic,
                   Lecturer = s.Lecturer,
                   Organizer = s.Organizer.UserName,
                   Category = s.Category.Name,
                   DateAndTime = s.DateAndTime.ToString("dd/MM/yyyy HH:mm")
               })
               .ToArrayAsync();

            return View(events);
        }

        [HttpGet]
        public async Task<IActionResult> Add(int id)
        {
            var model = new AddSeminarViewModel();
            model.Categories = await data.Categories
                .Select(c => new CategoryViewModel
                {
                    Name = c.Name,
                    Id = c.Id
                })
                .ToListAsync();

            return View(model);
        }
    }

}

