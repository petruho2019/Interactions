using Microsoft.AspNetCore.SignalR;

namespace Not.Application.Hubs;

public class NotificationHub : Hub
{
    static readonly List<User> Users = [];
    public async Task SendNotification(string str, string? recieverUsername)
    {
        if (string.IsNullOrWhiteSpace(recieverUsername))
        {
            await Clients.Others.SendAsync("ReceiveMessage", str);
        }
        else
        {
            var connectionId = Users?.LastOrDefault(u => u.Name == recieverUsername)?.ConnectionId;

            if (string.IsNullOrEmpty(connectionId))  // TODO тут теряем уведомления, если пользователь не подключен. Нужно сохранять их в БД и отправлять при следующем подключении
                return;

            await Clients.Client(connectionId).SendAsync("ReceiveMessage", recieverUsername, str);
        }
    }

    public override async Task OnConnectedAsync()
    {
        var id = Context.ConnectionId;
        var username = Context.User.Identity?.Name;

        if (!Users.Any(x => x.ConnectionId == id))
        {
            Users.Add(new User { ConnectionId = id, Name = username! });
        }
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var id = Context.ConnectionId;
        Users.RemoveAll(u => u.ConnectionId == id);
        return Task.CompletedTask;
    }
}

record User
{
    public string ConnectionId { get; set; }
    public string Name { get; set; }
}