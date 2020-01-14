using VladyslavOrlovPromo.Core.Dtos;
using VladyslavOrlovPromo.Core.Entities;

namespace VladyslavOrlovPromo.Services.Rankings
{
    public interface IPlayerOverviewFactory
    {
        PlayerOverview Create(string json);
    }
}
