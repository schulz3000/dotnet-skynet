using Skynet.Interfaces;
using Skynet.Models;
using System;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Skynet.Helpers;

namespace Skynet
{
    public partial class SkynetClient : IBlacklist
    {
        private const string BlacklistPath = "/skynet/blacklist";

        public Task AddToBlacklist(string hash, CancellationToken cancellationToken = default)
            => PostToBlacklistEndPoint(new { add = new[] { hash } }, cancellationToken);

        public Task AddToBlacklist(string[] hashes, CancellationToken cancellationToken = default)
            => PostToBlacklistEndPoint(new { add = hashes }, cancellationToken);

        public async Task<string[]> GetBlacklists(CancellationToken cancellationToken = default)
        {
            var result = await client.GetFromJsonAsync<BlacklistResponse>(BlacklistPath, defaultJsonSerializerOptions, Constants.DefaultTimeout, cancellationToken).ConfigureAwait(false);
            return result.Blacklist ?? Array.Empty<string>();
        }

        public Task RemoveFromBlacklist(string hash, CancellationToken cancellationToken = default)
            => PostToBlacklistEndPoint(new { remove = new[] { hash } }, cancellationToken);

        public Task RemoveFromBlacklist(string[] hashes, CancellationToken cancellationToken = default)
            => PostToBlacklistEndPoint(new { add = hashes }, cancellationToken);

        private async Task PostToBlacklistEndPoint(object payload, CancellationToken cancellationToken = default)
        {
            var response = await client.PostAsJsonAsync(BlacklistPath, payload, Constants.DefaultTimeout, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }
    }
}
