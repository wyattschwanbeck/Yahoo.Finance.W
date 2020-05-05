using System;
using System.IO;

namespace Yahoo.Finance
{
    //This will eventually be published in a separate package/class library.  I am only including it here locally to be used temporarily while I get that one up to NuGet.
    public class UnixToolkit
    {
        public static int GetUnixTime(DateTime timestamp)
        {
            DateTime epochtime = DateTime.Parse("1/1/1970");
            TimeSpan ts = timestamp - epochtime;
            return System.Convert.ToInt32(ts.TotalSeconds);
        }
    }
}