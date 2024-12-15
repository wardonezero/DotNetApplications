using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRMVC.Hubs;
using SignalRMVC.Models;

namespace SignalRMVC.Controllers;

public class HomeController(IHubContext<DeathlyHallowsHub> deathlyHub) : Controller
{
    private readonly IHubContext<DeathlyHallowsHub> _deathlyHub = deathlyHub;

    public async Task <IActionResult> VoteDeathlyHallows(string type)
    {
        try
        {
            var deathlyHallowType = Enum.Parse<DeathlyHallowType>(type);
            if (StaticDetails.DealthyHallowRace.TryGetValue(deathlyHallowType, out int value))
            {
                StaticDetails.DealthyHallowRace[deathlyHallowType] = ++value;
            }
            else
            {
                return BadRequest("Hallow type not found in the race dictionary.");
            }
            await _deathlyHub.Clients.All.SendAsync("UpdateDeathlyhallowsCount",
                StaticDetails.DealthyHallowRace[DeathlyHallowType.Cloak],
                StaticDetails.DealthyHallowRace[DeathlyHallowType.Stone],
                StaticDetails.DealthyHallowRace[DeathlyHallowType.Wand]);
        }
        catch (ArgumentException)
        {
            return View("DeathlyHallows");
        }
        return View("DeathlyHallows");

    }

    public IActionResult Index() => View();

    public IActionResult Privacy() => View();
    public IActionResult DeathlyHallows() => View();
    public IActionResult HarryPotter() => View();

    public IActionResult Notifications() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
