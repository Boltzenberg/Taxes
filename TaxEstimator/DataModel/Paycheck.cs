namespace TaxEstimator.DataModel
{
    public class Paycheck : PayPeriodStats
    {
        public int PaycheckMonth { get; }

        public int PaycheckDay { get; }

        public Paycheck(int month, int day, IndividualSettings settings, AnnualAggregate aggregate)
        {
            // Set the date for this paycheck
            this.PaycheckMonth = month;
            this.PaycheckDay = day;

            // Gross pay inputs
            this.Wages = Calculate.Paycheck.Salary(this.PaycheckMonth, settings);
            this.Bonus = Calculate.Paycheck.Bonus(this.PaycheckMonth, this.PaycheckDay, settings);
            double grossPay = this.Wages + this.Bonus;

            // Pre-Tax withholdings and additional taxable stuff -> HSA before PreTax!
            this.PreTax401k = Calculate.Paycheck.PreTax401k(grossPay, settings, aggregate);
            this.HSA = Calculate.Paycheck.HSA(grossPay, this.PreTax401k, settings, aggregate);
            this.StockAwards = Calculate.Paycheck.StockAwards(this.PaycheckMonth, this.PaycheckDay, settings);
            this.TaxableEarnings = grossPay - this.PreTax401k + settings.StayFitTaxable + this.StockAwards + settings.DisabilityInsuranceTaxable - this.HSA;

            // Taxes
            this.Federal = Calculate.Taxes.Federal(this.TaxableEarnings, this.Bonus, settings);
            this.SocialSecurity = Calculate.Taxes.SocialSecurity(this.TaxableEarnings, this.PreTax401k, aggregate);
            this.Medicare = Calculate.Taxes.Medicare(this.TaxableEarnings, this.PreTax401k, settings.ImputedLifeInsurance, aggregate);

            // After tax withholdings
            double earnings = this.TaxableEarnings - this.Federal - this.SocialSecurity - this.Medicare;
            this.AfterTax401k = Calculate.Paycheck.AfterTax401k(earnings, settings, aggregate);
            earnings -= this.AfterTax401k;
            this.ESPP = Calculate.Paycheck.ESPP(earnings, settings, aggregate);
        }
    }
}
