using System;
using VladyslavOrlovPromo.Core.Enums;

namespace VladyslavOrlovPromo.Core.Entities
{
    public class PlayerOverview
    {
        protected MatchTypeCode MatchTypeCode;

        public PlayerOverview(int atpHigh, int itfHigh, string atpHighDate, string itfHighDate, int atpCurrent, int itfCurrent)
        {
            AtpCareerHigh = atpHigh;
            ItfCareerHigh = itfHigh;
            AtpCurrent = atpCurrent;
            ItfCurrent = itfCurrent;

            if (DateTime.TryParse(atpHighDate, out DateTime temp))
                AtpCareerHighDate = temp;
            else
                AtpCareerHighDate = default;

            if (DateTime.TryParse(itfHighDate, out temp))
                ItfCareerHighDate = temp;
            else
                ItfCareerHighDate = default;
        }

        public int AtpCareerHigh { get; private set; }
        
        public DateTime AtpCareerHighDate { get; private set; }

        public int ItfCareerHigh { get; private set; }

        public DateTime ItfCareerHighDate { get; private set; }

        public int AtpCurrent { get; private set; }

        public int ItfCurrent { get; private set; }
    }
}