using Skynet.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Skynet.Interfaces
{
    public interface IStats
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<StatisticResponse> GetStatistic(CancellationToken cancellationToken = default);
    }
}
