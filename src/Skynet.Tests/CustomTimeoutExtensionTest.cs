using Skynet.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Skynet.Tests
{
    public class CustomTimeoutExtensionTest
    {
        [Fact]
        public async Task DoNotTriggerTimeoutTest()
        {
            var result = await HttpClientExtensions.ExecuteWithTimeout(async token =>
            {
                await Task.Delay(500, token);
                return true;
            }, TimeSpan.FromSeconds(5), CancellationToken.None);

            Assert.True(result);
        }

        [Fact]
        public Task TriggerTimeoutTest()
        {
            return Assert.ThrowsAsync<TimeoutException>(() =>
            HttpClientExtensions.ExecuteWithTimeout(async token =>
            {
                await Task.Delay(3000, token);
                return true;
            }, TimeSpan.FromSeconds(2), CancellationToken.None));
        }        

        [Fact]
        public Task CancelBeforeTimeoutTest()
        {
            var cts = new CancellationTokenSource(1000);

            return Assert.ThrowsAsync<TaskCanceledException>(() =>
            HttpClientExtensions.ExecuteWithTimeout(async token =>
            {
                await Task.Delay(5000, token);
                return true;
            }, TimeSpan.FromSeconds(5), cts.Token));
        }

        [Fact]
        public Task CancelBeforeTimeoutNonTaskTest()
        {
            var cts = new CancellationTokenSource(1000);

            return Assert.ThrowsAsync<SkynetException>(() =>
            HttpClientExtensions.ExecuteWithTimeout<bool>(async token =>
            {
                while (!token.IsCancellationRequested)
                {
                    await Task.Delay(250);
                }

                throw new SkynetException("Operation Cancelled");

            }, TimeSpan.FromSeconds(5), cts.Token));
        }
    }
}
