using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VladyslavOrlovPromo.Core.Models;
using VladyslavOrlovPromo.Repositories.Interfaces;

namespace VladyslavOrlovPromo.Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISliderRepository _sliderRepository;

        public HomeController(ILogger<HomeController> logger, ISliderRepository sliderRepository)
        {
            _logger = logger;
            _sliderRepository = sliderRepository;
        }

        public async Task<IActionResult> IndexAsync()
        {
            _logger.LogInformation("Home/Index action gets called");

            await _sliderRepository.Fetch();

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
