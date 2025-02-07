using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentLibrary.Data;
using StudentLibrary.Model;

namespace StudentLibrary.Pages
{
    public class BookModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public BookModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; }

        public void OnGet(int id)
        {
            if (id > 0)
            {
                Book = _context.Books.FirstOrDefault(b => b.Id == id);
            }
            else
            {
                Book = new Book() { Author = "", Title = ""};
            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Book.Id == 0)
                {
                    _context.Books.Add(Book);
                }
                else
                {
                    _context.Books.Update(Book);
                }
                _context.SaveChanges();
                return RedirectToPage("Books");
            }
            return Page();
        }
    }
}
