using System.ComponentModel.DataAnnotations;

namespace StudentLibrary.Model
{
    public class Student
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Имя обязательно")]
        public required string Name { get; set; }
        
        [EmailAddress(ErrorMessage = "Введите корректный Email")]
        public string? Email { get; set; }
        
        [Phone(ErrorMessage = "Введите корректный номер телефона")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Дата рождения обязательна")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Выберите пол")]
        public string Gender { get; set; } = "Male"; // "Male" или "Female"

        public bool IsActive { get; set; } = true;

        [Range(1, 5, ErrorMessage = "Выберите курс от 1 до 5")]
        public int Course { get; set; }

        public string? Address { get; set; }

        [Required(ErrorMessage = "Выберите факультет")]
        public string Faculty { get; set; } = "IT";

        public string? Comments { get; set; }

    }
}
