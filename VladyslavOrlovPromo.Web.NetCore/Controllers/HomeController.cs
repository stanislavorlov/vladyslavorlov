using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VladyslavOrlovPromo.Core.Models;
using VladyslavOrlovPromo.Repositories;
using VladyslavOrlovPromo.Repositories.Interfaces;

namespace VladyslavOrlovPromo.Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRankingRepository _rankingRepository;

        public HomeController(ILogger<HomeController> logger, IRankingRepository rankingRepository)
        {
            _logger = logger;
            _rankingRepository = rankingRepository;
        }

        public async Task<IActionResult> Index()
        {
            //SliderRepository sliderRepository = new SliderRepository();
            //var result = await sliderRepository.Fetch();

            var singlesResult = await _rankingRepository.FetchSingleRanking();
            var doublesResult = await _rankingRepository.FetchDoubleRanking();

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
