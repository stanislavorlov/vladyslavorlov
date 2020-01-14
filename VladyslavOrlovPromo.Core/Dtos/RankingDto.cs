using System;

namespace VladyslavOrlovPromo.Core.Dtos
{
    public class RankingItemDto
    {
        public string Name { get; set; }

        public int Rank { get; set; }

        public string Date { get; set; }
    }

    public class RankingDto
    {
        public RankingItemDto[] Rankings { get; set; }

        public RankingItemDto[] CareerHighRankings { get; set; }
    }
}