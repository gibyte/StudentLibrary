using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using StudentLibrary.Data;
using StudentLibrary.Hubs;
using StudentLibrary.Model;
using System;

namespace StudentLibrary.Pages
{
    [Authorize(Roles = "Admin")]// авторизация
    public class BookModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<ChatHub> _hubContext;

        public BookModel(ApplicationDbContext context, IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
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
        public async Task<IActionResult> OnPostAsync()
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

            await _context.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync("BookUpdated", Book);

            return RedirectToPage("Books");
        }
    }
}
