using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRMVC.Data;
using System.Security.Claims;

namespace SignalRMVC.Hubs;

public class AMessengerHub(ApplicationDbContext context) : Hub
{
    private readonly ApplicationDbContext _context = context;
    public override async Task OnConnectedAsync()
    {
        var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!string.IsNullOrEmpty(userId))
        {
            var userName = _context.Users.FirstOrDefault(u => u.Id == userId)?.UserName;
            await Clients.Users(OnlineUsers()).SendAsync("ReciveUserConnection",userId,userName);
        }
    }

    //public async Task SendMessageToAll(string user, string message)
    //{
    //    await Clients.All.SendAsync("ReceiveMessage", user, message);
    //}
    //[Authorize]
    //public async Task SendMessageTo(string sender, string reciver, string message)
    //{
    //    string? userId = _context.Users.FirstOrDefault(u => u.Email == reciver)?.Id;
    //    if (!string.IsNullOrEmpty(userId))
    //    {
    //        await Clients.User(userId).SendAsync("ReceiveMessage", sender, message);
    //    }
    //}
    #region HubConnections
    private static readonly Dictionary<string, List<string>> Users = [];
    public static bool HasUserConnection(string UserId, string ConnectionId)
    {
        try
        {
            if (Users.TryGetValue(UserId, out List<string>? value))
            {
                return value.Any(p => p.Contains(ConnectionId));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return false;
    }
    public static void AddUserConnection(string UserId, string ConnectionId)
    {

        if (!string.IsNullOrEmpty(UserId) && !HasUserConnection(UserId, ConnectionId))
        {
            if (Users.TryGetValue(UserId, out List<string>? value))
                value.Add(ConnectionId);
            else
                Users.Add(UserId, [ConnectionId]);
        }
    }

    public static List<string> OnlineUsers()
    {
        return [.. Users.Keys];
    }
    #endregion
}
