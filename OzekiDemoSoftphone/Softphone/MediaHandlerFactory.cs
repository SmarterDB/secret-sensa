using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Ozeki.Media;
using Ozeki.Media.Audio.Waveform.Formats;
using Ozeki.Media.DSP;
using Ozeki.Media.MediaHandlers;
using Ozeki.Media.MediaHandlers.Facade;
using Ozeki.Media.MediaHandlers.Video;
using Ozeki.VoIP;
using Ozeki.VoIP.Media;

namespace OzekiDemoSoftphone.Softphone
{
    /// <summary>
    /// Creates typical phone call listener scenarios.
    /// </summary>
    public sealed class MediaHandlerFactory
    {
        private readonly Dictionary<CallState, Stream> incomingToneStreams;
        private readonly Dictionary<CallState, Stream> outGoingToneStreams;
        private readonly WaveFormat commonWaveFormat;
        private readonly MediaConnector mediaConnector ;


        /// <summary>
        /// Initializes a new instance of the <see cref="MediaHandlerFactory"/> class.
        /// </summary>
        public MediaHandlerFactory()
        {
            outGoingToneStreams = new Dictionary<CallState, Stream>();
            incomingToneStreams = new Dictionary<CallState, Stream>();
            InitIncomingToneFileNames();
            InitOutGoingToneFileNames();
            mediaConnector=new MediaConnector();
            commonWaveFormat = new WaveFormat();
        }

        private void InitIncomingToneFileNames()
        {
            incomingToneStreams.Add(
                CallState.Ringing,
                Assembly.GetExecutingAssembly().GetManifestResourceStream(
                    "Ozeki.Resources.ringtone.wav"
            ));
        }

        private void InitOutGoingToneFileNames()
        {
            outGoingToneStreams.Add(
                CallState.Ringing,
                Assembly.GetExecutingAssembly().GetManifestResourceStream(
                    "Ozeki.Resources.ringback.wav"
            ));
        }

        /// <summary>
        /// Creates a complex phone call listener, built up from microphone, speaker, waverecorder, waveplayer, dtmf and ringtone sounds.
        /// </summary>
        /// <returns>The media handler collection that represent the phone call listener.</returns>
        public MediaHandlerCollection CreateSoftPhoneCallListener()
        {
            var phoneCallAudioReceiver = new PhoneCallAudioReceiver();
            var phoneCallAudioSender = new PhoneCallAudioSender();

            var microphone =  Microphone.GetDefaultDevice();
            var speaker =  Speaker.GetDefaultDevice();
            

            var audioProcessor = new AudioQualityEnhancer();
            audioProcessor.SetEchoSource(speaker);   // Handles the audio that comes from the PhoneCallListener

            var outgoingDataMixer = new AudioMixerMediaHandler();
            var recordDataMixer = new AudioMixerMediaHandler();
            var speakerMixer = new AudioMixerMediaHandler();

            var dtmf = new DtmfEventWavePlayer();
            var ringtones = new PhoneCallStateWavePlayer(
                commonWaveFormat,
                outGoingToneStreams,
                incomingToneStreams
            );


            if (microphone != null) mediaConnector.Connect(microphone, audioProcessor);
            mediaConnector.Connect(audioProcessor, outgoingDataMixer);
            mediaConnector.Connect(outgoingDataMixer, phoneCallAudioSender);
            // No WaveStreamPlayback (see CreateWaveStreamPlayback method)

            //mediaConnector.Connect(phoneCallAudioReceiver, speaker);
            mediaConnector.Connect(phoneCallAudioReceiver, speakerMixer);
            mediaConnector.Connect(dtmf, speakerMixer);
            mediaConnector.Connect(ringtones, speakerMixer);
            if (speaker != null) mediaConnector.Connect(speakerMixer, speaker);

            mediaConnector.Connect(phoneCallAudioReceiver, recordDataMixer);
            mediaConnector.Connect(outgoingDataMixer, recordDataMixer);


      

            // No WaveStreamRecorder (see CreateWaveStreamRecorder method)

            /*
             * Media Connections:
             *
             * +---------------------------------------------------------------+
             * |                           PhoneCall                           |**************
             * +---------------------------------------------------------------+  *          *
             *              ^^                              VV                    *          *
             *     +--------------------+             +----------------------+  +----+   +---------+
             *     |PhoneCallAudioSender|             |PhoneCallAudioReceiver|  |DMTF|   |Ringtones|
             *     +--------------------+             +----------------------+  +----+   +---------+
             *                ^^                            V              V       V          |
             *       +-----------------+              +---------------+   +------------+      |
             *       |OutgoingDataMixer|----- >> -----|RecordDataMixer|   |SpeakerMixer|--<<--/
             *       +-----------------+              +---------------+   +------------+
             *         ^^          ^^                       V                      |
             *+--------------+   +------------------+    +------------------+      |
             *|AudioProcessor|   |WaveStreamPlayback|    |WaveStreamRecorder|      V
             *+--------------+   +------------------+    +------------------+      V
             *        ^^                                                           |
             *    +----------+                                                 +-------+
             *    |Microphone|                                                 |Speaker|
             *    +----------+                                                 +-------+
             */
            var mediaHandlers = new Dictionary<string, VoIPMediaHandler>
                                    {                                      
                                        {"AudioProcessor", audioProcessor},
                                        {"OutGoingDataMixer", outgoingDataMixer},
                                        {"RecordDataMixer", recordDataMixer},
                                        {"SpeakerMixer", speakerMixer},
                                        {"DTMF", dtmf},
                                        {"Ringtones", ringtones},
                                        
                                        {"AudioSender",phoneCallAudioSender},
                                        {"AudiReceiver",phoneCallAudioReceiver},                                        
        };
            if(microphone != null)
                mediaHandlers.Add("Microphone", microphone);
            if (speaker != null)
                mediaHandlers.Add("Speaker", speaker);

           
            return new MediaHandlerCollection(mediaConnector,mediaHandlers);
        }


         public MediaHandlerCollection CreateSoftPhoneVideoCallListener()
         {
             var phonecallVideoReceiver = new PhoneCallVideoReceiver();
             var phonecallVideoSender = new PhoneCallVideoSender();
             ImageProvider<Image> remoteImageHandler = new DrawingImageProvider();
             ImageProvider<Image> localImageHandler = new DrawingImageProvider();
             var webCamera = WebCamera.GetDefaultDevice();

           
             if (webCamera != null)
             {
                 mediaConnector.Connect(webCamera, phonecallVideoSender);
                 mediaConnector.Connect(webCamera, localImageHandler);
             }
             mediaConnector.Connect(phonecallVideoReceiver, remoteImageHandler);

             var mediaHandlers = new Dictionary<string, VoIPMediaHandler>
                                     {
                                         {"RemoteImageHandler", remoteImageHandler},
                                         {"LocalImageHandler", localImageHandler},
                                         {"VideoSender", phonecallVideoSender},
                                         {"VideoReceiver", phonecallVideoReceiver}

                                     };
             if (webCamera != null)
             {
                 mediaHandlers.Add("WebCamera", webCamera);                 
             }


             return new MediaHandlerCollection(mediaConnector, mediaHandlers);
         }


    }
}
