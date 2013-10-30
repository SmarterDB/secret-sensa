using Ozeki.VoIP;

namespace OzekiDemoSoftphone.PM.Data
{
    public enum CallDirection
    {
        Incoming,
        Outgoing
    }

    /// <summary>
    /// Provides information about a phone call
    /// </summary>
    /// <remarks>
    /// This is shared data about phoneline beetwen model and the presentation layer.
    /// </remarks>
    public class PhoneCallInfo
    {
        /// <summary>
        /// The identifier of the call.
        /// </summary>
        public string CallID { get; private set; }

        /// <summary>
        /// The call has information about its phone line.
        /// </summary>
        public PhoneLineInfo PhoneLineInfo { get; private set; }

        /// <summary>
        /// The dialed string.
        /// </summary>
        public string Dial { get; private set; }

        /// <summary>
        /// Gets or sets the type of the call (incoming/outgoing)
        /// </summary>
        public CallDirection Direction { get; private set; }

        /// <summary>
        /// Creates phone call information for call history.
        /// </summary>
        /// <param name="phoneLineInfo">Associated phone line information.</param>
        /// <param name="dial">Dialed string.</param>
        /// <param name="type">The type of the call (incoming/outgoing).</param>
        public PhoneCallInfo(PhoneLineInfo phoneLineInfo, string dial, CallDirection type)
        {
            CallID = "";
            PhoneLineInfo = phoneLineInfo;
            Dial = dial;
            Direction = type;
        }

        /// <summary>
        /// Creates phone call information.
        /// </summary>
        /// /// <param name="callID">The identifier of the call.</param>
        /// <param name="phoneLineInfo">Associated phone line information.</param>
        /// <param name="dial">Dialed string.</param>
        /// <param name="type">The type of the call (incoming/outgoing).</param>
        public PhoneCallInfo(string callID, PhoneLineInfo phoneLineInfo, string dial, CallDirection type)
        {
            CallID = callID;
            PhoneLineInfo = phoneLineInfo;
            Dial = dial;
            Direction = type;
        }

        /// <summary>
        /// Equality comparer.
        /// </summary>
        /// <param name="obj">Meant to be other PhoneCallInfo object.</param>
        /// <returns>Returns true if the two objects are equal, otherwise false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;

            PhoneCallInfo other = obj as PhoneCallInfo;
            if (other == null)
                return false;

            return
                CallID.Equals(other.CallID) &&
                PhoneLineInfo.Equals(other.PhoneLineInfo);
        }

        /// <summary>
        /// Calculates hash code based on the contained data.
        /// </summary>
        /// <returns>The hash code of the object.</returns>
        public override int GetHashCode()
        {
            return
                CallID.GetHashCode() + 
                Direction.GetHashCode();
        }

        /// <summary>
        /// Simply overrides ToString().
        /// </summary>
        /// <returns>Description of the phone call.</returns>
        public override string ToString()
        {
            string dir = (Direction == CallDirection.Outgoing) ? "out" : "in";
            return string.Format("({0}) {1} {2}", dir, Dial, PhoneLineInfo);
        }
    }
}
