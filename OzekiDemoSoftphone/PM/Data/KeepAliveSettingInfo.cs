namespace OzekiDemoSoftphone.PM.Data
{
    /// <summary>
    /// Keep alive settings data.
    /// </summary>
    public class KeepAliveSettingInfo
    {
        /// <summary>
        /// The desired interval of keep alive packets in seconds.
        /// </summary>
        public int Interval { get; private set; }

        /// <summary>
        /// Constructs a KeepAliveSettingsInfo data object.
        /// </summary>
        /// <param name="interval">The desired interval of keep alive packets in seconds.</param>
        public KeepAliveSettingInfo(int interval)
        {
            Interval = interval;
        }

        /// <summary>
        /// Determines whether the specified Object is equal to the current Object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;

            KeepAliveSettingInfo other = obj as KeepAliveSettingInfo;
            if (other == null)
                return false;

            return other.Interval.Equals(Interval);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Interval.GetHashCode();
        }
    }
}
