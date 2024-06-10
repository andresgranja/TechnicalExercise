using Microsoft.AspNetCore.Mvc;
using TechnicalExercise.Application;
using TechnicalExercise.Core.Entities;

namespace TechnicalExercise.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookService bookService;

        public BooksController(BookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooksAsync()
        {
            var books = await bookService.GetAllBooksAsync();
            if (books == null || !books.Any())
                return NotFound("No books found.");
            return Ok(books);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Book>> AddBookAsync([FromBody] Book book)
        {
            await bookService.AddBookAsync(book);
            return CreatedAtAction(nameof(GetBookByIdAsync), new { id = book.BookId }, book);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Book>> GetBookByIdAsync(int id)
        {
            var book = await bookService.GetBookByIdAsync(id);
            if (book == null)
                return NotFound();
            return Ok(book);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateBookAsync(int id, [FromBody] Book book)
        {
            if (id != book.BookId)
                return BadRequest("Book ID mismatch");

            await bookService.UpdateBookAsync(book);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBookAsync(int id)
        {
            await bookService.DeleteBookAsync(id);
            return NoContent();
        }
    }
}
