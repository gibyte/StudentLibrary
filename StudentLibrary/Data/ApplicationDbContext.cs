using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StudentLibrary.Model;
using StudentLibrary.Model.AuthApp;

namespace StudentLibrary.Data
{
    public class ApplicationDbContext : DbContext
    {
        //Конструктор, который принимает параметры конфигурации базы данных.
        // — Передаёт эти параметры в базовый класс DbContext с помощью : base(options).
        // — Это позволяет настраивать подключение к базе через Dependency Injection.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //Database.Migrate(); // Автоматически применяет миграции и создаёт базу, если её нет
        } 

        public DbSet<Book> Books { get; set; } //таблица Books, содержащая данные о книгах.
        public DbSet<Student> Students { get; set; } //таблица Students, содержащая данные о студентах.
        public DbSet<Loan> Loans { get; set; } //таблица Loans, хранящая информацию о выдачах книг студентам.
        public DbSet<AuthUser> AuthUsers { get; set; }

    }
}
