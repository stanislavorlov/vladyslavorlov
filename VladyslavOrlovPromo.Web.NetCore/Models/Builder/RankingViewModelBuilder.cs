using VladyslavOrlovPromo.Core.Entities;

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
            _rankingViewModel.Singles = new RankingType
            {
                Current = new RankingPartialDisplay
                {
                    LeftTitle = "ATP Singles Ranking",
                    LeftRank = singles.AtpCurrent,
                    RightTitle = "ITF Singles Ranking",
                    RightRank = singles.ItfCurrent
                },
                Highest = new RankingPartialDisplay
                {
                    LeftTitle = $"ATP Singles Ranking ({singles.AtpCareerHighDate.ToString("dd MMM yyyy")})",
                    LeftRank = singles.AtpCareerHigh,
                    RightTitle = $"ITF Singles Ranking ({singles.ItfCareerHighDate.ToString("dd MMM yyyy")})",
                    RightRank = singles.ItfCareerHigh
                }
            };
        }

        public void BuildDoublesPart(PlayerOverview doubles)
        {
            _rankingViewModel.Doubles = new RankingType
            {
                Current = new RankingPartialDisplay
                {
                    LeftTitle = "ATP Doubles Ranking",
                    LeftRank = doubles.AtpCurrent,
                    RightTitle = "ITF Doubles Ranking",
                    RightRank = doubles.ItfCurrent
                },
                Highest = new RankingPartialDisplay
                {
                    LeftTitle = $"ATP Doubles ({doubles.AtpCareerHighDate.ToString("dd MMM yyyy")})",
                    LeftRank = doubles.AtpCareerHigh,
                    RightTitle = $"ITF Doubles ({doubles.ItfCareerHighDate.ToString("dd MMM yyyy")})",
                    RightRank = doubles.ItfCareerHigh
                }
            };
        }

        public RankingViewModel GetRankingView()
        {
            return _rankingViewModel;
        }
    }
}
