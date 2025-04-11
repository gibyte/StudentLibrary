using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentLibrary.Data;
using StudentLibrary.Model;

namespace StudentLibrary.Pages
{
    [Authorize]
    public class BooksModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public BooksModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Book> Books { get; set; }

        public void OnGet()
        {
            Books = _context.Books.ToList();
        }
    }
}
