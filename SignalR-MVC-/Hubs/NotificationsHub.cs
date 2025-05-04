using Microsoft.AspNetCore.SignalR;

namespace SignalRMVC.Hubs;

public class NotificationsHub : Hub
{
    private static int NotificationsCount;
    private readonly static List<string> Notifications = [];

    public async Task SendNotification(string message)
    {
        if (!string.IsNullOrEmpty(message))
        {
            Notifications.Add(message);
            NotificationsCount = Notifications.Count;
            await GetNotifications();
        }
    }

    public async Task GetNotifications()
    {
        await Clients.All.SendAsync("LoadNotification", Notifications, NotificationsCount);
    }
}
