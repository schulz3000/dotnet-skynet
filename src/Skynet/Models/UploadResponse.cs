namespace Skynet.Models
{
    public class UploadResponse
    {
        /// <summary>
        /// This is the skylink that can be used with the /skynet/skylink GET endpoint to retrieve the file that has been uploaded.
        /// </summary>
        public string Skylink { get; set; }

        /// <summary>
        /// This is the hash that is encoded into the skylink.
        /// </summary>
        public string Merkleroot { get; set; }

        /// <summary>
        /// This is the bitfield that gets encoded into the skylink. The bitfield contains a version, an offset and a length in a heavily compressed and optimized format.
        /// </summary>
        public int Bitfield { get; set; }
    }
}
