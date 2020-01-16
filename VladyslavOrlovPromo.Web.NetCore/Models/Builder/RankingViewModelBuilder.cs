using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using VladyslavOrlovPromo.Core.Entities;
using VladyslavOrlovPromo.Core.Enums;
using VladyslavOrlovPromo.Services.Rankings.Interfaces;

namespace VladyslavOrlovPromo.Web.NetCore.Models.Builder
{
    internal class RankingViewModelBuilder : IRankingViewModelBuilder
    {
        private readonly RankingViewModel _rankingViewModel;
        private readonly IRankingService _rankingService;

        public RankingViewModelBuilder(IRankingService rankingService)
        {
            _rankingViewModel = new RankingViewModel();
            _rankingService = rankingService;
        }

        public async Task BuildSinglesPartAsync(CancellationToken cancellationToken)
        {
            var singlesOverview = await _rankingService.GetPlayerOverviewAsync(MatchTypeCode.Singles, cancellationToken);

            BuildRankingPart(singlesOverview, MatchTypeCode.Singles);
        }

        public async Task BuildDoublesPartAsync(CancellationToken cancellationToken)
        {
            var doublesOverview = await _rankingService.GetPlayerOverviewAsync(MatchTypeCode.Doubles, cancellationToken);

            BuildRankingPart(doublesOverview, MatchTypeCode.Doubles);
        }

        public RankingViewModel GetRankingView()
        {
            return _rankingViewModel;
        }

        private void BuildRankingPart(PlayerOverview playerOverview, MatchTypeCode matchTypeCode)
        {
            Type rankingVmType = typeof(RankingViewModel);
            PropertyInfo propertyInfo = rankingVmType.GetProperty(matchTypeCode.Title);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(_rankingViewModel, new RankingType
                {
                    Current = new RankingPartialDisplay
                    {
                        LeftTitle = $"ATP {matchTypeCode.Title} Ranking",
                        LeftRank = playerOverview.AtpCurrent,
                        RightTitle = $"ITF {matchTypeCode.Title} Ranking",
                        RightRank = playerOverview.ItfCurrent
                    },
                    Highest = new RankingPartialDisplay
                    {
                        LeftTitle = $"ATP {matchTypeCode.Title} Ranking ({playerOverview.AtpCareerHighDate.ToString("dd MMM yyyy")})",
                        LeftRank = playerOverview.AtpCareerHigh,
                        RightTitle = $"ITF {matchTypeCode.Title} Ranking ({playerOverview.ItfCareerHighDate.ToString("dd MMM yyyy")})",
                        RightRank = playerOverview.ItfCareerHigh
                    }
                });
            }
        }
    }
}
