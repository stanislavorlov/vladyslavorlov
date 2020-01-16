using System.Threading;
using System.Threading.Tasks;
using VladyslavOrlovPromo.Core.Entities;
using VladyslavOrlovPromo.Core.Enums;

namespace VladyslavOrlovPromo.Services.Rankings.Interfaces
{
    public interface IRankingService
    {
        Task<PlayerOverview> GetPlayerOverviewAsync(MatchTypeCode matchTypeCode, CancellationToken cancellationToken);
    }
}