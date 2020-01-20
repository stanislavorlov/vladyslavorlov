using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using VladyslavOrlovPromo.Core.Enums;
using VladyslavOrlovPromo.Services.PlayerOverviews.Interfaces;
using VladyslavOrlovPromo.Web.NetCore.Models.Builder;

namespace VladyslavOrlovPromo.Web.NetCore.Components
{
    public class ShowRankSummary : ViewComponent
    {
        private readonly IPlayerOverviewService _rankingService;
        private readonly IRankingViewModelBuilder _rankVMBuilder;

        public ShowRankSummary(IPlayerOverviewService rankingService, IRankingViewModelBuilder rankingViewModelBuilder)
        {
            _rankingService = rankingService;
            _rankVMBuilder = rankingViewModelBuilder;
        }

        public async Task<IViewComponentResult> InvokeAsync(string matchType, CancellationToken cancellationToken)
        {
            MatchTypeCode matchTypeCode = MatchTypeCode.Parse(matchType);
            var playerOverview = await _rankingService.GetPlayerOverviewAsync(matchTypeCode, cancellationToken);

            var rankingVm = _rankVMBuilder.GetRankingViewModel(playerOverview, matchTypeCode);

            return View(rankingVm);
        }
    }
}