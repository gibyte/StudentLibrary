using Microsoft.AspNetCore.Mvc;

namespace StudentLibrary.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : Controller
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentData(int id)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"http://localhost:5202/api/students/details/{id}");
            var data = await response.Content.ReadAsStringAsync();

            return Content(data);
        }

    }
}
