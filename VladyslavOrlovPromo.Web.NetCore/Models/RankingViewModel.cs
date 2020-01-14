namespace VladyslavOrlovPromo.Web.NetCore.Models
{
    public class RankingViewModel
    {
        public RankingType Singles { get; set; }

        public RankingType Doubles { get; set; }
    }

    public class RankingType
    {
        public RankingItem Itf { get; set; }

        public RankingItem Atp { get; set; }
    }

    public class RankingItem
    {
        public int Current { get; set; }

        public int Highest { get; set; }

        public string HighestDate { get; set; }
    }
}