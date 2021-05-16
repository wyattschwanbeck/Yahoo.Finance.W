using System;

namespace Yahoo.Finance
{
    public enum HistoricalDataDownloadResult
    {
        Successful = 0,
        DataDoesNotExistForSpecifiedTimePeriod = 1, //Stock was not public at the time that was requested
        Unauthorized = 2,
        OtherFailure = 3,
        Downloading = 4,
        NoDataFound = 5 //Symbol does not exist
    }
}