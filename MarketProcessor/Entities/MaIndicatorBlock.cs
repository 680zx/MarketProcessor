namespace MarketProcessor.Entities
{
    public class MaIndicatorBlock : BaseIndicatorBlock
    {
        public double EmaValue { get; set; }

        public void Clone(MaIndicatorBlock maIndicatorBlock)
        {
            this.CandleStickChartId = maIndicatorBlock.CandleStickChartId;
            this.CandleStickVolume = maIndicatorBlock.CandleStickVolume;
            this.EmaValue = maIndicatorBlock.EmaValue;
        }
    }
}
