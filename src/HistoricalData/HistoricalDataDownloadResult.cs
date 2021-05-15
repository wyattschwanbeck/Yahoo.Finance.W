using System;

namespace Yahoo.Finance
{
    public enum HistoricalDataDownloadResult
    {
        Successful = 0,
        DataDoesNotExist = 1,
        Unauthorized = 2,
        OtherFailure = 3,
        Downloading = 4
    }
}