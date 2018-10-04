namespace TaxEstimator.DataModel
{
    public class AnnualAggregate : PayPeriodStats
    {
        public void UpdateFromPaycheck(Paycheck paycheck)
        {
            this.Wages += paycheck.Wages;
            this.Bonus += paycheck.Bonus;
            this.PreTax401k += paycheck.PreTax401k;
            this.HSA += paycheck.HSA;
            this.TaxableEarnings += paycheck.TaxableEarnings;
            this.StockAwards += paycheck.StockAwards;
            this.Federal += paycheck.Federal;
            this.SocialSecurity += paycheck.SocialSecurity;
            this.Medicare += paycheck.Medicare;
            this.AfterTax401k += paycheck.AfterTax401k;
            this.ESPP += paycheck.ESPP;
        }
    }
}
