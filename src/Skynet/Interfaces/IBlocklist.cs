using System.Threading;
using System.Threading.Tasks;

namespace Skynet.Interfaces
{
    public interface IBlocklist
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string[]> GetBlocklists(CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddToBlocklist(string hash, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hashes"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddToBlocklist(string[] hashes, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RemoveFromBlocklist(string hash, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hashes"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RemoveFromBlocklist(string[] hashes, CancellationToken cancellationToken = default);
    }
}
