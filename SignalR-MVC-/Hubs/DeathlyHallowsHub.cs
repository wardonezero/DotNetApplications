using Microsoft.AspNetCore.SignalR;
using SignalRMVC.Models;

namespace SignalRMVC.Hubs;

public class DeathlyHallowsHub: Hub
{
    public Dictionary<DeathlyHallowType, int> GetRaceStatus() => StaticDetails.DealthyHallowRace;
}
