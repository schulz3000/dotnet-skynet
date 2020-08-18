using Skynet.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Skynet.Interfaces
{
    public interface IPortals
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Portal[]> GetPortals(CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="portal"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddPortal(Portal portal, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="portals"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddPortals(Portal[] portals, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="portal"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RemovePortal(string portal, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="portals"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RemovePortals(string[] portals, CancellationToken cancellationToken = default);
    }
}
