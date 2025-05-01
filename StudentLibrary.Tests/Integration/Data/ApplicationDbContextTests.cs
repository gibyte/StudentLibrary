using Microsoft.EntityFrameworkCore; // Используется для работы с Entity Framework Core.
using StudentLibrary.Data; // Контекст базы данных.
using StudentLibrary.Model.AuthApp; // Модели для аутентификации.
using StudentLibrary.Model;

namespace StudentLibrary.Tests.Integration.Data
{
    public class ApplicationDbContextTests
    {
        // Метод для получения экземпляра контекста базы данных с уникальным именем базы данных.
        // Это нужно, чтобы каждый тест использовал свою отдельную БД в памяти.
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_" + Guid.NewGuid()) // Уникальное имя базы данных для каждого теста
                .Options;

            // Создаём и возвращаем новый экземпляр контекста с заданными параметрами
            return new ApplicationDbContext(options);
        }

        // Юнит-тест, который проверяет возможность добавления и чтения сущностей в контексте.
        [Fact]
        public void CanInsertAndReadEntities()
        {
            // Arrange: подготовка данных
            var context = GetDbContext(); // Создание контекста базы данных

            // Создание тестовых данных для книг, студентов и займов
            var book = new Book { Title = "Test Book", Author = "Author A", Year = 2022 }; // Создание объекта книги
            var student = new Student
            {
                Name = "Test Student", // Имя студента
                Email = "student@example.com", // Email студента
                Phone = "1234567890", // Телефон
                BirthDate = new DateTime(2000, 1, 1), // Дата рождения
                Gender = "Male", // Пол
                Course = 3, // Курс
                Faculty = "IT" // Факультет
            };
            // var authUser = new AuthUser { Id = Guid.NewGuid().ToString(), UserName = "user1", Email = "user1@mail.com" }; // Закомментированный объект для аутентифицированного пользователя.
            var loan = new Loan { Book = book, Student = student, LoanDate = DateTime.Today }; // Связь между книгой и студентом через займ.

            // Act: выполнение операции
            context.Books.Add(book); // Добавление книги в контекст
            context.Students.Add(student); // Добавление студента в контекст
            // context.AuthUsers.Add(authUser); // Закомментированное добавление пользователя в контекст
            context.Loans.Add(loan); // Добавление займа в контекст
            context.SaveChanges(); // Сохранение изменений в базе данных

            // Assert: проверка результатов
            // Проверка, что все добавленные сущности сохранились в базе данных
            Assert.Single(context.Books); // Должна быть ровно одна книга в базе
            Assert.Single(context.Students); // Должен быть ровно один студент в базе
            // Assert.Single(context.AuthUsers); // Проверка на наличие одного пользователя (закомментировано)
            Assert.Single(context.Loans); // Должен быть ровно один займ в базе

            // Проверка, что данные о займе были сохранены корректно
            var savedLoan = context.Loans.Include(l => l.Book).Include(l => l.Student).FirstOrDefault(); // Получаем первый (и единственный) займ из базы с включением данных о книге и студенте.
            Assert.NotNull(savedLoan); // Проверяем, что займ не null
            Assert.Equal("Test Book", savedLoan.Book.Title); // Проверка, что заголовок книги в займе соответствует ожидаемому
            Assert.Equal("Test Student", savedLoan.Student.Name); // Проверка, что имя студента в займе соответствует ожидаемому
        }
    }
}
