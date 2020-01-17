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

        public static MatchTypeCode Parse(string value)
        {
            switch (value.ToLower())
            {
                case "s":
                case "singles":
                    return Singles;
                case "d":
                case "doubles":
                    return Doubles;
                default:
                    return null;
            }
        }

        public override string ToString()
        {
            return Value;
        }
    }
}