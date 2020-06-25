using System;
using Yahoo.Finance;
using Newtonsoft.Json;
using TimHanewich.Investing;
using System.Collections.Generic;
using System.Net.Http;


namespace FunctionalTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            Equity e = Equity.Create("MSFT");
            e.DownloadStatisticsAsync().Wait();
            Console.WriteLine(JsonConvert.SerializeObject(e));
            
        }

        static void TestOne()
        {
            do
            {
                Console.WriteLine("Symbol?");
                string s = Console.ReadLine();
                try
                {
                    EquityStatisticalData esd = EquityStatisticalData.CreateAsync(s).Result;
                    string json = JsonConvert.SerializeObject(esd);
                    Console.WriteLine(json);
                }
                catch
                {
                    Console.WriteLine("Failure!");
                }
                
            } while (true);
            
        }

        static void TestSp500()
        {
            string[] sp500 = InvestingToolkit.GetEquityGroupAsync(EquityGroup.SP500).Result;
            List<string> Errors = new List<string>();
            int t = 0;
            foreach (string s in sp500)
            {
                Console.Write("Working on " + s + "... ");
                t = t + 1;
                try
                {
                    EquityStatisticalData esd = EquityStatisticalData.CreateAsync(s).Result;
                    float pdone = (float)t / (float)sp500.Length;
                    Console.WriteLine("success... " + pdone.ToString("#0.0%"));
                }
                catch (Exception e)
                {
                    Console.WriteLine("FAILURE! " + e.Message);
                    Errors.Add(s);
                }

                
            }


            Console.WriteLine("Done!");
            Console.WriteLine("Critical errors:");
            foreach (string s in Errors)
            {
                Console.WriteLine(s);
            }

        }
    }
}
