using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VladyslavOrlovPromo.Core.Models;
using VladyslavOrlovPromo.Services.Rankings;

namespace VladyslavOrlovPromo.Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRankingService _rankingService;

        public HomeController(ILogger<HomeController> logger, IRankingService rankingService)
        {
            _logger = logger;
            _rankingService = rankingService;
        }

        public async Task<IActionResult> Index()
        {
            var singlesOverview = await _rankingService.GetPlayerOverviewAsync(Enums.MatchTypeCode.S);
            var doublesOverview = await _rankingService.GetPlayerOverviewAsync(Enums.MatchTypeCode.D);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
