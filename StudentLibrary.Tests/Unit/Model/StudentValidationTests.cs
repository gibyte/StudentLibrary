using StudentLibrary.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentLibrary.Tests.Unit.Model
{
    public class StudentValidationTests
    {
        [Fact]
        public void Student_WithValidData_ShouldBeValid()
        {
            var student = new Student
            {
                Name = "Иван Иванов",
                Email = "ivan@example.com",
                Phone = "+79001234567",
                BirthDate = new DateTime(2000, 5, 20),
                Gender = "Male",
                Course = 3,
                Faculty = "IT"
            };

            var context = new ValidationContext(student);
            var result = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(student, context, result, true);

            Assert.True(isValid);
        }

        [Fact]
        public void Student_WithInvalidEmail_ShouldBeInvalid()
        {
            var student = new Student
            {
                Name = "Мария",
                Email = "invalid-email",
                BirthDate = DateTime.Today.AddYears(-20),
                Gender = "Female",
                Course = 2,
                Faculty = "IT"
            };

            var context = new ValidationContext(student);
            var result = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(student, context, result, true);

            Assert.False(isValid);
            Assert.Contains(result, r => r.ErrorMessage == "Введите корректный Email");
        }
    }
}
