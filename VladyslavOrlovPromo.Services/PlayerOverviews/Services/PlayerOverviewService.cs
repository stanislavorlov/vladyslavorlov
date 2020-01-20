using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;
using VladyslavOrlovPromo.Core.Configs;
using VladyslavOrlovPromo.Core.Entities;
using VladyslavOrlovPromo.Core.Enums;
using VladyslavOrlovPromo.Repositories.Interfaces;
using VladyslavOrlovPromo.Services.PlayerOverviews.Interfaces;

namespace VladyslavOrlovPromo.Services.PlayerOverviews.Services
{
    public class PlayerOverviewService : IPlayerOverviewService
    {
        private readonly PlayerOverviewConfiguration _playerOverviewConfiguration;
        private readonly IPlayerOverviewFactory _playerOverviewFactory;
        private readonly IRequestRepository _requestRepository;

        public PlayerOverviewService(IOptions<PlayerOverviewConfiguration> playerOverviewOptions,
            IPlayerOverviewFactory playerOverviewFactory,
            IRequestRepository requestRepository)
        {
            _playerOverviewConfiguration = playerOverviewOptions.Value;
            _requestRepository = requestRepository;
            _playerOverviewFactory = playerOverviewFactory;
        }

        public async Task<PlayerOverview> GetPlayerOverviewAsync(MatchTypeCode matchTypeCode, CancellationToken cancellationToken)
        {
            var requestUrl = string.Format(_playerOverviewConfiguration.RequestUrl, matchTypeCode, _playerOverviewConfiguration.PlayerId);

            var responseContent = await _requestRepository.SendHttpGetRequestAsync(requestUrl, cancellationToken);

            return _playerOverviewFactory.Create(responseContent);
        }
    }
}
