using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;

namespace Yahoo.Finance
{
    public class EarningsCalendarProvider
    {
        public async Task<string[]> GetCompaniesReportingEarningsAsync(DateTime date)
        {
            string url = "https://finance.yahoo.com/calendar/earnings?day=" + date.Year.ToString("0000") + "-" + date.Month.ToString("00") + "-" + date.Day.ToString("00"); 
            HttpClient hc = new HttpClient();
            HttpResponseMessage hrm = await hc.GetAsync(url);
            string web = await hrm.Content.ReadAsStringAsync();

            int loc1 = 0;
            int loc2 = 0;
            List<string> Splitter = new List<string>();

            loc1 = web.IndexOf("table class");
            if (loc1 == -1)
            {
                List<string> none = new List<string>();
                return none.ToArray();
            }
            loc2 = web.IndexOf("</table");
            string table_data = web.Substring(loc1, loc2 - loc1);

            Splitter.Add("<tr");

            string[] rows = table_data.Split(Splitter.ToArray(), StringSplitOptions.None);

            int t = 0;
            List<string> ToReturn = new List<string>();
            for (t=2;t<rows.Length;t++)
            {
               
                Splitter.Clear();
                Splitter.Add("<td");
                string[] cols = rows[t].Split(Splitter.ToArray(), StringSplitOptions.None);


                //Get symbol
                loc1 = cols[1].IndexOf("a href");
                loc1 = cols[1].IndexOf(">", loc1 + 1);
                loc2 = cols[1].IndexOf("<", loc1 + 1);
                ToReturn.Add(cols[1].Substring(loc1 + 1, loc2 - loc1 - 1));
                

            }


            return ToReturn.ToArray();
        }
    }

}