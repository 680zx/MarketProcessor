namespace MarketProcessor.Entities
{
    public class MacdIndicatorBlock : MaIndicatorBlock
    {
        public double MacdValue { get; set; }
        public double SignalMacdValue { get; set; }
        public double MacdDelta { get; set; }

        public void Clone(MacdIndicatorBlock macdIndicatorBlock)
        {
            this.CandleStickChartId = macdIndicatorBlock.CandleStickChartId;
            this.CandleStickVolume = macdIndicatorBlock.CandleStickVolume;
            this.EmaValue = macdIndicatorBlock.EmaValue;
            this.MacdValue = macdIndicatorBlock.MacdValue;
            this.SignalMacdValue = macdIndicatorBlock.SignalMacdValue;
            this.MacdDelta = macdIndicatorBlock.MacdDelta;
        }
    }
}
