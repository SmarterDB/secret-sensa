using Ozeki.Network.Nat;

namespace OzekiDemoSoftphone.PM.Data
{
    /// <summary>
    /// NAT Settings information
    /// </summary>
    public class NATSettingsInfo
    {
        /// <summary>
        /// Gets the type of the NAT Traversal method (eg. STUN, ICE...)
        /// </summary>
        public NatTraversalMethod TraversalMethodType { get; private set; }

        /// <summary>
        /// Gets the transport address of the server
        /// </summary>
        public string ServerAddress { get; private set; }

        /// <summary>
        /// Gets the username used for authentication with the server
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// Gets the password used for authentication with the server
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// Constructs a NATSettingsInfo data object.
        /// </summary>
        /// <param name="traversalMethodType"></param>
        /// <param name="serverAddress"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public NATSettingsInfo(NatTraversalMethod traversalMethodType, string serverAddress, string userName, string password)
        {
            TraversalMethodType = traversalMethodType;
            ServerAddress = serverAddress;
            UserName = userName;
            Password = password;
        }
    }
}
