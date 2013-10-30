using Ozeki.Network;
using Ozeki.VoIP;

namespace OzekiDemoSoftphone.PM.Data
{
    /// <summary>
    /// Phone line data.
    /// </summary>
    /// <remarks>
    /// This is shared data about phoneline beetwen model and the presentation layer.
    /// </remarks>
    public class PhoneLineInfo
    {
        /// <summary>
        /// Display name.
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// User name.
        /// </summary>
        public string Username { get; private set; }

        /// <summary>
        /// Register name.
        /// </summary>
        public string RegisterName { get; private set; }

        /// <summary>
        /// Password.
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// Register domain, or direct connection.
        /// </summary>
        public string Domain { get; private set; }

        /// <summary>
        /// Outbound Proxy
        /// </summary>
        public string Proxy { get; private set; }

        /// <summary>
        /// Registration required.
        /// </summary>
        /// <remarks>
        /// If this is false, the given domain is the direct address of the SIP device.
        /// </remarks>
        public bool RegRequired { get; private set; }

        public TransportType TransportType { get; private set; }

        public SRTPMode SrtpMode { get;  set; }


        /// <summary>
        /// Creates phone line information.
        /// </summary>
        /// <param name="displayName">Display name.</param>
        /// <param name="userName">User name.</param>
        /// <param name="registerName">Register name.</param>
        /// <param name="password">Password.</param>
        /// <param name="domain">Register domain, or direct connection.</param>
        /// <param name="regReq">Registration required.</param>
        public PhoneLineInfo(
            string displayName,
            string userName,
            string registerName,
            string password,
            string domain,
            string proxy,
            bool regReq,
            TransportType transportType,
            SRTPMode srtpMode)
        {
            DisplayName = displayName;
            Username = userName;
            RegisterName = registerName;
            Password = password;
            Domain = domain;
            Proxy = proxy;
            RegRequired = regReq;
            TransportType = transportType;
            SrtpMode = srtpMode;
        }

        /// <summary>
        /// Equality comparer.
        /// </summary>
        /// <param name="obj">Meant to be other PhoneLineInfo object.</param>
        /// <returns>Returns true if the two objects are equal, otherwise false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;

            PhoneLineInfo other = obj as PhoneLineInfo;
            if(other == null)
                return false;

            return
                DisplayName.Equals(other.DisplayName) &&
                Username.Equals(other.Username) &&
                RegisterName.Equals(other.RegisterName) &&
                Password.Equals(other.Password) &&
                Domain.Equals(other.Domain) &&
                Proxy.Equals(other.Proxy) &&
                RegRequired.Equals(other.RegRequired);
        }

        /// <summary>
        /// Calculates hash code based on the contained data.
        /// </summary>
        /// <returns>The hash code of the object.</returns>
        public override int GetHashCode()
        {
            return
                DisplayName.GetHashCode() +
                Username.GetHashCode() +
                RegisterName.GetHashCode() +
                Password.GetHashCode() +
                Domain.GetHashCode() +
                Proxy.GetHashCode() +
                RegRequired.GetHashCode();
        }

        /// <summary>
        /// Simply overrides ToString().
        /// </summary>
        /// <returns>Description of the phone line.</returns>
        public override string ToString()
        {
            return Username + "@" + Domain;
        }

    }
}
