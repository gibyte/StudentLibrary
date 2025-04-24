using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentLibrary.Data;

namespace StudentLibrary.Pages
{
    [Authorize(Roles = "Admin")]// авторизация
    public class BookDeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public BookDeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
            return RedirectToPage("Books");
        }
    }
}
