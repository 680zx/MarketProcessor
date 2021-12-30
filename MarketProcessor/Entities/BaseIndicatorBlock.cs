namespace MarketProcessor.Entities
{
    public abstract class BaseIndicatorBlock
    {
        public int CandleStickChartId { get; set; }

        public CandleStickChart CandleStickChart { get; set; }

        public double CandleStickVolume { get; set; }
    }
}
