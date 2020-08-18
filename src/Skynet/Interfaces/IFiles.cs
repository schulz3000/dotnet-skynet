using Skynet.Models;
using Skynet.Options;
using System.Threading;
using System.Threading.Tasks;

namespace Skynet.Interfaces
{
    public interface IFiles
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<UploadResponse> Upload(string filenameOrDirectory, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<UploadResponse> Upload(string filenameOrDirectory, UploadOptions options, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<UploadResponse> Upload(byte[] file, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<UploadResponse> Upload(byte[] file, UploadOptions options, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="skylink"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<SkynetFileMetadata> GetFileMetadata(string skylink, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="skylink"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<SkynetFileMetadata> GetFileMetadata(string skylink, DownloadOptions options, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="skylink"></param>
        /// <param name="destination"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<SkynetFileMetadata> Download(string skylink, string destination, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="skylink"></param>
        /// <param name="destination"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<SkynetFileMetadata> Download(string skylink, string destination, DownloadOptions options, CancellationToken cancellationToken = default);
    }
}
