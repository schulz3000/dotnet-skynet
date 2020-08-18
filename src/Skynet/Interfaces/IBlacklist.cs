using System.Threading;
using System.Threading.Tasks;

namespace Skynet.Interfaces
{
    public interface IBlacklist
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string[]> GetBlacklists(CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddToBlacklist(string hash, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hashes"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddToBlacklist(string[] hashes, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RemoveFromBlacklist(string hash, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hashes"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RemoveFromBlacklist(string[] hashes, CancellationToken cancellationToken = default);
    }
}
