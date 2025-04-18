using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StudentLibrary.Pages
{
    [Authorize]
    public class ChatModel : PageModel
    {
        public string UserName { get; set; } = "Аноним";

        public void OnGet()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                UserName = User.Identity.Name!;
            }
        }
    }
}
