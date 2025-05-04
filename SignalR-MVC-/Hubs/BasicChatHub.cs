using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRMVC.Data;

namespace SignalRMVC.Hubs;

public class BasicChatHub(ApplicationDbContext context) : Hub
{
    private readonly ApplicationDbContext _context = context;

    public async Task SendMessageToAll(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
    [Authorize]
    public async Task SendMessageTo(string sender, string reciver, string message)
    {
        string? userId = _context.Users.FirstOrDefault(u => u.Email == reciver)?.Id;
        if (!string.IsNullOrEmpty(userId))
        {
            await Clients.User(userId).SendAsync("ReceiveMessage", sender, message);
        }
    }
}
