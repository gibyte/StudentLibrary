using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentLibrary.Data;
using StudentLibrary.Model;

namespace StudentLibrary.Pages
{
    [Authorize(Roles = "Admin,User")]// авторизация
    public class StudentModel(ApplicationDbContext context) : PageModel
    {
        private readonly ApplicationDbContext _context = context;

        [BindProperty]
        public required Student Student { get; set; }

        public void OnGet(int id)
        {
            if (id > 0)
            {
                Student = _context.Students.FirstOrDefault(b => b.Id == id);
            }
            else
            {
                Student = new Student() { Name = "Новый" };
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Student.Id == 0)
            {
                _context.Students.Add(Student);
            }
            else
            {
                _context.Students.Update(Student);
            }
            _context.SaveChanges();
            return RedirectToPage("Students");
        }
    }
}
