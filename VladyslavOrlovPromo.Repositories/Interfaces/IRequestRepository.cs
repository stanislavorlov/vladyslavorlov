using System.Threading;
using System.Threading.Tasks;

namespace VladyslavOrlovPromo.Repositories.Interfaces
{
    public interface IRequestRepository
    {
        Task<string> SendHttpGetRequestAsync(string requestUrl, CancellationToken cancellationToken);
    }
}
