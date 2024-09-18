﻿using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using OrganizingEvents.Data;
using OrganizingEvents.Models;

namespace OrganizingEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public EventsController(ApplicationDbContext db)
        {
            _db = db;
        }

        //ListAll
        [HttpGet]
        [Route("GetAllList")]
        public async Task<IActionResult> GetAsync()
        {
            var events = await _db.Events
                .Include(q => q.EventThemes)
                .Include(q => q.EventCategories)
                .ToListAsync();
            return Ok(events);
        }


        //GetById
        [HttpGet("GetEventById/{id}")]
        public async Task<IActionResult> GetEventsByIdAsync(int id)
        {
            var events = await _db.Events
                .Include(q => q.EventThemes)
                .Include(q => q.EventCategories)
                .FirstOrDefaultAsync(q => q.Id == id);
            if (events == null)
            {
                return NotFound();
            }
            return Ok(events);
        }


        //Add
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> PostAsync(Events events)
        {
            // Kontrollo nëse EventThemes ekziston
            var existingEventThemes = await _db.EventThemes.FindAsync(events.ThemeId);
            if (existingEventThemes == null)
            {
                return NotFound($"EventThemes me ID {events.ThemeId} nuk ekziston.");
            }

            // Kontrollo nëse EventCategories ekziston
            var existingEventCategories = await _db.EventCategories.FindAsync(events.CategoryId);
            if (existingEventCategories == null)
            {
                return NotFound($"EventCategories me ID {events.CategoryId} nuk ekziston.");
            }

            events.EventThemes = existingEventThemes;
            events.EventCategories = existingEventCategories;

            _db.Events.Add(events);
            await _db.SaveChangesAsync();
            return Created($"/GetEventsById/{events.Id}", events);
        }


        //Update
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> PutAsync(Events events)
        {
            // Kontrollo nëse EventThemes ekziston
            var existingEventThemes = await _db.EventThemes.FindAsync(events.ThemeId);
            if (existingEventThemes == null)
            {
                return NotFound($"EventThemes me ID {events.ThemeId} nuk ekziston.");
            }

            // Kontrollo nëse EventCategories ekziston
            var existingEventCategories = await _db.EventCategories.FindAsync(events.CategoryId);
            if (existingEventCategories == null)
            {
                return NotFound($"EventCategories me ID {events.CategoryId} nuk ekziston.");
            }

            events.EventThemes = existingEventThemes;
            events.EventCategories = existingEventCategories;

            _db.Events.Update(events);
            await _db.SaveChangesAsync();
            return NoContent();
        }


        //Delete
        [Route("Delete")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int Id)
        {
            var eventsIdDelete = await _db.Events
                .Include(q => q.EventThemes)
                .Include(q => q.EventCategories)
                .FirstOrDefaultAsync(q => q.Id == Id);

            if (eventsIdDelete == null)
            {
                return NotFound();
            }

            _db.Events.Remove(eventsIdDelete);
            await _db.SaveChangesAsync();
            return NoContent();
        }


        // Search Events by Name
        [HttpGet("SearchEvent")]
        public async Task<IActionResult> SearchEventAsync([FromQuery] string searchEvent)
        {
            if (string.IsNullOrEmpty(searchEvent))
            {
                return BadRequest("Search term cannot be empty.");
            }

            var events = await _db.Events
                .Include(e => e.EventThemes)
                .Include(e => e.EventCategories)
                .Where(e => EF.Functions.Like(e.EventName, $"%{searchEvent}%"))
                .ToListAsync();

            return Ok(events);
        }

    }
}

