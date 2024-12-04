using Microsoft.AspNetCore.SignalR;

namespace SignalRMVC.Hubs;
public class UserCount : Hub
{
    public static byte TotalViewers { get; private set; }

    public async Task NewViewer()
    {
        TotalViewers++;
        await Clients.All.SendAsync("UpdateTotalViewers", TotalViewers);
    }
}