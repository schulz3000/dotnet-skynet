using System.Diagnostics.CodeAnalysis;

namespace Skynet.Models
{
    public class BlocklistResponse
    {
        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        public string[] Blocklist { get; set; }
    }
}
