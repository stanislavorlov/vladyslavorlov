using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using VladyslavOrlovPromo.Core.Exceptions;
using VladyslavOrlovPromo.Repositories.Interfaces;

namespace VladyslavOrlovPromo.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly HttpClient _httpClient;

        public RequestRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> SendHttpGetRequestAsync(string requestUrl, CancellationToken cancellationToken)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, requestUrl))
            using (var response = await _httpClient.SendAsync(request, cancellationToken))
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                    return content;

                throw new NetworkException(content)
                {
                    StatusCode = (int)response.StatusCode
                };
            }
        }
    }
}
