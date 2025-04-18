using Microsoft.AspNetCore.SignalR;

namespace StudentLibrary.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Receive(string user, string message)
        {
            await Clients.All.SendAsync("Receive", user, message);
        }
        public async Task SendMessage(string message, string user)
        {
            await Clients.All.SendAsync("Receive", message, user);
        }
        public async Task UpStudent()
        {
            await Clients.All.SendAsync("UpStudent");
        }    
    }
}
