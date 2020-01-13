using System.Linq;
using VladyslavOrlovPromo.Core.Dtos;
using VladyslavOrlovPromo.Core.Entities;

namespace VladyslavOrlovPromo.Services.Rankings
{
    public class PlayerOverviewFactory : IPlayerOverviewFactory
    {
        public PlayerOverview Create(RankingDto rankingDto)
        {
            return new PlayerOverview(
                rankingDto.CareerHighRankings.First(n => n.Name.Contains("ATP")).Rank,
                rankingDto.Rankings.First(n => n.Name.Contains("ATP")).Rank,
                rankingDto.CareerHighRankings.First(n => n.Name.Contains("ITF")).Rank,
                rankingDto.Rankings.First(n => n.Name.Contains("ITF")).Rank);
        }
    }
}
