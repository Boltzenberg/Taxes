using System;

namespace TaxEstimator.DataModel
{
    public class PayPeriodStats
    {
        // Income
        public double Wages { get; protected set; }
        public double Bonus { get; protected set; }

        // Pre-tax expenses
        public double PreTax401k { get; protected set; }
        public double HSA { get; protected set; }
        public double TaxableEarnings { get; protected set; }

        // Stock
        public double StockAwards { get; protected set; }

        // Taxes
        public double Federal { get; protected set; }
        public double SocialSecurity { get; protected set; }
        public double Medicare { get; protected set; }

        // After Tax
        public double AfterTax401k { get; protected set; }
        public double ESPP { get; protected set; }
    }
}
