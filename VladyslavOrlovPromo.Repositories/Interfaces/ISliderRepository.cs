using System.Threading.Tasks;

namespace VladyslavOrlovPromo.Repositories.Interfaces
{
    public interface ISliderRepository
    {
        Task<string> Fetch();
    }
}
