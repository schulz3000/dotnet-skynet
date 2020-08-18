using System;

namespace Skynet.Options
{
    public class ConnectionOptions
    {
        internal static ConnectionOptions Default { get; } = new ConnectionOptions();

        public Uri PortalUrl { get; set; } = new Uri(Constants.DefaultPortalUrl);
        public string ApiKey { get; set; }
        public string CustomUserAgent { get; set; }
    }
}
