using Newtonsoft.Json;

namespace TaxEstimator.DataModel
{
    [JsonObject]
    public class PerPaycheckSettings
    {
        [JsonProperty]
        [JsonRequired]
        public int Month;

        [JsonProperty]
        [JsonRequired]
        public int Day;

        [JsonProperty]
        public double Bonus;

        [JsonProperty]
        public int StockAwards;
    }
}
