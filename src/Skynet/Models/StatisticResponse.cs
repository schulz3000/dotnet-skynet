namespace Skynet.Models
{
    public class StatisticResponse
    {
        public int Uptime { get; set; }
        public UploadStats UploadStats { get; set; }
        public VersionInfo VersionInfo { get; set; }
        public PerformanceStats PerformanceStats { get; set; }
    }

    public class UploadStats
    {
        public long NumFiles { get; set; }
        public long TotalSize { get; set; }
    }

    public class VersionInfo
    {
        public string Version { get; set; }
        public string GitRevision { get; set; }
    }

    public class PerformanceStats
    {
        public PerformanceStatsItem TimeToFirstByte { get; set; }
        public PerformanceStatsItem Download64Kb { get; set; }
        public PerformanceStatsItem Download1Mb { get; set; }
        public PerformanceStatsItem Download4Mb { get; set; }
        public PerformanceStatsItem DownloadLarge { get; set; }
        public PerformanceStatsItem Upload4Mb { get; set; }
        public PerformanceStatsItem UploadLarge { get; set; }
    }

    public class PerformanceStatsItem
    {
        public string LastUpdate { get; set; }
        public PerformanceStatsTimings OneMinute { get; set; }
        public PerformanceStatsTimings FiveMinutes { get; set; }
        public PerformanceStatsTimings FifteenMinutes { get; set; }
        public PerformanceStatsTimings TwentyFourHours { get; set; }
        public PerformanceStatsTimings Lifetime { get; set; }
    }

    public class PerformanceStatsTimings
    {
        public float N60ms { get; set; }
        public float N120ms { get; set; }
        public float N240ms { get; set; }
        public float N500ms { get; set; }
        public float N1000ms { get; set; }
        public float N2000ms { get; set; }
        public float N5000ms { get; set; }
        public int N10s { get; set; }
        public int Nlong { get; set; }
        public float Nerr { get; set; }
    }
}
