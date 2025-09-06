using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BOOKSTORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Get all books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAll()
        {
            return await _context.Books
                                 .AsNoTracking()
                                 .Include(b => b.Genre) // يجيب النوع مع الكتاب
                                 .ToListAsync();
        }

        // ✅ Get book by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> Get(int id)
        {
            var entity = await _context.Books
                                       .Include(b => b.Genre)
                                       .FirstOrDefaultAsync(b => b.Id == id);

            if (entity == null)
                return NotFound($"No book found with Id = {id}");

            return entity;
        }

        // ✅ Create new book (Admin only)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Book>> Create([FromBody] Book entity)
        {
            if (entity == null)
                return BadRequest("Book data is required.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // ✅ تحقق أن GenreId موجود
            //var genreExists = await _context.Genres.AnyAsync(g => g.GenreId == entity.GenreId);
            var genreExists = await _context.Genres.AnyAsync(g => g.GenreId == entity.GenreId);
            if (!genreExists)
                return BadRequest($"Invalid GenreId {entity.GenreId}. Please use a valid GenreId.");

            try
            {
                _context.Books.Add(entity);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // ✅ Update existing book (Admin only)
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Book entity)
        {
            if (id != entity.Id)
                return BadRequest("The provided Id does not match the book Id.");

            // تحقق أن GenreId موجود
            var genreExists = await _context.Genres.AnyAsync(g => g.GenreId == entity.GenreId);
            if (!genreExists)
                return BadRequest($"Invalid GenreId {entity.GenreId}. Please use a valid GenreId.");

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Books.Any(e => e.Id == id))
                    return NotFound($"No book found with Id = {id}");
                throw;
            }
        }

        // ✅ Delete book (Admin only)
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.Books.FindAsync(id);
            if (entity == null)
                return NotFound($"No book found with Id = {id}");

            _context.Books.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
