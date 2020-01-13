using System.Net.Http;
using VladyslavOrlovPromo.Repositories.Interfaces;
using System.Threading.Tasks;
using System.Text.Json;
using VladyslavOrlovPromo.Core.Dtos;

namespace VladyslavOrlovPromo.Repositories
{
    public class RankingRepository : IRankingRepository
    {
        private const int PlayerId = 800367967;
        private readonly HttpClient _httpClient;

        public RankingRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RankingDto> FetchSingleRanking()
        {
            var singleRankUrl = $"https://www.itftennis.com/Umbraco/Api/PlayerApi/" +
                $"GetPlayerOverview?circuitCode=MT&matchTypeCode=S&playerId={PlayerId}";

            var responseString = await _httpClient.GetStringAsync(singleRankUrl);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            return JsonSerializer.Deserialize<RankingDto>(responseString, options);
        }

        public async Task<RankingDto> FetchDoubleRanking()
        {
            var doublesRankUrl = $"https://www.itftennis.com/Umbraco/Api/PlayerApi/" +
                $"GetPlayerOverview?circuitCode=MT&matchTypeCode=D&playerId={PlayerId}";

            var responseString = await _httpClient.GetStringAsync(doublesRankUrl);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            return JsonSerializer.Deserialize<RankingDto>(responseString, options);
        }
    }
}