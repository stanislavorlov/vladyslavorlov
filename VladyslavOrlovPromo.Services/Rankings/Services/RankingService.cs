using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;
using VladyslavOrlovPromo.Core.Configs;
using VladyslavOrlovPromo.Core.Entities;
using VladyslavOrlovPromo.Core.Enums;
using VladyslavOrlovPromo.Repositories.Interfaces;
using VladyslavOrlovPromo.Services.Rankings.Interfaces;

namespace VladyslavOrlovPromo.Services.Rankings.Services
{
    public class RankingService : IRankingService
    {
        private readonly PlayerProfileConfiguration _playerProfileConfiguration;
        private readonly IPlayerOverviewFactory _playerOverviewFactory;
        private readonly IRequestRepository _requestRepository;

        public RankingService(IOptions<PlayerProfileConfiguration> playerProfileOptions,
            IPlayerOverviewFactory playerOverviewFactory,
            IRequestRepository requestRepository)
        {
            _playerProfileConfiguration = playerProfileOptions.Value;
            _requestRepository = requestRepository;
            _playerOverviewFactory = playerOverviewFactory;
        }

        public async Task<PlayerOverview> GetPlayerOverviewAsync(MatchTypeCode matchTypeCode, CancellationToken cancellationToken)
        {
            var requestUrl = _playerProfileConfiguration.RankQuery;
            var playerId = _playerProfileConfiguration.PlayerId;

            var rankingUrl = string.Format(requestUrl, matchTypeCode, playerId);

            var response = await _requestRepository.SendHttpGetRequestAsync(rankingUrl, cancellationToken);

            return _playerOverviewFactory.Create(response);
        }
    }
}
