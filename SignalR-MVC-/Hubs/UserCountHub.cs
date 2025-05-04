using Microsoft.AspNetCore.SignalR;

namespace SignalRMVC.Hubs;
public class UserCountHub : Hub
{
    public static byte TotalViewers { get; private set; }
    public static byte TotalUseres { get; private set; }

    public async Task NewViewer()
    {
        TotalViewers++;
        await Clients.All.SendAsync("UpdateTotalViewers", TotalViewers);
    }

    public override Task OnConnectedAsync()
    {
        TotalUseres++;
        Clients.All.SendAsync("UpdateTotalUsers", TotalUseres).GetAwaiter().GetResult();
        return base.OnConnectedAsync();
    }
    public override Task OnDisconnectedAsync(Exception? exception)
    {
        TotalUseres--;
        Clients.All.SendAsync("UpdateTotalUsers", TotalUseres).GetAwaiter().GetResult();
        return base.OnDisconnectedAsync(exception);
    }
}