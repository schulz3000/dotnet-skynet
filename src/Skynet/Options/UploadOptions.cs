namespace Skynet.Options
{
    public class UploadOptions : DefaultOptions
    {
        internal static UploadOptions Default { get; } = new UploadOptions();

        public UploadOptions()
        {
            EndpointPath = "/skynet/skyfile";
        }

        /// <summary>
        /// The field name for files on the portal. Usually should not need to be changed.
        /// </summary>
        public string PortalFileFieldName { get; set; } = "file";

        /// <summary>
        /// The field name for directories on the portal. Usually should not need to be changed.
        /// </summary>
        public string PortalDirectoryFileFieldName { get; set; } = "files[]";

        /// <summary>
        /// Custom filename. This is the filename that will be returned when downloading the file in a browser.
        /// </summary>
        public string CustomFilename { get; set; }

        /// <summary>
        /// Custom dirname. If this is empty, the base name of the directory being uploaded will be used by default.
        /// </summary>
        public string CustomDirname { get; set; }
    }
}
