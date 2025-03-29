using System.Security.Claims;

namespace StudentLibrary.Model
{
    public class LoginModel
    {
        public ClaimsIdentity? Username { get; internal set; }
        public bool RememberMe { get; internal set; }
    }
}