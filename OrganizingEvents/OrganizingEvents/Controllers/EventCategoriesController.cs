using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using OrganizingEvents.Data;
using OrganizingEvents.Models;

namespace OrganizingEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventCategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public EventCategoriesController(ApplicationDbContext db)
        {
            _db = db;
        }

        //ListAll
        [HttpGet]
        [Route("GetAllList")]
        public async Task<IActionResult> GetAsync()
        {
            var category = await _db.EventCategories.ToListAsync();
            return Ok(category);
        }


        //GetById
        [HttpGet]
        [Route("GetEventCategoriesById")]
        public async Task<IActionResult> GetEventCategoriesByIdAsync(int Id)
        {
            var category = await _db.EventCategories.FindAsync(Id);
            return Ok(category);
        }


        //Add
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> PostAsync(EventCategories category)
        {
            _db.EventCategories.Add(category);
            await _db.SaveChangesAsync();
            return Created($"/GetEventCategoriesById/{category.Id}", category);
        }
    }
}
