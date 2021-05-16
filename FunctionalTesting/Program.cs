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
            string[] stocks = InvestingToolkit.GetEquityGroupAsync(EquityGroup.SP500).Result;
            foreach (string s in stocks)
            {
                HistoricalDataProvider hdp = new HistoricalDataProvider();
                Console.Write("Working on " + s + "... ");
                hdp.DownloadHistoricalDataAsync(s, new DateTime(2010, 1, 1), new DateTime(2011, 1, 1)).Wait();
                if (hdp.DownloadResult == HistoricalDataDownloadResult.Successful)
                {
                    Console.WriteLine(hdp.HistoricalData.Length.ToString());
                }
                else if (hdp.DownloadResult == HistoricalDataDownloadResult.DataDoesNotExist)
                {
                    Console.WriteLine("Data did not exist at the time!");
                }
                else if (hdp.DownloadResult == HistoricalDataDownloadResult.Unauthorized)
                {
                    Console.WriteLine("Unauthorized!!!!!");
                }
                else
                {
                    Console.WriteLine("Idk... " + hdp.DownloadResult.ToString());
                }
            }
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
