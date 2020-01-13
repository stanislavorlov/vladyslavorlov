using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using VladyslavOrlovPromo.Core.Dtos;
using VladyslavOrlovPromo.Core.Entities;
using VladyslavOrlovPromo.Core.Enums;

namespace VladyslavOrlovPromo.Services.Rankings
{
    public class RankingService : IRankingService
    {
        private readonly IConfiguration _configuration;
        private readonly IPlayerOverviewFactory _playerOverviewFactory;
        private readonly HttpClient _httpClient;

        public RankingService(IConfiguration configuration, IPlayerOverviewFactory playerOverviewFactory, HttpClient httpClient)
        {
            this._configuration = configuration;
            this._httpClient = httpClient;
            this._playerOverviewFactory = playerOverviewFactory;
        }

        public async Task<PlayerOverview> GetPlayerOverviewAsync(MatchTypeCode matchTypeCode)
        {
            var requestUrl = _configuration.GetSection("PlayerSettings").GetSection("rankQuery").Value;
            var playerId = _configuration.GetSection("PlayerSettings").GetSection("playerId").Value;

            var rankingUrl = string.Format(requestUrl, matchTypeCode, playerId);

            var responseString = await _httpClient.GetStringAsync(rankingUrl);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            var rankings = JsonSerializer.Deserialize<RankingDto>(responseString, options);

            return _playerOverviewFactory.Create(rankings);
        }
    }
}
