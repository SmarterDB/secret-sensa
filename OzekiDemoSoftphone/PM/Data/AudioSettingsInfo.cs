using System.Collections.Generic;
using Ozeki.Media.Audio;

namespace OzekiDemoSoftphone.PM.Data
{
    /// <summary>
    /// Audio settings data.
    /// </summary>
    public class AudioSettingsInfo
    {
        /// <summary>
        /// The volume of speaker.
        /// </summary>        
        public float Volume { get; private set; }

        /// <summary>
        /// Is muted?
        /// </summary>
        public bool Mute { get; private set; }

        /// <summary>
        /// Sound devices in the system.
        /// </summary>        
        public IEnumerable<DeviceInfo> Devices { get; private set; }

        /// <summary>
        /// The selected device from the sound devices.
        /// </summary>
        public string SelectedDevice { get; private set; }

        /// <summary>
        /// Constructs a SpeakerSettingsInfo data object.
        /// </summary>
        /// <param name="volume">The volume of speaker.</param>
        /// <param name="mute">Is muted?</param>
        /// <param name="devices">Possibly sound devices in the system.</param>
        /// <param name="device">The selected device from the sound devices.</param>
        public AudioSettingsInfo(float volume, bool mute, IEnumerable<DeviceInfo> devices, string device)
        {
            Volume = volume;
            Mute = mute;
            Devices = devices;
            SelectedDevice = device;
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

            AudioSettingsInfo other = obj as AudioSettingsInfo;
            if (obj == null)
                return false;

            if (!(Volume.Equals(other.Volume) &&
                 Mute.Equals(other.Mute) &&
                 Devices.Equals(other.Devices)))
                return false;

            IEnumerator<DeviceInfo> otherEnumerator = other.Devices.GetEnumerator();
            IEnumerator<DeviceInfo> ownEnumerator = Devices.GetEnumerator();

            while (ownEnumerator.MoveNext() && otherEnumerator.MoveNext())
                if (!ownEnumerator.Current.Equals(otherEnumerator.Current))
                    return false;

            return ownEnumerator.MoveNext() != otherEnumerator.MoveNext();
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int code = Volume.GetHashCode();
            code += Mute.GetHashCode();
            code += SelectedDevice.GetHashCode();

            foreach (object device in Devices)
                code += device.GetHashCode();

            return code;
        }

    }
}
