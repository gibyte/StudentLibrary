using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Security.Principal;
using StudentLibrary.Model;

namespace StudentLibrary.Hubs
{
    public class ChatHub : Hub
    {
        // Потокобезопасный словарь, хранящий информацию о подключенных пользователях
        private static readonly ConcurrentDictionary<string, string> OnlineUsers = new();
        //Используется ConcurrentDictionary, так как SignalR-хабы могут обслуживать несколько клиентов одновременно, и доступ к коллекции должен быть потокобезопасным.
        //Ключ — это ConnectionId, значение — имя пользователя (User.Identity.Name) или сам ConnectionId, если пользователь не аутентифицирован.


        //Метод OnConnectedAsync() вызывается автоматически при подключении клиента к хабу.       
        public override async Task OnConnectedAsync()
        {
            var userName = Context.User?.Identity?.Name ?? Context.ConnectionId;
            //Извлекается имя пользователя из контекста(Context.User?.Identity?.Name).
            //Если пользователь не аутентифицирован, используется ConnectionId как имя.
            OnlineUsers[Context.ConnectionId] = userName;
            //Добавление пользователя в OnlineUsers по его ConnectionId.

            await Clients.All.SendAsync("UserListUpdate", OnlineUsers.Values.Distinct().ToList());
            //После подключения рассылается всем клиентам обновлённый список пользователей (UserListUpdate).
            //Distinct() используется, чтобы исключить дубликаты имён, если пользователь подключён из нескольких вкладок.
            await base.OnConnectedAsync(); //Вызов базового метода завершает подключение.
            //
        }

        //Метод вызывается при отключении клиента.
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            OnlineUsers.TryRemove(Context.ConnectionId, out _); //Удаляется пользователь по ConnectionId из словаря OnlineUsers.
            await Clients.All.SendAsync("UserListUpdate", OnlineUsers.Values.Distinct().ToList());//После отключения обновлённый список онлайн-пользователей отправляется всем клиентам.
            await base.OnDisconnectedAsync(exception); //завершает процесс.
        }

        //Метод вызывается клиентами для отправки сообщения.
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("Receive", user, message); //Сообщение транслируется всем подключённым пользователям.
        }

        public async Task SendBookUpdate(Book book)
        {
            await Clients.All.SendAsync("BookUpdated", book);
        }

    }
}
