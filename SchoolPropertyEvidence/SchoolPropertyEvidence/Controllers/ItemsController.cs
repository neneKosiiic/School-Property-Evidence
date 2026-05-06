using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolPropertyEvidence.Data;
using SchoolPropertyEvidence.Models;

namespace SchoolPropertyEvidence.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase {
        private readonly ApplicationDbContext _context;

        public ItemsController(ApplicationDbContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemModel>>> GetItems() {
            return await _context.Items
                .Include(i => i.Category)
                .Include(i => i.Room)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemModel>> GetItem(int id) {
            var item = await _context.Items
                .Include(i => i.Category)
                .Include(i => i.Room)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null) {
                return NotFound();
            }

            return item;
        }

        [HttpPost]
        public async Task<ActionResult<ItemModel>> CreateItem(ItemModel item) {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, ItemModel item) {
            if (id != item.Id) {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!ItemExists(id)) {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id) {
            var item = await _context.Items.FindAsync(id);

            if (item == null) {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(int id) {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}