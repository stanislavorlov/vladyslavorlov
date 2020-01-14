using System.Linq;
using System.Text.Json;
using VladyslavOrlovPromo.Core.Dtos;
using VladyslavOrlovPromo.Core.Entities;

namespace VladyslavOrlovPromo.Services.Rankings
{
    public class PlayerOverviewFactory : IPlayerOverviewFactory
    {
        public PlayerOverview Create(string jsonString)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                IgnoreNullValues = true,
            };

            var rankingDto = JsonSerializer.Deserialize<RankingDto>(jsonString, options);

            return new PlayerOverview(
                rankingDto.CareerHighRankings.First(n => n.Name.Contains("ATP")).Rank,
                rankingDto.CareerHighRankings.First(n => n.Name.Contains("ITF")).Rank,
                rankingDto.CareerHighRankings.First(n => n.Name.Contains("ATP")).Date,
                rankingDto.CareerHighRankings.First(n => n.Name.Contains("ITF")).Date,
                rankingDto.Rankings.First(n => n.Name.Contains("ATP")).Rank,
                rankingDto.Rankings.First(n => n.Name.Contains("ITF")).Rank);
        }
    }
}