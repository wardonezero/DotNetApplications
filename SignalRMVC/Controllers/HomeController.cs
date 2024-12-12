using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRMVC.Hubs;
using SignalRMVC.Models;

namespace SignalRMVC.Controllers
{
    public class HomeController(ILogger<HomeController> logger,IHubContext<DeathlyHallowsHub> deathlyHub) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly IHubContext<DeathlyHallowsHub> _deathlyHub = deathlyHub;

        public async Task <IActionResult> DeathlyHallows(string type)
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
                return View("Index");
            }
            return Accepted();

        }

        public IActionResult Index() => View();

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
