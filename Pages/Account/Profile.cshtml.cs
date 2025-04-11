using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StudentLibrary.Pages.Account
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        public string UserEmail { get; set; }

        public void OnGet()
        {
            UserEmail = User.Identity?.Name ?? "Неизвестный пользователь";
        }
    }
}
