using VladyslavOrlovPromo.Core.Entities;
using VladyslavOrlovPromo.Core.Enums;

namespace VladyslavOrlovPromo.Web.NetCore.Models.Builder
{
    internal class RankingViewModelBuilder : IRankingViewModelBuilder
    {
        public RankingViewModel GetRankingViewModel(PlayerOverview playerOverview, MatchTypeCode matchTypeCode)
        {
            return new RankingViewModel
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
            };
        }
    }
}
