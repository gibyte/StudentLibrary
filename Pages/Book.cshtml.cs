using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentLibrary.Data;
using StudentLibrary.Model;
using System;

namespace StudentLibrary.Pages
{
    [Authorize(Roles = "Admin")]// авторизация
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
                Book = new Book();
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
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
    }
}
