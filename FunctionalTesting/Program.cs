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
            TestOne();
            
            // string path = "C:\\Users\\tihanewi\\Downloads\\Failure BRKB.html";
            // HttpClient hc = new HttpClient();
            // HttpResponseMessage hrm = hc.GetAsync("https://finance.yahoo.com/quote/brk.b/key-statistics?p=brk.b").Result;
            // string web = hrm.Content.ReadAsStringAsync().Result;
            // System.IO.File.WriteAllText(path, web);
            
        }

        static void TestOne()
        {
            do
            {
                Console.WriteLine("Symbol?");
                string s = Console.ReadLine();
                try
                {
                    EquityStatisticData esd = EquityStatisticData.CreateAsync(s).Result;
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
                    EquityStatisticData esd = EquityStatisticData.CreateAsync(s).Result;
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
