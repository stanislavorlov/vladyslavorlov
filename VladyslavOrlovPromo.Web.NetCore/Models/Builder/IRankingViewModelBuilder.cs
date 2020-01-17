using VladyslavOrlovPromo.Core.Entities;
using VladyslavOrlovPromo.Core.Enums;

namespace VladyslavOrlovPromo.Web.NetCore.Models.Builder
{
    public interface IRankingViewModelBuilder
    {
        RankingViewModel GetRankingViewModel(PlayerOverview playerOverview, MatchTypeCode matchTypeCode);
    }
}