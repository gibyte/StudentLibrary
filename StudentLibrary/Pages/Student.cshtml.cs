using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using StudentLibrary.Data;
using StudentLibrary.Hubs;
using StudentLibrary.Model;

namespace StudentLibrary.Pages
{

    //[Authorize(Roles = "Admin,User")]
    public class StudentModel(ApplicationDbContext context, IHubContext<ChatHub> hubContext) : PageModel
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IHubContext<ChatHub> _hubContext = hubContext;

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
                Student = new Student() { Name = "Íîâûé" };
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
            //_hubContext.
            _hubContext.Clients.All.SendAsync("Receive", "usr", "msg");
            return RedirectToPage("Students");
        }
    }
}
