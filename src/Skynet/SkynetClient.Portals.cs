using Skynet.Interfaces;
using Skynet.Models;
using System;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Skynet.Helpers;

namespace Skynet
{
    public partial class SkynetClient : IPortals
    {
        private const string PortalsPath = "/skynet/portals";

        public Task AddPortal(Portal portal, CancellationToken cancellationToken = default)
            => PostToPortalEndPoint(new { add = new[] { portal } }, cancellationToken);

        public Task AddPortals(Portal[] portals, CancellationToken cancellationToken = default)
            => PostToPortalEndPoint(new { add = portals }, cancellationToken);

        public async Task<Portal[]> GetPortals(CancellationToken cancellationToken = default)
        {
            var result = await client.GetFromJsonAsync<PortalResponse>(PortalsPath, defaultJsonSerializerOptions, Constants.DefaultTimeout, cancellationToken).ConfigureAwait(false);
            return result.Portals ?? Array.Empty<Portal>();
        }

        public Task RemovePortal(string portal, CancellationToken cancellationToken = default)
            => PostToPortalEndPoint(new { remove = new[] { portal } }, cancellationToken);

        public Task RemovePortals(string[] portals, CancellationToken cancellationToken = default)
            => PostToPortalEndPoint(new { remove = portals }, cancellationToken);

        private async Task PostToPortalEndPoint(object payload, CancellationToken cancellationToken = default)
        {
            var response = await client.PostAsJsonAsync(PortalsPath, payload, Constants.DefaultTimeout, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }
    }
}
