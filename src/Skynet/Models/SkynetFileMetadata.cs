using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Skynet.Models
{
    public class SkynetFileMetadata : SkynetFile
    {
        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        public Dictionary<string,Subfile> Subfiles { get; set; }
    }

    public abstract class SkynetFile
    {
        public int Mode { get; set; }
        public string Filename { get; set; }
    }

    public class Subfile : SkynetFile
    {
        public string ContentType { get; set; }
        public int Offset { get; set; }
        public int Len { get; set; }
    }
}
