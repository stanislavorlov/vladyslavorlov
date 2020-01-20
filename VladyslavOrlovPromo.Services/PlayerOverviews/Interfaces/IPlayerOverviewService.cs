using System.Threading;
using System.Threading.Tasks;
using VladyslavOrlovPromo.Core.Entities;
using VladyslavOrlovPromo.Core.Enums;

namespace VladyslavOrlovPromo.Services.PlayerOverviews.Interfaces
{
    public interface IPlayerOverviewService
    {
        Task<PlayerOverview> GetPlayerOverviewAsync(MatchTypeCode matchTypeCode, CancellationToken cancellationToken);
    }
}