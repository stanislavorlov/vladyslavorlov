using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using VladyslavOrlovPromo.Core.Enums;
using VladyslavOrlovPromo.Services.Rankings.Interfaces;
using VladyslavOrlovPromo.Web.NetCore.Models.Builder;

namespace VladyslavOrlovPromo.Web.NetCore.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly IRankingService _rankingService;
        private readonly IRankingViewModelBuilder _rankingModelBuilder;

        public ProfileController(ILogger<ProfileController> logger, IRankingService rankingService, IRankingViewModelBuilder rankingViewModelBuilder)
        {
            _logger = logger;
            _rankingService = rankingService;
            _rankingModelBuilder = rankingViewModelBuilder;
        }

        public async Task<IActionResult> IndexAsync()
        {
            _logger.LogInformation("Profile/Index action gets called");

            var singlesOverview = await _rankingService.GetPlayerOverviewAsync(MatchTypeCode.S);
            var doublesOverview = await _rankingService.GetPlayerOverviewAsync(MatchTypeCode.D);

            _rankingModelBuilder.BuildSinglesPart(singlesOverview);
            _rankingModelBuilder.BuildDoublesPart(doublesOverview);

            var rankViewModel = _rankingModelBuilder.GetRankingView();

            return View(rankViewModel);
        }
    }
}