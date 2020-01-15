using System;
using System.Reflection;
using VladyslavOrlovPromo.Core.Entities;
using VladyslavOrlovPromo.Core.Enums;

namespace VladyslavOrlovPromo.Web.NetCore.Models.Builder
{
    internal class RankingViewModelBuilder : IRankingViewModelBuilder
    {
        private readonly RankingViewModel _rankingViewModel;

        public RankingViewModelBuilder()
        {
            _rankingViewModel = new RankingViewModel();
        }

        public void BuildSinglesPart(PlayerOverview singles)
        {
            BuildRankingPart(singles, MatchTypeCode.Singles);
        }

        public void BuildDoublesPart(PlayerOverview doubles)
        {
            BuildRankingPart(doubles, MatchTypeCode.Doubles);
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
