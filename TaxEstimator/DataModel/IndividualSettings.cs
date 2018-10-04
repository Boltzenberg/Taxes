using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace TaxEstimator.DataModel
{
    [JsonObject]
    public class IndividualSettings
    {
        public static IndividualSettings Deserialize(string path)
        {
            return JsonConvert.DeserializeObject<IndividualSettings>(File.ReadAllText(path));
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        [JsonProperty]
        [JsonRequired]
        public double AnnualSalaryBeforeSeptember;

        [JsonProperty]
        [JsonRequired]
        public double AnnualSalaryAfterSeptember;

        [JsonProperty]
        [JsonRequired]
        public double PreTax401kWithholdingRate;

        [JsonProperty]
        [JsonRequired]
        public double StayFitTaxable;

        [JsonProperty]
        [JsonRequired]
        public double DisabilityInsuranceTaxable;

        [JsonProperty]
        [JsonRequired]
        public double ImputedLifeInsurance;

        /// <summary>
        /// Amount of HSA lump sum withheld at the beginning of the year until 
        /// </summary>
        [JsonProperty]
        [JsonRequired]
        public double HSALumpSum;

        [JsonProperty]
        [JsonRequired]
        public double FederalTaxRate;

        [JsonProperty]
        [JsonRequired]
        public double AfterTax401kWithholdingRate;

        [JsonProperty]
        [JsonRequired]
        public double ESPPWithholdingRate;

        [JsonProperty]
        [JsonRequired]
        public double LegalPlanPaycheckFee;

        [JsonProperty]
        public List<PerPaycheckSettings> PerPaycheckSettings;
    }
}
