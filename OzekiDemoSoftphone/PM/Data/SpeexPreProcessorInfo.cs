namespace OzekiDemoSoftphone.PM.Data
{
    /// <summary>
    /// Speex Preprocessor info for events
    /// </summary>
    public class SpeexPreProcessorInfo
    {
        /// <summary>
        /// 0=AGC, 1=VAD, 2=Denoise
        /// </summary>
        public int ComponentId { get; set; }

        /// <summary>
        /// Indicates whether the component is enabled or disabled
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="componentId">0=AGC, 1=VAD, 2=Denoise</param>
        /// <param name="isEnabled"></param>
        public SpeexPreProcessorInfo(int componentId, bool isEnabled)
        {
            ComponentId = componentId;
            IsEnabled = isEnabled;
        }
    }
}
