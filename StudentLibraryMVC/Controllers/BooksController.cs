using Microsoft.AspNetCore.Mvc;
using StudentLibraryMVC.Model;

namespace StudentLibraryMVC.Controllers
{
    public class BooksController : Controller
    {
        public IActionResult Index()
        {
            var books = new List<Book> {
            new Book { Id = 1, Title = "Test", Author = "Author", Year = 2000, IsAvailable = true }};

            return View(books);
        }
    }
}
