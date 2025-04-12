using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentLibrary.Data;
using StudentLibrary.Model;

namespace StudentLibrary.Pages
{
    [Authorize]// авторизация
    public class StudentsModel(ApplicationDbContext context) : PageModel
    {
        private readonly ApplicationDbContext _context = context;

        public List<Student> Students { get; set; } = [];

        public void OnGet()
        {
            Students = _context.Students.ToList();
        }

        public IActionResult OnPostDelete(int id)
        {
            var student = _context.Students.Find(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
            return RedirectToPage();
        }

    }
}
