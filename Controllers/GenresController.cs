using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BOOKSTORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GenresController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetAll()
        {
            return await _context.Genres.AsNoTracking().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> Get(int id)
        {
            var entity = await _context.Genres.FindAsync(id);
            if (entity == null) return NotFound();
            return entity;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Genre>> Create(Genre entity)
        {
            _context.Genres.Add(entity);
            int v = await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = (int)entity.GenreId }, entity);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Genre entity)
        {
            if (id != (int)entity.GenreId) return BadRequest();
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.Genres.FindAsync(id);
            if (entity == null) return NotFound();
            _context.Genres.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}