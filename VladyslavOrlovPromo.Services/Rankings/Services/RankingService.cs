using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;
using VladyslavOrlovPromo.Core.Configs;
using VladyslavOrlovPromo.Core.Entities;
using VladyslavOrlovPromo.Core.Enums;
using VladyslavOrlovPromo.Core.Exceptions;
using VladyslavOrlovPromo.Services.Rankings.Interfaces;

namespace VladyslavOrlovPromo.Services.Rankings.Services
{
    public class RankingService : IRankingService
    {
        private readonly PlayerProfileConfiguration _playerProfileConfiguration;
        private readonly IPlayerOverviewFactory _playerOverviewFactory;
        private readonly HttpClient _httpClient;

        public RankingService(IOptions<PlayerProfileConfiguration> playerProfileOptions,
            IPlayerOverviewFactory playerOverviewFactory,
            HttpClient httpClient)
        {
            _playerProfileConfiguration = playerProfileOptions.Value;
            _httpClient = httpClient;
            _playerOverviewFactory = playerOverviewFactory;
        }

        public async Task<PlayerOverview> GetPlayerOverviewAsync(MatchTypeCode matchTypeCode)
        {
            var requestUrl = _playerProfileConfiguration.RankQuery;
            var playerId = _playerProfileConfiguration.PlayerId;

            var rankingUrl = string.Format(requestUrl, matchTypeCode, playerId);

            using (var request = new HttpRequestMessage(HttpMethod.Get, rankingUrl))
            using (var response = await _httpClient.SendAsync(request))
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                    return _playerOverviewFactory.Create(content);

                throw new NetworkException(content)
                {
                    StatusCode = (int)response.StatusCode
                };
            }
        }
    }
}
