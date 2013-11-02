using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Ozeki.Media.DSP;
using Ozeki.Media.MediaHandlers;
using Ozeki.Media;
using Ozeki.VoIP;
using Ozeki.Media.MediaHandlers.Video;
using Ozeki.Media.Video;
using Ozeki.Media.Video.Imaging;
using System.Windows.Media.Imaging;
using Ozeki.Media.Audio;
using TestSoftphone;
using System.ComponentModel;
using Ozeki.Media.MediaHandlers.Facade;

namespace secret_sensa.Model
{
    public class MediaHandlers : INotifyPropertyChanged, IDisposable
    {
        #region Fields

        private MediaConnector audioConnector;
        private MediaHandlerCollection audioCollection;

        private MediaConnector videoConnector;
        private MediaHandlerCollection videoCollection;

        private bool initialized;

        #endregion

        #region Audio Handlers

        public Microphone Microphone { get; private set; }
        public Speaker Speaker { get; private set; }
        public AudioQualityEnhancer AudioEnhancer { get; private set; }
        private DtmfEventWavePlayer dtmfPlayer;

        // mixers
        private AudioMixerMediaHandler outgoingDataMixer;
        private AudioMixerMediaHandler speakerMixer;
        private AudioMixerMediaHandler recordDataMixer;

        // phone call handlers
        private PhoneCallAudioSender phoneCallAudioSender;
        private PhoneCallAudioReceiver phoneCallAudioReceiver;

        // audio files
        private WaveStreamRecorder wavRecorder;
        private WaveStreamPlayback wavPlayer;
        private MP3StreamPlayback mp3StreamPlayback;
        private WaveStreamPlayback ringtonePlayer;
        private WaveStreamPlayback ringbackPlayer;

        #endregion

        #region Video Handlers

        public WebCamera WebCamera { get; private set; }

        // image providers
        public ImageProvider<BitmapSource> LocalImageProvider { get; private set; }
        public ImageProvider<BitmapSource> RemoteImageProvider { get; private set; }

        // phone call handlers
        private PhoneCallVideoSender phoneCallVideoSender;
        private PhoneCallVideoReceiver phoneCallVideoReceiver;

        #endregion

        #region Other Properties

        /// <summary>
        /// Gets the available noise reduction levels.
        /// </summary>
        public List<NoiseReductionLevel> NoiseReductionLevels { get; private set; }

        /// <summary>
        /// Gets the level of the microphone.
        /// </summary>
        public float MicrophoneLevel { get; private set; }

        /// <summary>
        /// Gets the level of the speaker.
        /// </summary>
        public float SpeakerLevel { get; private set; }

        /// <summary>
        /// Gets the available microphone devices.
        /// </summary>
        public List<DeviceInfo> Microphones
        {
            get { return Microphone.GetDevices(); }
        }

        /// <summary>
        /// Gets the available speaker devices.
        /// </summary>
        public List<DeviceInfo> Speakers
        {
            get { return Speaker.GetDevices(); }
        }

        /// <summary>
        /// Gets the available camera devices.
        /// </summary>
        public List<VideoDeviceInfo> Cameras
        {
            get { return WebCamera.GetDevices(); }
        }

        /// <summary>
        /// Gets the frame rates that can be set to the camera.
        /// </summary>
        public List<int> FrameRates { get; private set; }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when the level of the microphone has changed.
        /// </summary>
        public event EventHandler<GEventArgs<float>> MicrophoneLevelChanged;

        /// <summary>
        /// Occurs when the the microphone stopped recording.
        /// </summary>
        public event EventHandler<EventArgs> MicrophoneStopped;

        /// <summary>
        /// Occurs when the level of the speaker has changed.
        /// </summary>
        public event EventHandler<GEventArgs<float>> SpeakerLevelChanged;

        /// <summary>
        /// Occurs when the the speaker stopped playing.
        /// </summary>
        public event EventHandler<EventArgs> SpeakerStopped;

        #endregion

        #region Init

        public MediaHandlers()
        {
            NoiseReductionLevels = new List<NoiseReductionLevel>();
            NoiseReductionLevels.Add(NoiseReductionLevel.NoReduction);
            NoiseReductionLevels.Add(NoiseReductionLevel.Low);
            NoiseReductionLevels.Add(NoiseReductionLevel.Medium);
            NoiseReductionLevels.Add(NoiseReductionLevel.High);

            FrameRates = new List<int> {0, 30, 25, 20, 15, 10, 5};

            audioConnector = new MediaConnector();
            videoConnector = new MediaConnector();
        }

        public void Init()
        {
            if (initialized)
                return;

            InitAudio();
            InitVideo();

            if (Microphone != null)
            {
                SubscribeToMicrophoneEvents();
                Microphone.Start();
            }

            if (Speaker != null)
            {
                SubscribeToSpeakerEvents();
                //Speaker.Start();
            }

            initialized = true;

            /*
             * Media Connections:
             *
             * +---------------------------------------------------------------+
             * |                           PhoneCall                           |***********
             * +---------------------------------------------------------------+          *
             *              ^^                              VV                            *
             *     +--------------------+          +----------------------+  +----+   +---------+  +-----+
             *     |PhoneCallAudioSender|          |PhoneCallAudioReceiver|  |DMTF|   |Ringtones|  | TTS |
             *     +--------------------+          +----------------------+  +----+   +---------+  +-----+
             *                ^^                          V             V       V        |           |
             *       +-----------------+         +---------------+   +------------+      |           |
             *       |OutgoingDataMixer|--->>----|RecordDataMixer|   |SpeakerMixer|--<<--/----<<-----/
             *       +-----------------+         +---------------+   +------------+
             *         ^^          ^^                V                  |
             *+--------------+   +---------+    +-----------+           |
             *|AudioEnhancer |   |WavPlayer|    |WavRecorder|           V
             *+--------------+   +---------+    +-----------+           V
             *        ^^                                                |
             *    +----------+                                       +-------+
             *    |Microphone|                                       |Speaker|
             *    +----------+                                       +-------+
             */
        }

        /// <summary>
        /// Initializes the audio handlers (microphone, speaker, DTMF player etc.).
        /// </summary>
        private void InitAudio()
        {
            // ----- CREATE -----
            // devices
            Microphone = Microphone.GetDefaultDevice();
            Speaker = Speaker.GetDefaultDevice();

            // audio processors
            AudioEnhancer = new AudioQualityEnhancer();
            AudioEnhancer.SetEchoSource(Speaker);
            dtmfPlayer = new DtmfEventWavePlayer();

            // ringtones
            var ringbackStream = LoadRingbackStream();
            var ringtoneStream = LoadRingtoneStream();
            ringtonePlayer = new WaveStreamPlayback(ringtoneStream, true, false);
            ringbackPlayer = new WaveStreamPlayback(ringbackStream, true, false);

            // mixers
            outgoingDataMixer = new AudioMixerMediaHandler();
            speakerMixer = new AudioMixerMediaHandler();
            recordDataMixer = new AudioMixerMediaHandler();

            // phone handlers
            phoneCallAudioSender = new PhoneCallAudioSender();
            phoneCallAudioReceiver = new PhoneCallAudioReceiver();


            // ----- CONNECT -----
            // connect outgoing
            audioConnector.Connect(AudioEnhancer, outgoingDataMixer);
            audioConnector.Connect(outgoingDataMixer, phoneCallAudioSender);
            audioConnector.Connect(outgoingDataMixer, recordDataMixer);
            if (Microphone != null)
            {
                Microphone.LevelChanged += (Microphone_LevelChanged);
                audioConnector.Connect(Microphone, AudioEnhancer);
            }

            // connect incoming
            audioConnector.Connect(phoneCallAudioReceiver, speakerMixer);
            audioConnector.Connect(phoneCallAudioReceiver, recordDataMixer);
            audioConnector.Connect(ringtonePlayer, speakerMixer);
            audioConnector.Connect(ringbackPlayer, speakerMixer);
            audioConnector.Connect(dtmfPlayer, speakerMixer);
            if (Speaker != null)
            {
                Speaker.LevelChanged += (Speaker_LevelChanged);
                audioConnector.Connect(speakerMixer, Speaker);
            }

            // add to collection
            Dictionary<string, VoIPMediaHandler> collection = new Dictionary<string, VoIPMediaHandler>();
            collection.Add("AudioEnhancer", AudioEnhancer);
            collection.Add("DTMFPlayer", dtmfPlayer);
            collection.Add("OutgoingDataMixer", outgoingDataMixer);
            collection.Add("SpeakerMixer", speakerMixer);
            collection.Add("PhoneCallAudioSender", phoneCallAudioSender);
            collection.Add("PhoneCallAudioReceiver", phoneCallAudioReceiver);
            audioCollection = new MediaHandlerCollection(audioConnector, collection);
        }

        private Stream LoadRingbackStream()
        {
            Stream ringback = Assembly.GetExecutingAssembly().GetManifestResourceStream(
                "secret_sensa.Resources.ringback.wav");

            if (ringback == null)
                throw new Exception("Cannot load default ringback.");

            return ringback;
        }

        private Stream LoadRingtoneStream()
        {
            Stream basicRing = Assembly.GetExecutingAssembly().GetManifestResourceStream(
                "secret_sensa.Resources.basic_ring.wav");

            if (basicRing == null)
                throw new Exception("Cannot load default ringtone.");

            return basicRing;
        }

        /// <summary>
        /// Initializes the video handlers (camera, image providers etc.).
        /// </summary>
        private void InitVideo()
        {
            // ----- CREATE -----
            WebCamera = WebCamera.GetDefaultDevice();
            
            LocalImageProvider = new BitmapSourceProvider();
            RemoteImageProvider = new BitmapSourceProvider();

            phoneCallVideoReceiver = new PhoneCallVideoReceiver();
            phoneCallVideoSender = new PhoneCallVideoSender();

            // ----- CONNECT -----
            videoConnector.Connect(phoneCallVideoReceiver, RemoteImageProvider);
            if (WebCamera != null)
            {
                videoConnector.Connect(WebCamera, LocalImageProvider);
                videoConnector.Connect(WebCamera, phoneCallVideoSender);
            }

            // add to collection
            Dictionary<string, VoIPMediaHandler> collection = new Dictionary<string, VoIPMediaHandler>();
            collection.Add("LocalImageProvider", LocalImageProvider);
            collection.Add("RemoteImageProvider", RemoteImageProvider);
            collection.Add("PhoneCallVideoReceiver", phoneCallVideoReceiver);
            collection.Add("PhoneCallVideoSender", phoneCallVideoSender);
            videoCollection = new MediaHandlerCollection(videoConnector, collection);
        }

        #endregion

        #region Invocators

        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnSpeakerLevelChanged(float value)
        {
            var handler = SpeakerLevelChanged;
            if (handler != null)
                handler(this, new GEventArgs<float>(value));
        }

        private void OnMicrophoneLevelChanged(float value)
        {
            var handler = MicrophoneLevelChanged;
            if (handler != null)
                handler(this, new GEventArgs<float>(value));
        }

        #endregion

        #region Audio EventHandlers

        /// <summary>
        /// This will be called when the level of the microphone has changed.
        /// </summary>
        private void Microphone_LevelChanged(object sender, VoIPEventArgs<float> e)
        {
            MicrophoneLevel = e.Item;
            OnPropertyChanged("MicrophoneLevel");

            OnMicrophoneLevelChanged(e.Item);
        }

        /// <summary>
        /// This will be called when the level of the speaker has changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Speaker_LevelChanged(object sender, VoIPEventArgs<float> e)
        {
            SpeakerLevel = e.Item;
            OnPropertyChanged("SpeakerLevel");

            OnSpeakerLevelChanged(e.Item);
        }

        #endregion

        #region Microphone

        /// <summary>
        /// Changes the microphone device.
        /// </summary>
        public void ChangeMicrophone(DeviceInfo deviceInfo)
        {
            if (!initialized)
                return;

            float prevVolume = 0;
            bool prevMuted = false;

            if (Microphone != null)
            {
                // same device
                if (Microphone.DeviceInfo.Equals(deviceInfo))
                    return;

                // backup settings
                prevVolume = Microphone.Volume;
                prevMuted = Microphone.Muted;

                // dispose previous device
                audioConnector.Disconnect(Microphone, AudioEnhancer);
                UnsubscribeFromMicrophoneEvents();
                Microphone.Dispose();
            }

            // create new microphone
            Microphone = Microphone.GetDevice(deviceInfo);

            if (Microphone != null)
            {
                SubscribeToMicrophoneEvents();
                audioConnector.Connect(Microphone, AudioEnhancer);

                // set prev device settings
                Microphone.Volume = prevVolume;
                Microphone.Muted = prevMuted;
                Microphone.Start();
            }

            OnPropertyChanged("Microphone");
        }

        private void SubscribeToMicrophoneEvents()
        {
            if (Microphone == null)
                return;

            Microphone.LevelChanged += (Microphone_LevelChanged);
            Microphone.Stopped += Microphone_Stopped;
        }

        private void UnsubscribeFromMicrophoneEvents()
        {
            if (Microphone == null)
                return;

            Microphone.LevelChanged -= (Microphone_LevelChanged);
            Microphone.Stopped -= Microphone_Stopped;
        }

        void Microphone_Stopped(object sender, EventArgs e)
        {
            var handler = MicrophoneStopped;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        #endregion

        #region Speaker

        /// <summary>
        /// Changes the speaker device and sets the volume and muted property of the new device.
        /// </summary>
        public void ChangeSpeaker(DeviceInfo deviceInfo)
        {
            if (!initialized)
                return;

            float prevVolume = 0;
            bool prevMuted = false;

            if (Speaker != null)
            {
                // same device
                if (Speaker.DeviceInfo.Equals(deviceInfo))
                    return;

                // backup settings
                prevVolume = Speaker.Volume;
                prevMuted = Speaker.Muted;

                // dispose previous device
                audioConnector.Disconnect(speakerMixer, Speaker);
                UnsubscribeFromSpeakerEvents();
                Speaker.Dispose();

                AudioEnhancer.SetEchoSource(null);
            }

            // create new microphone
            Speaker = Speaker.GetDevice(deviceInfo);

            if (Speaker != null)
            {
                SubscribeToSpeakerEvents();
                audioConnector.Connect(speakerMixer, Speaker);

                // set prev device settings
                Speaker.Volume = prevVolume;
                Speaker.Muted = prevMuted;
                Speaker.Start();

                AudioEnhancer.SetEchoSource(Speaker);
            }

            OnPropertyChanged("Speaker");
        }

        private void SubscribeToSpeakerEvents()
        {
            if (Speaker == null)
                return;

            Speaker.LevelChanged += (Speaker_LevelChanged);
            Speaker.Stopped += Speaker_Stopped;
        }

        private void UnsubscribeFromSpeakerEvents()
        {
            if (Speaker == null)
                return;

            Speaker.LevelChanged -= (Speaker_LevelChanged);
            Speaker.Stopped -= Speaker_Stopped;
        }

        void Speaker_Stopped(object sender, EventArgs e)
        {
            var handler = SpeakerStopped;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        #endregion

        #region Camera

        /// <summary>
        /// Changes the camera device.
        /// </summary>
        public void ChangeCamera(int deviceID)
        {
            if (!initialized)
                return;

            // same device
            if (WebCamera != null && WebCamera.DeviceID == deviceID)
                return;

            // find the proper info
            VideoDeviceInfo newDeviceInfo = null;
            foreach (var info in Cameras)
            {
                if (info.DeviceID != deviceID)
                    continue;

                newDeviceInfo = info;
                break;
            }

            if (newDeviceInfo == null)
                return;


            // begin change device
            bool capturing = false;

            if (WebCamera != null)
            {
                // disconnect
                if (LocalImageProvider != null)
                    audioConnector.Disconnect(WebCamera, LocalImageProvider);
                audioConnector.Disconnect(WebCamera, phoneCallVideoSender);

                // dispose previous device
                capturing = WebCamera.Capturing;
                WebCamera.Stop();
                WebCamera.Dispose();
            }

            // create new
            WebCamera = WebCamera.GetDevice(newDeviceInfo);

            if (WebCamera != null)
            {
                audioConnector.Connect(WebCamera, LocalImageProvider);
                audioConnector.Connect(WebCamera, phoneCallVideoSender);

                if (capturing)
                    WebCamera.Start();
            }

            OnPropertyChanged("WebCamera");
        }

        /// <summary>
        /// Gets the supported video resolutions of the camera.
        /// </summary>
        /// <returns></returns>
        public List<Resolution> GetVideoResolutions()
        {
            var resolutions = new List<Resolution>();

            if (WebCamera == null)
                return resolutions;

            List<VideoCapabilities> capabilities = WebCamera.Capabilities;
            if (capabilities == null)
                return resolutions;

            resolutions.AddRange(capabilities.Select(cap => new Resolution(cap.Resolution.Width, cap.Resolution.Height)));

            return resolutions;
        }

        #endregion

        #region DTMF

        /// <summary>
        /// Starts playing the DTMF signal.
        /// </summary>
        internal void StartDtmf(DtmfNamedEvents signal)
        {
            if (!initialized)
                return;

            dtmfPlayer.Start(signal);
        }

        /// <summary>
        /// Starts playing the DTMF signal.
        /// </summary>
        internal void StartDtmf(int signal)
        {
            if (!initialized)
                return;

            dtmfPlayer.Start(signal);
        }

        /// <summary>
        /// Stops playing the DTMF signal.
        /// </summary>
        internal void StopDtmf(DtmfNamedEvents signal)
        {
            if (!initialized)
                return;

            dtmfPlayer.Stop();
        }

        /// <summary>
        /// Stops playing the DTMF signal.
        /// </summary>
        internal void StopDtmf(int signal)
        {
            if (!initialized)
                return;

            dtmfPlayer.Stop();
        }

        #endregion

        #region Wav Playback

        /// <summary>
        /// Loads a wav file for playback.
        /// </summary>
        public void LoadPlaybackWavFile(string filePath)
        {
            if (!initialized)
                return;

            if (wavPlayer != null)
            {
                audioConnector.Disconnect(wavPlayer, outgoingDataMixer);
                wavPlayer.Dispose();
                wavRecorder = null;
            }

            wavPlayer = new WaveStreamPlayback(filePath, false, true);

            audioConnector.Connect(wavPlayer, outgoingDataMixer);
        }

        /// <summary>
        /// Starts streaming the wav file.
        /// </summary>
        public void StartWavPlayback()
        {
            if (wavPlayer == null)
                return;

            wavPlayer.StartStreaming();
        }

        /// <summary>
        /// Pauses the wav file streaming.
        /// </summary>
        public void PauseWavPlayback()
        {
            if (wavPlayer == null)
                return;

            wavPlayer.PauseStreaming();
        }

        /// <summary>
        /// Stops streaming the wav file.
        /// </summary>
        public void StopWavPlayback()
        {
            if (wavPlayer == null)
                return;

            wavPlayer.StopStreaming();
        }

        #endregion

        #region Wav Record

        /// <summary>
        /// Loads a wav file for playback.
        /// </summary>
        public void LoadRecordWavFile(string filePath)
        {
            if (!initialized)
                return;

            if (wavRecorder != null)
            {
                audioConnector.Disconnect(recordDataMixer, wavRecorder);
                wavRecorder.Dispose();
                wavRecorder = null;
            }

            wavRecorder = new WaveStreamRecorder(filePath);

            audioConnector.Connect(recordDataMixer, wavRecorder);
        }

        /// <summary>
        /// Starts recording the audio into a wav file.
        /// </summary>
        public void StartWavRecording()
        {
            if (wavRecorder == null)
                return;

            wavRecorder.StartStreaming();
        }

        /// <summary>
        /// Pauses the wav recording.
        /// </summary>
        public void PauseWavRecording()
        {
            if (wavRecorder == null)
                return;

            wavRecorder.PauseStreaming();
        }

        /// <summary>
        /// Stops wav recording.
        /// </summary>
        public void StopWavRecording()
        {
            if (wavRecorder == null)
                return;

            wavRecorder.StopStreaming();
            wavRecorder.Dispose();
        }

        #endregion

        #region MP3 Playback

        /// <summary>
        /// Loads an mp3 file for playback.
        /// </summary>
        public void LoadPlaybackMP3File(string filePath)
        {
            if (!initialized)
                return;

            if (mp3StreamPlayback != null)
            {
                audioConnector.Disconnect(mp3StreamPlayback, outgoingDataMixer);
                mp3StreamPlayback.Dispose();
                mp3StreamPlayback = null;
            }

            mp3StreamPlayback = new MP3StreamPlayback(filePath, false, true);

            audioConnector.Connect(mp3StreamPlayback, outgoingDataMixer);
        }

        /// <summary>
        /// Starts recording the audio into a wav file.
        /// </summary>
        public void StartMP3Playback()
        {
            if (mp3StreamPlayback == null)
                return;

            mp3StreamPlayback.StartStreaming();
        }

        /// <summary>
        /// Pauses the wav recording.
        /// </summary>
        public void PauseMP3Playback()
        {
            if (mp3StreamPlayback == null)
                return;

            mp3StreamPlayback.PauseStreaming();
        }

        /// <summary>
        /// Stops wav recording.
        /// </summary>
        public void StopMP3Playback()
        {
            if (mp3StreamPlayback == null)
                return;

            mp3StreamPlayback.StopStreaming();
        }

        #endregion

        #region Ringtones

        internal void SetRingtone(string filePath)
        {
            bool isStreaming = false;

            // dispose previous
            if (ringtonePlayer != null)
            {
                isStreaming = ringtonePlayer.IsStreaming;

                audioConnector.Disconnect(ringtonePlayer, speakerMixer);
                ringtonePlayer.Dispose();
                ringtonePlayer = null;
            }

            // create new
            ringtonePlayer = new WaveStreamPlayback(filePath, true, true);

            audioConnector.Connect(ringtonePlayer, speakerMixer);

            // start if necessary
            if (isStreaming)
                ringtonePlayer.StartStreaming();
        }

        public void StartRingtone()
        {
            if (ringtonePlayer != null)
                ringtonePlayer.StartStreaming();
        }

        public void StopRingtone()
        {
            if (ringtonePlayer != null)
                ringtonePlayer.StopStreaming();
        }


        internal void SetRingback(string filePath)
        {
            bool isStreaming = false;

            // dispose previous
            if (ringbackPlayer != null)
            {
                isStreaming = ringbackPlayer.IsStreaming;
                audioConnector.Disconnect(ringbackPlayer, speakerMixer);
                ringbackPlayer.Dispose();
                ringbackPlayer = null;
            }

            // create new
            ringbackPlayer = new WaveStreamPlayback(filePath, true, true);
            audioConnector.Connect(ringbackPlayer, speakerMixer);

            // start if necessary
            if (isStreaming)
                ringbackPlayer.StartStreaming();
        }


        public void StartRingback()
        {
            if (ringbackPlayer != null)
                ringbackPlayer.StartStreaming();
        }

        public void StopRingback()
        {
            if (ringbackPlayer != null)
                ringbackPlayer.StopStreaming();
        }

        #endregion

        #region Audio Control

        /// <summary>
        /// Attaches the media handlers to the given phone call.
        /// </summary>
        public void AttachAudio(IPhoneCall call)
        {
            phoneCallAudioSender.AttachToCall(call);
            if (audioCollection != null)
                audioCollection.AttachToCall(call);
        }

        /// <summary>
        /// Detaches the media handlers from the phone call.
        /// </summary>
        public void DetachAudio()
        {
            if (audioCollection != null)
                audioCollection.Detach();
        }

        #endregion

        #region Video Control

        /// <summary>
        /// Attaches the media handlers to the given phone call.
        /// </summary>
        public void AttachVideo(IPhoneCall call)
        {
            if (videoCollection != null)
                videoCollection.AttachToCall(call);
        }

        /// <summary>
        /// Detaches the media handlers from the phone call.
        /// </summary>
        public void DetachVideo()
        {
            if (videoCollection != null)
                videoCollection.Detach();
        }

        /// <summary>
        /// Starts the video handlers.
        /// </summary>
        public void StartVideo()
        {
            if (WebCamera != null)
                WebCamera.Start();

            if (videoCollection != null)
                videoCollection.Start();
        }

        /// <summary>
        /// Stops the video handlers.
        /// </summary>
        public void StopVideo()
        {
            if (WebCamera != null)
                WebCamera.Stop();

            if (videoCollection != null)
                videoCollection.Stop();
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            // audio
            if (audioCollection != null)
                audioCollection.Dispose();

            if (audioConnector != null)
                audioConnector.Dispose();

            if (Microphone != null)
            {
                UnsubscribeFromMicrophoneEvents();
                Microphone.Dispose();
            }

            if (Speaker != null)
            {
                UnsubscribeFromSpeakerEvents();
                Speaker.Dispose();
            }

            if (wavRecorder != null)
                wavRecorder.Dispose();

            if (wavPlayer != null)
                wavPlayer.Dispose();

            if (mp3StreamPlayback != null)
                mp3StreamPlayback.Dispose();

            if (ringtonePlayer != null)
                ringtonePlayer.Dispose();

            if (ringbackPlayer != null)
                ringbackPlayer.Dispose();

            // video
            if (videoCollection != null)
                videoCollection.Dispose();

            if (videoConnector != null)
                videoConnector.Dispose();

            if (WebCamera != null)
                WebCamera.Dispose();
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
