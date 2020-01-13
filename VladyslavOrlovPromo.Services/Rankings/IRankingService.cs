using System.Threading.Tasks;
using VladyslavOrlovPromo.Core.Entities;
using VladyslavOrlovPromo.Core.Enums;

namespace VladyslavOrlovPromo.Services.Rankings
{
    public interface IRankingService
    {
        Task<PlayerOverview> GetPlayerOverviewAsync(MatchTypeCode matchTypeCode);
    }
}