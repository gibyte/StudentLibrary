using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentLibrary.Data;
using StudentLibrary.Model.AuthApp;

namespace StudentLibrary.Pages.Account
{
    [Authorize(Roles = "Admin")]
    public class ProfilesModel : PageModel
    {
        
        private readonly ApplicationDbContext _context;
        public ProfilesModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<AuthUser> Users { get; set; } = new();

        public void OnGet()
        {
            Users = _context.AuthUsers.ToList();
        }
        
    }
}
