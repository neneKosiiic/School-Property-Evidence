using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolPropertyEvidence.Data;
using SchoolPropertyEvidence.Models;

namespace SchoolPropertyEvidence.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RoomsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetRooms()
        {
            var rooms = _context.Rooms.ToList();
            return Ok(rooms);
        }

        [HttpGet("withcounts")]
        public async Task<IActionResult> GetRoomsWithCounts()
        {
            var rooms = await _context.Rooms
                .Include(r => r.ResponsiblePerson)
                .Include(r => r.Items).ThenInclude(i => i.Category)
                .Select(r => new RoomWithCountsDto
                {
                    Id = r.Id,
                    RoomName = r.RoomName,
                    ResponsibleFirstName = r.ResponsiblePerson != null ? r.ResponsiblePerson.FirstName : string.Empty,
                    ResponsibleLastName = r.ResponsiblePerson != null ? r.ResponsiblePerson.LastName : string.Empty,
                    // porovnání jmen kategorií case-insensitive; upravte texty podle vaší DB
                    Computers = r.Items.Count(i => i.Category != null && i.Category.CategoryName.ToLower() == "Počítače"),
                    Furniture = r.Items.Count(i => i.Category != null && i.Category.CategoryName.ToLower() == "Nábytek"),
                    Electronics = r.Items.Count(i => i.Category != null && i.Category.CategoryName.ToLower() == "Elektronika"),
                    LearningEquipment = r.Items.Count(i => i.Category != null && i.Category.CategoryName.ToLower() == "Učební pomůcky")
                })
                .ToListAsync();

            return Ok(rooms);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null) return NotFound();

            var hasItems = await _context.Items.AnyAsync(i => i.RoomId == id);
            if (hasItems)
            {
                return BadRequest("Room contains items and cannot be deleted. Remove or reassign items first.");
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
