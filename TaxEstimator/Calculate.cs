using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxEstimator.DataModel;

namespace TaxEstimator
{
    public static class Calculate
    {
        public static class Paycheck
        {
            public static double Salary(int month, IndividualSettings settings)
            {
                if (month >= Constants.Month.September)
                {
                    return settings.AnnualSalaryAfterSeptember / Constants.PaychecksPerYear;
                }
                else
                {
                    return settings.AnnualSalaryBeforeSeptember / Constants.PaychecksPerYear;
                }
            }

            public static double Bonus(int month, int day, IndividualSettings settings)
            {
                if (settings.PerPaycheckSettings != null)
                {
                    foreach (PerPaycheckSettings pp in settings.PerPaycheckSettings)
                    {
                        if (pp.Month == month && pp.Day == day && pp.Bonus > 0)
                        {
                            return pp.Bonus;
                        }
                    }
                }

                return 0;
            }

            public static double PreTax401k(double grossPay, IndividualSettings settings, AnnualAggregate aggregate)
            {
                double target = grossPay * (settings.PreTax401kWithholdingRate / 100);
                double allowed = Constants.Threshold.PreTax401kContributions - aggregate.PreTax401k;
                return Math.Min(target, allowed);
            }

            public static double HSA(double grossPay, double preTax401k, IndividualSettings settings, AnnualAggregate aggregate)
            {
                double hsaTarget = settings.HSALumpSum - aggregate.HSA;
                return Math.Min(hsaTarget, grossPay - preTax401k);
            }

            public static double StockAwards(int month, int day, IndividualSettings settings)
            {
                if (settings.PerPaycheckSettings != null)
                {
                    foreach (PerPaycheckSettings pp in settings.PerPaycheckSettings)
                    {
                        if (pp.Month == month && pp.Day == day && pp.StockAwards > 0)
                        {
                            return pp.StockAwards;
                        }
                    }
                }

                return 0;
            }

            public static double AfterTax401k(double earnings, IndividualSettings settings, AnnualAggregate aggregate)
            {
                double target = earnings * (settings.AfterTax401kWithholdingRate / 100);
                double allowed = Constants.Threshold.AfterTax401kConstributions - aggregate.AfterTax401k;
                return Math.Min(target, allowed);
            }

            public static double ESPP(double earnings, IndividualSettings settings, AnnualAggregate aggregate)
            {
                double target = earnings * (settings.ESPPWithholdingRate / 100);
                double allowed = Constants.Threshold.ESPPConstributions - aggregate.ESPP;
                return Math.Min(target, allowed);
            }
        }

        public static class Taxes
        {
            public static double Federal(double taxableEarnings, double bonus, IndividualSettings settings)
            {
                return ((taxableEarnings - bonus) * (settings.FederalTaxRate / 100) + (bonus * (Constants.TaxRate.BonusFederalTaxRate / 100)));
            }

            public static double SocialSecurity(double taxableEarnings, double preTax401k, AnnualAggregate aggregate)
            {
                double cap = Constants.Threshold.SocialSecurityCap * (Constants.TaxRate.SocialSecurity / 100);
                double remaining = cap - aggregate.SocialSecurity;
                double expected = (taxableEarnings + preTax401k) * (Constants.TaxRate.SocialSecurity / 100);
                return Math.Min(expected, remaining);
            }

            public static double Medicare(double taxableEarnings, double preTax401k, double imputedLifeInsurance, AnnualAggregate aggregate)
            {
                double medicareEarnings = taxableEarnings + preTax401k + imputedLifeInsurance;
                if (aggregate.TaxableEarnings > Constants.Threshold.MedicareRateChange)
                {
                    return taxableEarnings * (Constants.TaxRate.MedicareAboveThreshold / 100);
                }
                else if (aggregate.TaxableEarnings + medicareEarnings < Constants.Threshold.MedicareRateChange)
                {
                    return medicareEarnings * (Constants.TaxRate.MedicareBelowThreshold / 100);
                }
                else
                {
                    double below = Constants.Threshold.MedicareRateChange - aggregate.TaxableEarnings;
                    double above = medicareEarnings - below;
                    return (below * (Constants.TaxRate.MedicareBelowThreshold / 100)) + (above * (Constants.TaxRate.MedicareAboveThreshold / 100));
                }
            }
        }
    }
}
