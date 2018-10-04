using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxEstimator
{
    public static class Constants
    {
        public const int PaychecksPerYear = 24;

        public static class Month
        {
            public static int September = 9;
        }

        public static class Day
        {
            public static int BonusDay = 15;
        }

        public static class Threshold
        {
            public static double PreTax401kContributions = 18500;
            public static double SocialSecurityCap = 128400;
            public static double MedicareRateChange = 200000;
            public static double AfterTax401kConstributions = 27250;
            public static double ESPPConstributions = 30000;
        }

        public static class TaxRate
        {
            public static double SocialSecurity = 6.2;
            public static double MedicareBelowThreshold = 1.45;
            public static double MedicareAboveThreshold = 2.35;
            public static double BonusFederalTaxRate = 22;
        }
    }
}
