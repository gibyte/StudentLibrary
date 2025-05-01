using Microsoft.EntityFrameworkCore;
using StudentLibrary.Data;
using StudentLibrary.Model;
using StudentLibrary.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentLibrary.Tests.Integration.Pages
{
    // Класс для юнит-тестов страницы BooksModel
    public class BooksModelTests
    {
        // Метод создает In-Memory базу данных EF Core с тестовыми данными
        private ApplicationDbContext GetDbContext()
        {
            // Создаем уникальную базу данных в памяти для каждого теста
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // важно: уникальное имя
                .Options;

            // Создаем контекст БД
            var context = new ApplicationDbContext(options);

            // Добавляем тестовые книги
            context.Books.AddRange(
                new Book { Title = "C# Basics", Author = "John Doe", Year = 2020 },
                new Book { Title = "EF Core Guide", Author = "Jane Smith", Year = 2021 }
            );

            // Сохраняем изменения в базу
            context.SaveChanges();

            return context; // возвращаем контекст с предзаполненными данными
        }

        // Юнит-тест проверяет, что метод OnGet загружает книги в модель
        [Fact]
        public void OnGet_ShouldLoadBooks()
        {
            // Arrange — подготавливаем тестовые данные и модель
            var context = GetDbContext();          // получаем фейковый контекст с книгами
            var model = new BooksModel(context);   // создаем экземпляр модели страницы

            // Act — вызываем метод OnGet, который должен заполнить список книг
            model.OnGet();

            // Assert — проверяем, что результат соответствует ожиданиям
            Assert.NotNull(model.Books);                 // список должен быть не null
            Assert.Equal(2, model.Books.Count);          // в списке должно быть ровно 2 книги
            Assert.Contains(model.Books, b => b.Title == "C# Basics");  // проверка по названию
            Assert.Contains(model.Books, b => b.Author == "Jane Smith"); // проверка по автору
        }
    }
}
