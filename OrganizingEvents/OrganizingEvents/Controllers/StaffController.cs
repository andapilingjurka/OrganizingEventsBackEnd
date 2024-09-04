using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using OrganizingEvents.Data;
using OrganizingEvents.Models;
using System.Threading.Tasks;

namespace OrganizingEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public StaffController(ApplicationDbContext db)
        {
            _db = db;
        }

        // ListAll
        [HttpGet]
        [Route("GetAllStaff")]
        public async Task<IActionResult> GetAsync()
        {
            var staff = await _db.Staff.ToListAsync();
            return Ok(staff);
        }

        // GetById
        [HttpGet]
        [Route("GetStaffById/{id}")]
        public async Task<IActionResult> GetStaffByIdAsync(int id)
        {
            var staffMember = await _db.Staff.FirstOrDefaultAsync(s => s.Id == id);
            if (staffMember == null)
            {
                return NotFound();
            }
            return Ok(staffMember);
        }

        // Add
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> PostAsync(Staff staff)
        {
            _db.Staff.Add(staff);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStaffByIdAsync), new { id = staff.Id }, staff);
        }

        // Update
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> PutAsync(int id, Staff staff)
        {
            if (id != staff.Id)
            {
                return BadRequest();
            }

            _db.Entry(staff).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // Delete
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var staffMember = await _db.Staff.FindAsync(id);
            if (staffMember == null)
            {
                return NotFound();
            }

            _db.Staff.Remove(staffMember);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        private bool StaffExists(int id)
        {
            return _db.Staff.Any(e => e.Id == id);
        }
    }
}
