using System.ComponentModel.DataAnnotations;

namespace StudentLibrary.Model.AuthApp
{
    public class AuthUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "User"; // По умолчанию обычный пользователь

    }
}
