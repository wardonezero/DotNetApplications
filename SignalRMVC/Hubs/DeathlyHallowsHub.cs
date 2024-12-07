using Microsoft.AspNetCore.SignalR;

namespace SignalRMVC.Hubs;

public class DeathlyHallowsHub: Hub
{
    public Dictionary<DeathlyHallowType, int> GetRaceStatus() => StaticDetails.DealthyHallowRace;
}
