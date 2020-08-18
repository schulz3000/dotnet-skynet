using Skynet.Interfaces;
using Skynet.Options;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;

namespace Skynet
{
    public partial class SkynetClient : ISkynetClient, IDisposable
    {
        private static readonly JsonSerializerOptions defaultJsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private readonly HttpClient client;

        public SkynetClient(ConnectionOptions options = default)
        {
            if(options==default)
            {
                options = ConnectionOptions.Default;
            }

            client = new HttpClient();
            client.BaseAddress = options.PortalUrl;
            client.Timeout = Timeout.InfiniteTimeSpan;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrWhiteSpace(options.CustomUserAgent))
            {
                client.DefaultRequestHeaders.Add("User-Agent", options.CustomUserAgent);
            }

            if (!string.IsNullOrWhiteSpace(options.ApiKey))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", options.ApiKey);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                client?.Dispose();
            }
        }
    }
}
