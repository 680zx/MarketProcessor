namespace MarketProcessor.Entities
{
    public class VolumeIndicatorBlock : BaseIndicatorBlock
    {
        public double CandleStickVolume { get; set; }
        public bool IsLowVolume { get; set; }
    }
}
