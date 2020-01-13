using System.Threading.Tasks;
using VladyslavOrlovPromo.Core.Dtos;

namespace VladyslavOrlovPromo.Repositories.Interfaces
{
    public interface IRankingRepository
    {
        Task<RankingDto> FetchSingleRanking();

        Task<RankingDto> FetchDoubleRanking();
    }
}