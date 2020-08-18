using Skynet.Interfaces;
using Skynet.Models;
using System.Threading;
using System.Threading.Tasks;
using Skynet.Helpers;

namespace Skynet
{
    public partial class SkynetClient : IStats
    {
        public Task<StatisticResponse> GetStatistic(CancellationToken cancellationToken = default)
            => client.GetFromJsonAsync<StatisticResponse>("/skynet/stats", defaultJsonSerializerOptions, Constants.DefaultTimeout, cancellationToken);
    }
}
