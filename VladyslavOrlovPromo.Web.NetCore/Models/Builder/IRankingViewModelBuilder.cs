using System.Threading;
using System.Threading.Tasks;

namespace VladyslavOrlovPromo.Web.NetCore.Models.Builder
{
    public interface IRankingViewModelBuilder
    {
        Task BuildSinglesPartAsync(CancellationToken cancellationToken);

        Task BuildDoublesPartAsync(CancellationToken cancellationToken);

        RankingViewModel GetRankingView();
    }
}