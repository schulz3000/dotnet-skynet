using System.Diagnostics.CodeAnalysis;

namespace Skynet.Models
{
    public class BlacklistResponse
    {
        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        public string[] Blacklist { get; set; }
    }
}
