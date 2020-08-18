namespace Skynet.Options
{
    public class DownloadOptions:DefaultOptions
    {
        internal static DownloadOptions Default { get; } = new DownloadOptions();

        public DownloadOptions()
        {
            EndpointPath = "/";
        }
    }
}
