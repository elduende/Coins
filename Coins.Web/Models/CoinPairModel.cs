namespace Coins.Web.Models
{
    public class CoinPairModel
    {
        public string Pair { get; set; }
        public float Rate { get; set; }
        public float Limit { get; set; }
        public float Min { get; set; }
        public float MinerFree { get; set; }
    }
}