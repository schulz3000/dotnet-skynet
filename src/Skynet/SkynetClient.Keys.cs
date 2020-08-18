using Skynet.Interfaces;
using Skynet.Models;
using System;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Skynet.Helpers;

namespace Skynet
{
    public partial class SkynetClient : IKeys
    {
        public async Task AddKey(string key, CancellationToken cancellationToken = default)
        {
            var response = await client.PostAsync($"/skynet/addskykey?skykey={Uri.EscapeUriString(key)}", null, Constants.DefaultTimeout, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }

        public async Task<string> CreateKey(string name, SkyKeyType type, CancellationToken cancellationToken = default)
        {
            var response = await client.PostAsync($"/skynet/createskykey?name={Uri.EscapeUriString(name)}&type={type}", null, Constants.DefaultTimeout, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<CreateSkyKeyResponse>(defaultJsonSerializerOptions).ConfigureAwait(false);
            return result.SkyKey;
        }

        public async Task<string> GetIdByName(string name, CancellationToken cancellationToken = default)
        {
            var result = await client.GetFromJsonAsync<SkyKeyIdResponse>($"/skynet/skykeyid?name={Uri.EscapeUriString(name)}", defaultJsonSerializerOptions, Constants.DefaultTimeout, cancellationToken).ConfigureAwait(false);
            return result.SkyKeyId;
        }

        public Task<SkyKey> GetKeyById(string id, CancellationToken cancellationToken = default)
            => client.GetFromJsonAsync<SkyKey>($"/skynet/skykey?id={Uri.EscapeUriString(id)}", defaultJsonSerializerOptions, Constants.DefaultTimeout, cancellationToken);

        public Task<SkyKey> GetKeyByName(string name, CancellationToken cancellationToken = default)
           => client.GetFromJsonAsync<SkyKey>($"/skynet/skykey?name={Uri.EscapeUriString(name)}", defaultJsonSerializerOptions, Constants.DefaultTimeout, cancellationToken);

        public async Task<SkyKey[]> GetKeys(CancellationToken cancellationToken = default)
        {
            var result = await client.GetFromJsonAsync<SkyKeysResponse>("/skynet/skykeys", defaultJsonSerializerOptions, Constants.DefaultTimeout, cancellationToken).ConfigureAwait(false);
            return result.SkyKeys ?? Array.Empty<SkyKey>();
        }
    }
}
