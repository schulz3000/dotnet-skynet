using Skynet.Interfaces;
using Skynet.Models;
using System;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Skynet.Helpers;

namespace Skynet
{
    public partial class SkynetClient : IBlocklist
    {
        private const string BlocklistPath = "/skynet/blocklist";

        public Task AddToBlocklist(string hash, CancellationToken cancellationToken = default)
            => PostToBlocklistEndPoint(new { add = new[] { hash } }, cancellationToken);

        public Task AddToBlocklist(string[] hashes, CancellationToken cancellationToken = default)
            => PostToBlocklistEndPoint(new { add = hashes }, cancellationToken);

        public async Task<string[]> GetBlocklists(CancellationToken cancellationToken = default)
        {
            var result = await client.GetFromJsonAsync<BlocklistResponse>(BlocklistPath, defaultJsonSerializerOptions, Constants.DefaultTimeout, cancellationToken).ConfigureAwait(false);
            return result.Blocklist ?? Array.Empty<string>();
        }

        public Task RemoveFromBlocklist(string hash, CancellationToken cancellationToken = default)
            => PostToBlocklistEndPoint(new { remove = new[] { hash } }, cancellationToken);

        public Task RemoveFromBlocklist(string[] hashes, CancellationToken cancellationToken = default)
            => PostToBlocklistEndPoint(new { add = hashes }, cancellationToken);

        private async Task PostToBlocklistEndPoint(object payload, CancellationToken cancellationToken = default)
        {
            var response = await client.PostAsJsonAsync(BlocklistPath, payload, Constants.DefaultTimeout, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }
    }
}
