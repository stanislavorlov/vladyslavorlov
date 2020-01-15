using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using VladyslavOrlovPromo.Core.Enums;
using VladyslavOrlovPromo.Services.Rankings.Interfaces;
using VladyslavOrlovPromo.Web.NetCore.Models;

namespace VladyslavOrlovPromo.Web.NetCore.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly IRankingService _rankingService;

        public ProfileController(ILogger<ProfileController> logger, IRankingService rankingService)
        {
            _logger = logger;
            _rankingService = rankingService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            _logger.LogInformation("Profile/Index action gets called");

            var singlesOverview = await _rankingService.GetPlayerOverviewAsync(MatchTypeCode.S);
            var doublesOverview = await _rankingService.GetPlayerOverviewAsync(MatchTypeCode.D);

            var rankViewModel = new RankingViewModel
            {
                Singles = new RankingType
                {
                    Atp = new RankingItem
                    {
                        Current = singlesOverview.AtpCurrent,
                        Highest = singlesOverview.AtpCareerHigh,
                        HighestDate = singlesOverview.AtpCareerHighDate.ToString("dd MMM yyyy")
                    },
                    Itf = new RankingItem
                    {
                        Current = singlesOverview.ItfCurrent,
                        Highest = singlesOverview.ItfCareerHigh,
                        HighestDate = singlesOverview.ItfCareerHighDate.ToString("dd MMM yyyy")
                    }
                },
                Doubles = new RankingType
                {
                    Atp = new RankingItem
                    {
                        Current = doublesOverview.AtpCurrent,
                        Highest = doublesOverview.AtpCareerHigh,
                        HighestDate = doublesOverview.AtpCareerHighDate.ToString("dd MMM yyyy")
                    },
                    Itf = new RankingItem
                    {
                        Current = doublesOverview.ItfCurrent,
                        Highest = doublesOverview.ItfCareerHigh,
                        HighestDate = doublesOverview.ItfCareerHighDate.ToString("dd MMM yyyy")
                    }
                }
            };

            return View(rankViewModel);
        }
    }
}