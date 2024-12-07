using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SignalRMVC.Models;

namespace SignalRMVC.Controllers
{
    public class HomeController(ILogger<HomeController> logger) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;

        public IActionResult DeathlyHallows(string type)
        {
            try
            {
                var deathlyHallowType = Enum.Parse<DeathlyHallowType>(type);
                if (StaticDetails.DealthyHallowRace.ContainsKey(deathlyHallowType))
                {
                    StaticDetails.DealthyHallowRace[deathlyHallowType]++;
                }
                else
                {
                    return BadRequest("Hallow type not found in the race dictionary.");
                }
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
