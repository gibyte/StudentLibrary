using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentLibrary.Data;
using StudentLibrary.Model.AuthApp;

namespace StudentLibrary.Pages.Account
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ProfileModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AuthUser CurrentUser { get; set; }

        public bool IsAdmin => User.IsInRole("Admin");

        public IActionResult OnGet()
        {
            var email = User.Identity?.Name;
            if (email == null) return RedirectToPage("/Account/Login");

            CurrentUser = _context.AuthUsers.FirstOrDefault(u => u.Email == email);
            if (CurrentUser == null) return NotFound();

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var user = _context.AuthUsers.FirstOrDefault(u => u.Id == CurrentUser.Id);
            if (user == null) return NotFound();

            if (User.IsInRole("Admin"))
            {
                user.Role = CurrentUser.Role;
                _context.SaveChanges();
            }

            return RedirectToPage();
        }
    }
}
