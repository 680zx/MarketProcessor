namespace MarketProcessor.Entities
{
    public class MacdIndicatorBlock : BaseIndicatorBlock
    {
        public double MacdValue { get; set; }
        public double SignalMacdValue { get; set; }
        public double MacdDelta { get; set; }
    }
}
