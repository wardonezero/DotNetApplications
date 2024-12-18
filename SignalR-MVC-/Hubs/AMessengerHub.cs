using Microsoft.AspNetCore.SignalR;
using SignalRMVC.Data;
using System.Security.Claims;

namespace SignalRMVC.Hubs;

public class AMessengerHub(ApplicationDbContext context) : Hub
{
    private readonly ApplicationDbContext _context = context;
    public override Task OnConnectedAsync()
    {
        var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!string.IsNullOrEmpty(userId))
        {
            var userName = _context.Users.FirstOrDefault(u => u.Id == userId)?.UserName;
            AddUserConnection(userId, Context.ConnectionId);
            Clients.Users(OnlineUsers()).SendAsync("ReciveUserConnection", userId, userName);
        }
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!string.IsNullOrEmpty(userId))
        {
            if (Users.ContainsKey(userId))
            {
                Users[userId].Remove(Context.ConnectionId);

                if (!Users[userId].Any())
                {
                    Users.Remove(userId);
                    var userName = _context.Users.FirstOrDefault(u => u.Id == userId)?.UserName;
                    Clients.Users(OnlineUsers()).SendAsync("ReciveUserDisconnection", userId, userName);
                }
            }
        }

        return base.OnDisconnectedAsync(exception);

        /*//if (HasUserConnection(userId, Context.ConnectionId))
        //{
        //    var UserConnections = Users[userId];
        //    UserConnections.Remove(userId);
        //    Users.Remove(Context.ConnectionId);
        //    if (UserConnections.Any())
        //        Users.Add(userId, UserConnections);
        //}

        //if (!string.IsNullOrEmpty(userId))
        //{
        //    var userName = _context.Users.FirstOrDefault(u => u.Id == userId)?.UserName;
        //    Clients.Users(OnlineUsers()).SendAsync("ReciveUserDisconnection", userId, userName);
        //    //AddUserConnection(userId, Context.ConnectionId);
        //}
        //return base.OnDisconnectedAsync(exception);*/
    }

    public async Task AddPrivateChat(int maxChats, int privateChatId, string privateChatName)
    {
        var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        var userName = _context.Users.FirstOrDefault(u => u.Id == userId)?.UserName;
        await Clients.All.SendAsync("ReciveAddPrivateChat", maxChats, privateChatId, privateChatName, userId, userName);
    }

    public async Task DeletePrivateChat(int deleted, int selected, string privateChatName)
    {
        var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        var userName = _context.Users.FirstOrDefault(u => u.Id == userId)?.UserName;
        await Clients.All.SendAsync("ReciveDeletePrivateChat", deleted, selected, privateChatName, userName);
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
    public static Dictionary<string, List<string>> Users = [];

    public static bool HasUserConnection(string userId, string connectionId)
    {
        if (!string.IsNullOrEmpty(userId) && Users.ContainsKey(userId))
        {
            return Users[userId].Contains(connectionId);
        }
        return false;
        /*try
        {
            if (Users.ContainsKey(userId))
                return Users[userId].Any(p => p.Contains(connectionId));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message + " Error in HasUserConnection");
        }
        return false;*/
    }

    public static bool HasUser(string UserId)
    {
        try
        {
            if (Users.ContainsKey(UserId))
                return Users[UserId].Any();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message+ "Error in HasUser");
        }
        return false;
    }

    public static void AddUserConnection(string userId, string connectionId)
    {
        if (!string.IsNullOrEmpty(userId))
        {
            if (Users.ContainsKey(userId))
            {
                if (!Users[userId].Contains(connectionId))
                {
                    Users[userId].Add(connectionId);
                }
            }
            else
            {
                Users.Add(userId, new List<string> { connectionId });
            }
        }
        else
        {
            Console.WriteLine("Error in AddUserConnection");
        }
    }

    public static List<string> OnlineUsers() => Users.Keys.ToList();
    #endregion
}