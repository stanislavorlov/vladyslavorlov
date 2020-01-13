namespace VladyslavOrlovPromo.Core.Entities
{
    public class PlayerOverview
    {
        public PlayerOverview(int atpHigh, int itfHigh, int atpCurrent, int itfCurrent)
        {
            AtpCareerHigh = atpHigh;
            ItfCareerHigh = itfHigh;
            AtpCurrent = atpCurrent;
            ItfCurrent = itfCurrent;
        }

        public int AtpCareerHigh { get; private set; }

        public int ItfCareerHigh { get; private set; }

        public int AtpCurrent { get; private set; }

        public int ItfCurrent { get; private set; }
    }
}