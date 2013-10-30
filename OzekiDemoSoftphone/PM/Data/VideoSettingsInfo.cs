using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Ozeki.Media.Video;
using Ozeki.Media.Video.Imaging;

namespace OzekiDemoSoftphone.PM.Data
{
    public class VideoSettingsInfo
    {

        /// <summary>
        /// Camera devices in the system.
        /// </summary>        
        public IEnumerable<VideoDeviceInfo> Devices { get; private set; }

        /// <summary>
        /// The selected device from the video devices.
        /// </summary>
        public int SelectedDevice { get; private set; }


        public Resolution SelectedResolution { get; private set; }

        public VideoSettingsInfo(Resolution resolution, IEnumerable<VideoDeviceInfo> devices, int device)
        {
            SelectedResolution = resolution;
            Devices = devices;
            SelectedDevice = device;
        }
    }
}
