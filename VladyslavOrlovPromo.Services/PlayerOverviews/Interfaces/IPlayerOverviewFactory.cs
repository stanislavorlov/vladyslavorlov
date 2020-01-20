using VladyslavOrlovPromo.Core.Entities;

namespace VladyslavOrlovPromo.Services.PlayerOverviews.Interfaces
{
    public interface IPlayerOverviewFactory
    {
        PlayerOverview Create(string json);
    }
}
