using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BOOKSTORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailssController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailssController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetails>>> GetAll()
        {
            return await _context.OrderDetailss.AsNoTracking().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetails>> Get(int id)
        {
            var entity = await _context.OrderDetailss.FindAsync(id);
            if (entity == null) return NotFound();
            return entity;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<OrderDetails>> Create(OrderDetails entity)
        {
            _context.OrderDetailss.Add(entity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = (int)entity.Id }, entity);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OrderDetails entity)
        {
            if (id != (int)entity.Id) return BadRequest();
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.OrderDetailss.FindAsync(id);
            if (entity == null) return NotFound();
            _context.OrderDetailss.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}