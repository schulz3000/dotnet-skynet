using Skynet.Helpers;
using Skynet.Interfaces;
using Skynet.Models;
using Skynet.Options;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Skynet
{
    public partial class SkynetClient : IFiles
    {
        public Task<SkynetFileMetadata> Download(string skylink, string destination, CancellationToken cancellationToken = default)
            => Download(skylink, destination, DownloadOptions.Default, cancellationToken);

        public async Task<SkynetFileMetadata> Download(string skylink, string destination, DownloadOptions options, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(skylink))
            {
                throw new ArgumentNullException(nameof(skylink));
            }

            skylink = skylink.Replace(Constants.SkynetUriPrefix, string.Empty);

            var query = BuildDefaultQuerystring(options);

            if (!string.IsNullOrWhiteSpace(query))
            {
                query = "?" + query;
            }

            var response = await client.GetAsync($"{options.EndpointPath?.Trim('/')}/{Uri.EscapeUriString(skylink)}{query}", HttpCompletionOption.ResponseHeadersRead, TimeSpan.FromSeconds(options.TimeoutInSeconds), cancellationToken).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var metadata = ParseMetadataHeader(response.Headers);

            using var fs = File.OpenWrite(Path.Combine(destination, metadata.Filename));
            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            await stream.CopyToAsync(fs).ConfigureAwait(false);

            return metadata;
        }

        public Task<SkynetFileMetadata> GetFileMetadata(string skylink, CancellationToken cancellationToken = default)
            => GetFileMetadata(skylink, DownloadOptions.Default, cancellationToken);

        public async Task<SkynetFileMetadata> GetFileMetadata(string skylink, DownloadOptions options, CancellationToken cancellationToken = default)
        {
            var response = await client.SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Head,
                RequestUri = new Uri(client.BaseAddress, $"{options.EndpointPath?.Trim('/')}/{Uri.EscapeUriString(skylink)}")
            }, TimeSpan.FromSeconds(options.TimeoutInSeconds), cancellationToken).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            return ParseMetadataHeader(response.Headers);
        }

        private SkynetFileMetadata ParseMetadataHeader(HttpResponseHeaders headers)
        {
            if (!headers.TryGetValues("Skynet-File-Metadata", out var values) || !values.Any())
            {
                throw new SkynetException("Cannot read metadata");
            }

            return JsonSerializer.Deserialize<SkynetFileMetadata>(values.FirstOrDefault(), defaultJsonSerializerOptions);
        }

        public Task<UploadResponse> Upload(string filenameOrDirectory, CancellationToken cancellationToken = default)
            => Upload(filenameOrDirectory, UploadOptions.Default, cancellationToken);

        public Task<UploadResponse> Upload(string filenameOrDirectory, UploadOptions options, CancellationToken cancellationToken = default)
        {
            var isDirectory = Directory.Exists(filenameOrDirectory);
            var isFile = File.Exists(filenameOrDirectory);

            if (isDirectory)
            {
                var info = new DirectoryInfo(filenameOrDirectory);
                return DirectoryUpload(info, options, cancellationToken);
            }

            if (isFile)
            {
                var file = File.ReadAllBytes(filenameOrDirectory);
                if (string.IsNullOrEmpty(options.CustomFilename))
                {
                    var fileInfo = new FileInfo(filenameOrDirectory);
                    options.CustomFilename = fileInfo.Name;
                }
                return FileUpload(file, options, cancellationToken);
            }

            throw new FileNotFoundException("File or Directory not found", filenameOrDirectory);
        }

        public Task<UploadResponse> Upload(byte[] file, CancellationToken cancellationToken = default)
            => FileUpload(file, UploadOptions.Default, cancellationToken);

        public Task<UploadResponse> Upload(byte[] file, UploadOptions options, CancellationToken cancellationToken = default)
            => FileUpload(file, options, cancellationToken);

        private Task<UploadResponse> FileUpload(byte[] file, UploadOptions options, CancellationToken cancellationToken = default)
        {
            var multipart = new MultipartFormDataContent();

            multipart.Add(new ByteArrayContent(file), options.PortalFileFieldName, options.CustomFilename);

            return InternalUpload(multipart, options, cancellationToken);
        }

        private Task<UploadResponse> DirectoryUpload(DirectoryInfo directoryInfo, UploadOptions options, CancellationToken cancellationToken = default)
        {
            var multipart = new MultipartFormDataContent();

            foreach (var file in directoryInfo.EnumerateFiles("*", SearchOption.AllDirectories))
            {
                multipart.Add(new StreamContent(file.OpenRead()), file.Name, file.FullName.Replace(directoryInfo.FullName, string.Empty));
            }

            return InternalUpload(multipart, options, cancellationToken);
        }

        private async Task<UploadResponse> InternalUpload(MultipartFormDataContent content, UploadOptions options, CancellationToken cancellationToken = default)
        {
            //TODO: add options      

            var query = BuildDefaultQuerystring(options);

            if (!string.IsNullOrWhiteSpace(query))
            {
                query = "?" + query;
            }

            var response = await client.PostAsync(options.EndpointPath + query, content, TimeSpan.FromSeconds(options.TimeoutInSeconds), cancellationToken).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<UploadResponse>().ConfigureAwait(false);
        }

        private string BuildDefaultQuerystring(DefaultOptions options)
        {
            string query = string.Empty;

            if (!string.IsNullOrEmpty(options.SkykeyID))
            {
                query += $"id={options.SkykeyID}";
            }

            if (string.IsNullOrEmpty(options.SkykeyID) && !string.IsNullOrEmpty(options.SkykeyName))
            {
                query += $"name={options.SkykeyName}";
            }

            return query;
        }
    }
}
