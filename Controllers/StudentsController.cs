using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentLibrary.Data;
using StudentLibrary.Model;

namespace StudentLibrary.Controllers
{
    [Authorize]
    [Route("controller/students")] // Этот маршрут будет использоваться для всех действий
    public class StudentsController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        // GET: controller/students
        [HttpGet("")]
        public IActionResult Index()
        {
            var students = _context.Students.ToList();
            return View(students);
        }

        // POST: controller/students/delete/{id}
        [HttpPost("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var student = _context.Students.Find(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: controller/students/edit/{id}
        [HttpGet("edit/{id?}")]
        public IActionResult Edit(int? id)
        {
            if (id.HasValue && id > 0)
            {
                var student = _context.Students.FirstOrDefault(s => s.Id == id);
                if (student == null) return NotFound();
                return View(student);
            }
            return View(new Student() { Name = "Новый студент" });
        }

        // POST: controller/students/edit
        [HttpPost("edit")]
        public IActionResult Edit(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }

            if (student.Id == 0)
            {
                _context.Students.Add(student);
            }
            else
            {
                _context.Students.Update(student);
            }
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
