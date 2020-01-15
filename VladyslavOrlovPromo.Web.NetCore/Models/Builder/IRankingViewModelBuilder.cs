using VladyslavOrlovPromo.Core.Entities;

namespace VladyslavOrlovPromo.Web.NetCore.Models.Builder
{
    public interface IRankingViewModelBuilder
    {
        void BuildSinglesPart(PlayerOverview singles);

        void BuildDoublesPart(PlayerOverview doubles);

        RankingViewModel GetRankingView();
    }
}
