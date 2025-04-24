using System.ComponentModel.DataAnnotations;

namespace StudentLibrary.Model
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Требуется заголовок.")]
        [StringLength(100, ErrorMessage = "Заголовок не может быть длиннее 100 символов.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Требуется автор.")]
        public string Author { get; set; }

        [Range(1000, 2100, ErrorMessage = "Год должен быть между 1000 и 2100.")]
        public int Year { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}
