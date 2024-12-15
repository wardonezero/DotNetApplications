using Microsoft.AspNetCore.SignalR;

namespace SignalRMVC.Hubs;

public class HouseGroupHub : Hub
{
    public static List<string> HouseGroups = new List<string>();

    public async Task SubscribeToHouse(string houseName)
    {
        if (!HouseGroups.Contains(Context.ConnectionId + ":" + houseName))
        {
            HouseGroups.Add(Context.ConnectionId + ":" + houseName);
            string houseList = "";
            foreach (var str in HouseGroups)
            {
                if (str.Contains(Context.ConnectionId))
                {
                    houseList += str.Split(':')[1] + " ";
                }
            }
            await Clients.Caller.SendAsync("SubscriptionStatus", houseList, houseName, true);

            await Groups.AddToGroupAsync(Context.ConnectionId, houseName);
        }
    }

    public async Task UnsubscribeFromHouse(string houseName)
    {
        if (HouseGroups.Contains(Context.ConnectionId + ":" + houseName))
        {
            HouseGroups.Remove(Context.ConnectionId + ":" + houseName);

            string houseList = "";
            foreach (var str in HouseGroups)
            {
                if (str.Contains(Context.ConnectionId))
                {
                    houseList += str.Split(':')[1] + " ";
                }
            }
            await Clients.Caller.SendAsync("SubscriptionStatus", houseList, houseName, false);

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, houseName);
        }
    }
}
