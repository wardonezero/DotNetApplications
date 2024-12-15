using Microsoft.AspNetCore.SignalR;

namespace SignalRMVC.Hubs;

public class NotificationsHub : Hub
{
    private int NotificationsCount;
    private readonly List<string> Notifications = [];

    public async Task SendNotification(string message)
    {
        if (!string.IsNullOrEmpty(message))
        {
            Notifications.Add(message);
            NotificationsCount = Notifications.Count;
            await Clients.All.SendAsync("ReceiveNotification", message);
        }
    }

    public async Task GetNotifications()
    {
        await Clients.All.SendAsync("LoadNotification", Notifications, NotificationsCount);
    }
}
