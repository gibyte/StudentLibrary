using static System.Reflection.Metadata.BlobBuilder;

namespace StudentLibrary.Model
{
    public class Loan
    {
        public int Id { get; set; } // Уникальный идентификатор выдачи книги. Каждая запись в базе данных имеет свой Id, который используется как первичный ключ.
        public int BookId { get; set; } //Идентификатор книги, которая была выдана студенту. Это внешний ключ, который связывает запись Loan с конкретной книгой в таблице Books.
        public required Book Book { get; set; } //Навигационное свойство для связи с моделью Book. Позволяет получить информацию о книге, которая была выдана. required = обязательное поле
        public int StudentId { get; set; } // Идентификатор студента, который взял книгу. Это внешний ключ, который связывает запись Loan с конкретным студентом в таблице Students.
        public required Student Student { get; set; } // Навигационное свойство для связи с моделью Student. Позволяет получить информацию о студенте, который взял книгу.
        public DateTime LoanDate { get; set; } = DateTime.Now; //По умолчанию устанавливается в текущее время (DateTime.Now), когда создаётся новая запись.
        public DateTime? ReturnDate { get; set; } //Дата возврата книги. Может быть null, если книга ещё не возвращена. Как только студент вернёт книгу, это поле заполняется соответствующей датой.
    }
}
