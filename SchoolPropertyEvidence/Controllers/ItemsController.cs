using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolPropertyEvidence.Data;

namespace SchoolPropertyEvidence.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var items = await _context.Items
                .Include(i => i.Category)
                .Include(i => i.Room)
                .Select(i => new ItemDto
                {
                    Id = i.Id,
                    InventoryNumber = i.InventoryNumber,
                    ItemName = i.ItemName,
                    Description = i.Description,
                    CategoryId = i.CategoryId,
                    CategoryName = i.Category != null ? i.Category.CategoryName : null,
                    RoomId = i.RoomId,
                    RoomName = i.Room != null ? i.Room.RoomName : null,
                    IsActive = i.IsActive
                })
                .ToListAsync();

            return Ok(items);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null) return NotFound();

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private class ItemDto
        {
            public int Id { get; set; }
            public string InventoryNumber { get; set; } = string.Empty;
            public string ItemName { get; set; } = string.Empty;
            public string? Description { get; set; }
            public int CategoryId { get; set; }
            public string? CategoryName { get; set; }
            public int RoomId { get; set; }
            public string? RoomName { get; set; }
            public bool IsActive { get; set; }
        }
    }
}