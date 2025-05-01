using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using StudentLibrary.Hubs;

namespace StudentLibrary.Pages
{
    public class PrivacyModel(ILogger<PrivacyModel> logger, IHubContext<ChatHub> hubContext) : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger = logger;
        private readonly IHubContext<ChatHub> _hubContext = hubContext;

        public void OnGet()
        {
            _hubContext.Clients.All.SendAsync("Receive", "usr", "msg");
            _hubContext.Clients.All.SendAsync("SendMessage", "usr", "msg");   
        }
    }
}
