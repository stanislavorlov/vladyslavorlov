using VladyslavOrlovPromo.Core.Entities;

namespace VladyslavOrlovPromo.Services.Rankings.Interfaces
{
    public interface IPlayerOverviewFactory
    {
        PlayerOverview Create(string json);
    }
}
