namespace Skynet.Options
{
    public abstract class DefaultOptions
    {
        /// <summary>
        /// The relative URL path of the portal endpoint to contact.
        /// </summary>
        public string EndpointPath { get; set; }

        /// <summary>
        /// The name of the skykey on the portal used to decrypt the download.
        /// </summary>
        public string SkykeyName { get; set; }

        /// <summary>
        ///  The ID of the skykey on the portal used to decrypt the download.
        /// </summary>
        public string SkykeyID { get; set; }

        /// <summary>
        /// The timeout in seconds.
        /// </summary>
        public int TimeoutInSeconds { get; set; }
    }
}
