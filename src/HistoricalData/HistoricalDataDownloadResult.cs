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
        NoDataFound = 5 //Symbol does not exist or there was an error during download on the server side. For example, trying to get HWM: {"chart":{"result":null,"error":{"code":"Not Found","description":"Encountered an error when generating the download data"}}} (Returned 404: Not Found) 
    }
}