using Microsoft.AspNetCore.Mvc;
using StudentLibraryMVC.Model;

namespace StudentLibraryMVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksApiController : Controller
    {
    
        private static readonly List<Book> books = new()
        {
            new Book { Id = 1, Title = "Test", Author = "Author", Year = 2000, IsAvailable = true }
        };

        [HttpGet]
        public IActionResult Get() => Ok(books);

        [HttpPost]
        public IActionResult Post(Book book)
        {
            book.Id = books.Max(b => b.Id) + 1;
            books.Add(book);
            return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
        }
    }
}
