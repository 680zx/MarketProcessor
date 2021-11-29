namespace MarketProcessor.Entities
{
    public class VolumeIndicatorBlock : BaseIndicatorBlock
    {
        public double CandleStickVolume { get; set; }
        public bool IsVolumeAnomaly { get; set; }
    }
}
