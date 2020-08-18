using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Skynet.Helpers
{
    internal static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> GetAsync(this HttpClient client, string requestUri, HttpCompletionOption completionOption, TimeSpan timeout, CancellationToken cancellationToken)
            => ExecuteWithTimeout(token => client.GetAsync(requestUri, completionOption, token), timeout, cancellationToken);

        public static Task<HttpResponseMessage> PostAsync(this HttpClient client, string requestUri, HttpContent content, TimeSpan timeout, CancellationToken cancellationToken)
            => ExecuteWithTimeout(token => client.PostAsync(requestUri, content, token), timeout, cancellationToken);

        public static Task<HttpResponseMessage> SendAsync(this HttpClient client, HttpRequestMessage request, TimeSpan timeout, CancellationToken cancellationToken)
            => ExecuteWithTimeout(token => client.SendAsync(request, token), timeout, cancellationToken);

        public static Task<TValue> GetFromJsonAsync<TValue>(this HttpClient client, string requestUri, TimeSpan timeout, CancellationToken cancellationToken = default)
            => ExecuteWithTimeout(token => client.GetFromJsonAsync<TValue>(requestUri, token), timeout, cancellationToken);

        public static Task<TValue> GetFromJsonAsync<TValue>(this HttpClient client, string requestUri, JsonSerializerOptions options, TimeSpan timeout, CancellationToken cancellationToken = default)
            => ExecuteWithTimeout(token => client.GetFromJsonAsync<TValue>(requestUri, options, token), timeout, cancellationToken);

        public static Task<HttpResponseMessage> PostAsJsonAsync<TValue>(this HttpClient client, string requestUri, TValue value, TimeSpan timeout, CancellationToken cancellationToken)
            => ExecuteWithTimeout(token => client.PostAsJsonAsync<TValue>(requestUri, value, token), timeout, cancellationToken);

        public async static Task<TResult> ExecuteWithTimeout<TResult>(Func<CancellationToken, Task<TResult>> func, TimeSpan timeout, CancellationToken cancellationToken)
        {
            if (timeout == TimeSpan.Zero)
            {
                timeout = TimeSpan.FromSeconds(100);
            }

            using var ctsForTimeout = new CancellationTokenSource();
            ctsForTimeout.CancelAfter(timeout);
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, ctsForTimeout.Token);

            try
            {
                return await func(linkedCts.Token).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                if (ctsForTimeout.IsCancellationRequested)
                {
                    throw new TimeoutException();
                }

                throw;
            }
        }
    }
}
