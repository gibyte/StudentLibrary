using Microsoft.AspNetCore.Mvc;

namespace StudentLibrary.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentController : ControllerBase
    {
        private static List<string> students = new() { "Иван", "Мария", "Анна" };

        [HttpGet] // Получить список студентов
        public IActionResult GetStudents()
        {
            return Ok(students);
        }

        [HttpPost] // Добавить студента
        public IActionResult AddStudent([FromBody] string name)
        {
            students.Add(name);
            return Created("", name);
        }

        [HttpPut("{id}")] // Изменить данные студента
        public IActionResult UpdateStudent(int id, [FromBody] string name)
        {
            if (id >= students.Count) return NotFound();
            students[id] = name;
            return NoContent();
        }

        [HttpDelete("{id}")] // Удалить студента
        public IActionResult DeleteStudent(int id)
        {
            if (id >= students.Count) return NotFound();
            students.RemoveAt(id);
            return NoContent();
        }
    }

}
