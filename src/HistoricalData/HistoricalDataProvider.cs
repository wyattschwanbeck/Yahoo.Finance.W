using System;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using TimHanewichToolkit;


namespace Yahoo.Finance
{
    public class HistoricalDataProvider
            {
                public HistoricalDataRecord[] HistoricalData { get; set; }

                public async Task DownloadHistoricalDataAsync(string StockSymbol, DateTime PeriodStart, DateTime PeriodEnd)
                {
                    DateTime RequestStart = DateTime.Now;
                    string rawdata = "";

                    //Get the crumb!
                    HttpClient hc = new HttpClient();
                    HttpResponseMessage rm = await hc.GetAsync("https://finance.yahoo.com/quote/" + StockSymbol);
                    string web = await rm.Content.ReadAsStringAsync();
                    int loc1 = web.IndexOf("}}}}},\"CrumbStore\":{\"crumb\":");
                    if (loc1 == -1)
                    {
                        throw new Exception("Unable to verify stock '" + StockSymbol + "'.");
                    }
                    loc1 = web.IndexOf("crumb", loc1 + 1);
                    loc1 = web.IndexOf(":", loc1 + 1);
                    loc1 = web.IndexOf("\"", loc1 + 1);
                    int loc2 = web.IndexOf("\"", loc1 + 1);
                    string crumb = web.Substring(loc1 + 1, loc2 - loc1 - 1);

                    //Get the unix times
                    string Unix1 = UnixToolkit.GetUnixTime(PeriodStart).ToString();
                    string Unix2 = UnixToolkit.GetUnixTime(PeriodEnd).ToString();



                    //Get the info
                    string urlfordata = "https://query1.finance.yahoo.com/v7/finance/download/" + StockSymbol + "?period1=" + Unix1 + "&period2=" + Unix2 + "&interval=1d&events=history&crumb=" + crumb;
                    HttpResponseMessage fr = await hc.GetAsync(urlfordata);
                    string resptext = await fr.Content.ReadAsStringAsync();
                    if (resptext.Contains("Invalid cookie") == false)
                    {
                        rawdata = resptext;
                    }

                    //Parse into data records
                    List<HistoricalDataRecord> datarecs = new List<HistoricalDataRecord>();
                    List<string> Splitter = new List<string>();
                    Splitter.Add("\n");
                    string[] rows = rawdata.Split(Splitter.ToArray(), StringSplitOptions.None);
                    int t = 0;
                    for (t = 1; t <= rows.Length - 1; t++)
                    {
                        string thisrow = rows[t];
                        if (thisrow != "")
                        {
                            try
                            {
                                HistoricalDataRecord rec = new HistoricalDataRecord();

                                Splitter.Clear();
                                Splitter.Add(",");
                                string[] cols = thisrow.Split(Splitter.ToArray(), StringSplitOptions.None);

                                rec.Date = DateTime.Parse(cols[0]);
                                rec.Open = System.Convert.ToSingle(cols[1]);
                                rec.High = System.Convert.ToSingle(cols[2]);
                                rec.Low = System.Convert.ToSingle(cols[3]);
                                rec.Close = System.Convert.ToSingle(cols[4]);
                                rec.AdjustedClose = System.Convert.ToSingle(cols[5]);
                                rec.Volume = System.Convert.ToInt32(cols[6]);

                                datarecs.Add(rec);
                            }
                            catch
                            {
                                throw new Exception("Unable to conver this row: " + thisrow);
                            }
                        }
                    }

                    //Post it to this class
                    HistoricalData = datarecs.ToArray();


                }

            }
}