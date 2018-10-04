using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxEstimator.DataModel
{
    [JsonObject]
    public class StockAwardVest
    {
        [JsonProperty]
        [JsonRequired]
        public double StockAwardIncome;
    }
}
