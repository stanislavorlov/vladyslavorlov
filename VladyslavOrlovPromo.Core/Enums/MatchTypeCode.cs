namespace VladyslavOrlovPromo.Core.Enums
{
    public class MatchTypeCode
    {
        private MatchTypeCode(string typeCode, string title)
        {
            Value = typeCode;
            Title = title;
        }

        public string Value { get; }

        public string Title { get; }

        public static MatchTypeCode Singles { get { return new MatchTypeCode("S", nameof(Singles)); } }

        public static MatchTypeCode Doubles { get { return new MatchTypeCode("D", nameof(Doubles)); } }

        public override string ToString()
        {
            return Value;
        }
    }
}