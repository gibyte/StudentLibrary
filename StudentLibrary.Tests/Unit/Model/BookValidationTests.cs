using StudentLibrary.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentLibrary.Tests.Unit.Model
{
    public class BookValidationTests
    {
        // Тест проверяет, что при корректных данных книга проходит валидацию.
        [Fact]
        public void Book_WithValidData_ShouldBeValid()
        {
            // Создаем объект книги с валидными значениями.
            var book = new Book
            {
                Title = "C# in Depth",     // Обязательное поле, строка < 100 символов
                Author = "Jon Skeet",      // Обязательное поле, строка < 100 символов
                Year = 2020,               // В пределах допустимого диапазона 1000–2100
                IsAvailable = true         // Допустимое булево значение
            };

            // Создаем контекст валидации на основе объекта
            var context = new ValidationContext(book);

            // Сюда будут записаны ошибки валидации, если они есть
            var result = new List<ValidationResult>();

            // Проводим валидацию объекта с учетом всех атрибутов [Required], [Range] и т.п.
            var isValid = Validator.TryValidateObject(book, context, result, true);

            // Ожидаем, что валидация прошла успешно (все поля корректны)
            Assert.True(isValid);

            // Также убеждаемся, что список ошибок пуст
            Assert.Empty(result);
        }

        // Тест проверяет, что если не указать заголовок, то объект будет невалиден.
        [Fact]
        public void Book_WithEmptyTitle_ShouldBeInvalid()
        {
            // Создаем объект книги с пустым заголовком
            var book = new Book
            {
                Title = "",                // Пустая строка — нарушает правило [Required]
                Author = "Jon Skeet",      // Корректное значение
                Year = 2020                // Корректное значение
                                           // IsAvailable не указывается, но по умолчанию true
            };

            var context = new ValidationContext(book);
            var result = new List<ValidationResult>();

            // Выполняем валидацию
            var isValid = Validator.TryValidateObject(book, context, result, true);

            // Ожидаем, что объект не прошел валидацию
            Assert.False(isValid);

            // Проверяем, что ошибка касается конкретно заголовка
            Assert.Contains(result, r => r.ErrorMessage == "Требуется заголовок.");
        }
    }
}
