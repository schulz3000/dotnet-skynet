using System;
using System.Threading.Tasks;

namespace Skynet.TestApp
{
    internal static class Program
    {
        private static async Task Main()
        {
            using var client = new SkynetClient();

            var blocklists = await client.GetBlocklists();

            var stats = await client.GetStatistic();

            var metadata = await client.GetFileMetadata("IADR9tvmKSzmY-i0Bfyd8mXgaGUZmuQDsbimvgjnFQXIhQ");

            var downloadMetadata = await client.Download("IADR9tvmKSzmY-i0Bfyd8mXgaGUZmuQDsbimvgjnFQXIhQ", @"c:\temp\sia");

            var result = await client.Upload(@"c:\temp\sia\sia-lm.png");

            Console.WriteLine(result.Skylink);

            Console.ReadKey();
        }
    }
}
