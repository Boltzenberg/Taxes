using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxEstimator.DataModel;

namespace Scratch
{
    class Program
    {
        static void Main(string[] args)
        {
            AnnualAggregate agg = new AnnualAggregate();
            IndividualSettings settings = IndividualSettings.Deserialize("sample.json");

            /*
            IndividualSettings output = new IndividualSettings();
            output.PerPaycheckSettings = new List<PerPaycheckSettings>();
            output.PerPaycheckSettings.Add(new PerPaycheckSettings() { Month = 3, Day = 15, StockAwards = 300 });
            output.PerPaycheckSettings.Add(new PerPaycheckSettings() { Month = 9, Day = 15, Bonus = 80000 });
            Console.WriteLine(output.Serialize());
            */

            List<int> days = new List<int>() { 15, 30 };
            for (int month = 1; month <= 12; month++)
            {
                foreach (int day in days)
                {
                    Paycheck p = new Paycheck(month, day, settings, agg);
                    agg.UpdateFromPaycheck(p);

                    WriteToConsole(string.Format("Paycheck ({0}/{1})", month, day), p);
                    Console.WriteLine();
                    WriteToConsole("Current Aggregate", agg);
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }
        }

        private static void WriteToConsole(string title, PayPeriodStats stats)
        {
            Console.WriteLine(title);
            Console.WriteLine("Wages:            {0:C}", stats.Wages);
            Console.WriteLine("Bonus:            {0:C}", stats.Bonus);
            Console.WriteLine("------------------");
            Console.WriteLine("Pre-Tax 401(k):   {0:C}", stats.PreTax401k);
            Console.WriteLine("HSA:              {0:C}", stats.HSA);
            Console.WriteLine("Taxable Earnings: {0:C}", stats.TaxableEarnings);
            Console.WriteLine("------------------");
            Console.WriteLine("Stock Awards:     {0:C}", stats.StockAwards);
            Console.WriteLine("Federal Taxes:    {0:C}", stats.Federal);
            Console.WriteLine("Social Security:  {0:C}", stats.SocialSecurity);
            Console.WriteLine("Medicare:         {0:C}", stats.Medicare);
            Console.WriteLine("------------------");
            Console.WriteLine("After-Tax 401(k): {0:C}", stats.AfterTax401k);
            Console.WriteLine("ESPP:             {0:C}", stats.ESPP);
        }
    }
}
