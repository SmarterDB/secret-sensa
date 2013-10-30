using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using Ozeki.Common;
using Ozeki.Common.Logger;
using Ozeki.Media;
using Ozeki.Media.Audio;
//using Ozeki.Media.Codec;
using Ozeki.Media.Codec;
using Ozeki.Media.DSP;
using Ozeki.Media.MediaHandlers;
using Ozeki.Media.MediaHandlers.Facade;
using Ozeki.Media.MediaHandlers.Video;
using Ozeki.Media.Video;
using Ozeki.Media.Video.Imaging;
using Ozeki.Network.Nat;
using Ozeki.VoIP;
using Ozeki.VoIP.Media;
using Ozeki.VoIP.MessageSummary;
using Ozeki.VoIP.SDK;
using Ozeki.VoIP.SIP;
using Ozeki.VoIP.SIP.Logger;
using OzekiDemoSoftphone.PM;
using OzekiDemoSoftphone.PM.Data;
using OzekiDemoSoftphone.Utils;

namespace OzekiDemoSoftphone.Softphone
{
    ///<summary>
    ///The SoftPhone Engine.
    ///</summary>
    ///<remarks>
    ///We use the singleton pattern to manage the SoftPhone, and write 
    ///a simple presentation about the usage of VoIP SDK. The SFE is an 
    ///IModel for the GUI. And also the SFE must be IPhoneLineListener 
    ///as react to some events of the phone lines.
    ///</remarks>
    public class SoftphoneEngine : INotifyPropertyChanged 
    {
        #region Common Fields, Properties

        private static SoftphoneEngine Singleton;

        /// <summary>
        /// Returns the only softphone object in the application.
        /// </summary>
        public static SoftphoneEngine Instance
        {
            get
            {
                if (Singleton != null)
                    return Singleton;

                Singleton = new SoftphoneEngine();
                return Singleton;
            }
        }

        /// <summary>
        /// The SoftPhone is provided by the VoIPSDK.
        /// </summary>
        private ISoftPhone softPhone;

        /// <summary>
        /// Will be attached to calls.
        /// </summary>
        private PhoneCallAudioReceiver phoneCallAudioReceiver;

        /// <summary>
        /// Will be attached to calls.
        /// </summary>
        private PhoneCallAudioSender phoneCallAudioSender;

        /// <summary>
        /// Will be attached to calls.
        /// </summary>
        private PhoneCallVideoSender phonecallVideoSender;

        /// <summary>
        /// Will be attached to calls.
        /// </summary>
        private PhoneCallVideoReceiver phonecallVideoReceiver;

        /// <summary>
        /// Connects media handlers to each other
        /// </summary>
        private MediaConnector mediaConnector;

        private AudioMixerMediaHandler incomingDataMixer;
  
        /// <summary>
        /// Mixes audio from different sources. It will be connected to the PhoneCallListener.
        /// </summary>
        private AudioMixerMediaHandler outgoingDataMixer;

        private MediaHandlerCollection activeVideoCallListener;
        private MediaHandlerCollection activeAudioCallListener;
        private PhoneCallStateWavePlayer ringtoneWavePlayer;
        private DtmfEventWavePlayer dtmfEventWavePlayer;

        /// <summary>
        /// It contains the current NAT settings.
        /// </summary>
        private NATSettingsInfo natSettings;

        /// <summary>
        /// It logs the received and sent sip messages.
        /// </summary>
        private SIPMessageLogger sipMessageLogger;

        public bool Forwarding { get; set; }
        public string ForwardCallTo { get; set; }
        #endregion

        #region Initialization, Disposing

        private bool disposed;

        /// <summary>
        /// Occurs when the the softphone engine disposing.
        /// </summary>
        public event EventHandler<GEventArgs<bool>> Disposing;

        private SoftphoneEngine()
        {
            sipMessageLogger = new SIPMessageLogger();
            sipMessageLogger.SIPMessageReceived += (sipMessageLogger_SIPMessageReceived);
            sipMessageLogger.SIPMessageSent += (sipMessageLogger_SIPMessageSent);
            Logger.Attach(sipMessageLogger);
            Logger.Open(LogLevel.Information);
            //SoftPhone = new ArbSoftPhone(10000, 200000, 20000, 500000);

            softPhone = SoftPhoneFactory.CreateSoftPhone(SoftPhoneFactory.GetLocalIP(), 5000, 7000, 5060);
            //SoftPhone.SetSIPMessageManipulator(this);
            //OzSIP.Log.SIPMessageLogger.NewSIPMessage += SIPMessageLogger_NewSIPMessage;
            
            phoneCallsBijection = new Bijection<PhoneCallInfo, IPhoneCall>();
            PhoneLinesBijection = new Bijection<PhoneLineInfo, IPhoneLine>();
            incomingCallsBijection = new Bijection<PhoneCallInfo, IPhoneCall>();

            softPhone.IncomingCall += SoftPhone_IncomingCall;
            PhoneLinesBijection.PropertyChanged += PhoneLinesBijection_PropertyChanged;
            phoneCallsBijection.PropertyChanged += PhoneCallsBijection_PropertyChanged;

            callHistory = new List<PhoneCallInfo>();

            CreateMediaHandlers();
            activeAudioCallListener.Start();

            disposed = false;
            ForwardCallTo = "";
        }

        /// <summary>
        /// Creates the incoming and outgoing media handlers such as microphone or speaker
        /// </summary>
        private void CreateMediaHandlers()
        {
            MediaHandlerFactory factory = new MediaHandlerFactory();
            activeAudioCallListener = factory.CreateSoftPhoneCallListener();
            activeVideoCallListener = factory.CreateSoftPhoneVideoCallListener();

            phoneCallAudioReceiver = activeAudioCallListener.GetComponent("AudiReceiver") as PhoneCallAudioReceiver;
            phoneCallAudioSender = activeAudioCallListener.GetComponent("AudioSender") as PhoneCallAudioSender;

            phonecallVideoSender = activeVideoCallListener.GetComponent("VideoSender") as PhoneCallVideoSender;
            phonecallVideoReceiver = activeVideoCallListener.GetComponent("VideoReceiver") as PhoneCallVideoReceiver;

            mediaConnector = activeAudioCallListener.MediaConnector;

            microphone = activeAudioCallListener.GetComponent("Microphone") as Microphone;            
            if (microphone != null)
            {                
                microphone.LevelChanged += (Microphone_LevelChanged);
            }

            speaker = activeAudioCallListener.GetComponent("Speaker") as Speaker;
            if (speaker != null)
            {                
                speaker.LevelChanged += (Speaker_LevelChanged);
            }

            incomingDataMixer = activeAudioCallListener.GetComponent("SpeakerMixer") as AudioMixerMediaHandler;
            camera = activeVideoCallListener.GetComponent("WebCamera") as WebCamera;

            remoteImageHandler = activeVideoCallListener.GetComponent("RemoteImageHandler") as ImageProvider<Image>;
            localImageHandler = activeVideoCallListener.GetComponent("LocalImageHandler") as ImageProvider<Image>;

            AudioProcessor = activeAudioCallListener.GetComponent("AudioProcessor") as AudioQualityEnhancer;
            outgoingDataMixer = activeAudioCallListener.GetComponent("OutGoingDataMixer") as AudioMixerMediaHandler;
            RecordDataMixer = activeAudioCallListener.GetComponent("RecordDataMixer") as AudioMixerMediaHandler;

            dtmfEventWavePlayer = activeAudioCallListener.GetComponent("DTMF") as DtmfEventWavePlayer;
            ringtoneWavePlayer = activeAudioCallListener.GetComponent("Ringtones") as PhoneCallStateWavePlayer;

            Stream basicRing = Assembly.GetExecutingAssembly().GetManifestResourceStream(
                "OzekiDemoSoftphone.Resources.basic_ring.wav"
                );

            ringtoneWavePlayer.UpdateIncomingStateStream(CallState.Ringing, @"..\..\Resources\basic_ring.wav");
            ringtoneWavePlayer.UpdateOutgoingStateStream(CallState.Ringing, @"..\..\Resources\ringback.wav");
            
        }

     

        /// <summary>
        /// Disposes the softphone engine.
        /// </summary>
        public void Dispose()
        {
            Disposing(this, new GEventArgs<bool>(true));

            //Dispose once, dispose for ever.
            disposed = true;

            mediaConnector.Dispose();
            if(microphone != null) microphone.Dispose();
            if(speaker != null) speaker.Dispose();
            if(camera != null) camera.Dispose();
            AudioProcessor.Dispose();
            RecordDataMixer.Dispose();
            outgoingDataMixer.Dispose();
            if (WaveStreamPlayback != null)
                WaveStreamPlayback.Dispose();
            if (WaveStreamRecorder != null)
                WaveStreamRecorder.Dispose();
            phoneCallAudioReceiver.Dispose();
            phoneCallAudioSender.Dispose();
            phonecallVideoSender.Dispose();
            phonecallVideoSender.Dispose();
            remoteImageHandler.Dispose();

            lock (phoneCallsBijection)
            {
                PhoneLinesBijection.PropertyChanged -= PhoneLinesBijection_PropertyChanged;
                phoneCallsBijection.PropertyChanged -= PhoneCallsBijection_PropertyChanged;

                /* If there are active calls, sends ask them hang up.
                 * Hang up indicates disposition as well.
                 * */
                foreach (var kvp in phoneCallsBijection)
                    kvp.Value.HangUp();

                /* If there are registered phone lines, the
                 * The SoftPhoneEngine just disposes the objects.                
                 * */
                foreach (var kvp in PhoneLinesBijection)
                    kvp.Value.Dispose();


                phoneCallsBijection.Clear();
                PhoneLinesBijection.Clear();
            }

            lock (softPhone)
            {
                softPhone.Close();
                softPhone.IncomingCall -= SoftPhone_IncomingCall;
            }
        }

        #endregion

        #region Logging

        /// <summary>
        /// SIP message received by the SIP Stack.
        /// </summary>
        public event EventHandler<GEventArgs<string>> SIPMessageReceived;

        /// <summary>
        /// SIP message sent by the SIP Stack.
        /// </summary>
        public event EventHandler<GEventArgs<string>> SIPMessageSent;

        void sipMessageLogger_SIPMessageSent(object sender, VoIPEventArgs<string> e)
        {
            if (SIPMessageSent != null)
                SIPMessageSent(this, new GEventArgs<string>(string.Format(">> {0}", e.Item)));
        }

        void sipMessageLogger_SIPMessageReceived(object sender, VoIPEventArgs<string> e)
        {
            if (SIPMessageReceived != null)
                SIPMessageReceived(this, new GEventArgs<string>(string.Format("<< {0}", e.Item)));
        }

        #endregion

        #region Registration

        /// <summary>
        /// Creates a phone line object and tries to register or activate it.
        /// </summary>
        /// <param name="phoneLineInfo">The information required for registration, or for activation.</param>
        public void RegisterPhoneLine(PhoneLineInfo phoneLineInfo)
        {
            if (disposed)
                return;

            if (PhoneLinesBijection.ContainsKey(phoneLineInfo))
                return;

            SIPAccount acc = phoneLineInfo.AsSIPAccount();
            IPhoneLine line;
            if (natSettings==null)
                 line = softPhone.CreatePhoneLine(acc, phoneLineInfo.TransportType, phoneLineInfo.SrtpMode);
            else
                 line = softPhone.CreatePhoneLine(acc, new NatConfiguration(natSettings.TraversalMethodType, natSettings.ServerAddress), phoneLineInfo.TransportType, phoneLineInfo.SrtpMode);
            line.PhoneLineStateChanged += PhoneLineInformation;
            softPhone.RegisterPhoneLine(line);
        }

        /// <summary>
        /// Unregisters the associated phone line.
        /// </summary>
        /// <param name="phoneLineInfo">Phone information.</param>
        /// <remarks>
        /// Finds a phone line object associated with the information provided, 
        /// and begin to unregister the associated phone line.
        /// </remarks>
        public void UnregisterPhoneLine(PhoneLineInfo phoneLineInfo)
        {
            if (disposed)
                return;

            if (!PhoneLinesBijection.ContainsKey(phoneLineInfo))
                return;
            
            IPhoneLine line = PhoneLinesBijection.Get(phoneLineInfo);
         
            if (line.LineState == PhoneLineState.NoRegNeeded)
            {
                /*  If the phone line has direct connection, where no registration 
                 * needed, simply just remove the given phone line from the bijection.
                 * */
                PhoneLinesBijection.RemoveBijection(phoneLineInfo);
                return;
            }
            /* Otherwise try to unregister the phone line.
             * */
            softPhone.UnregisterPhoneLine(line);
        }

        #endregion

        #region PhoneLine

        /// <summary>
        /// Returns information about active phone lines.
        /// </summary>
        /// <remarks>
        /// The phone line can be registered at some SIPProxy
        /// or can be connected directly to some SIP device.
        /// </remarks>
        public IEnumerable<PhoneLineInfo> PhoneLines
        {
            get
            {
                try
                {
                    var list = new List<PhoneLineInfo>();
                    foreach (var kvp in PhoneLinesBijection)
                        list.Add(kvp.Key);
                    return list;
                }
                catch (InvalidOperationException)
                {
                    return PhoneLines;
                }
            } //TODO
        }

        /// <summary>
        /// It is an association between phoneline objects and objects that 
        /// represent the information of the line.
        /// </summary>
        /// <remarks>
        /// This is necessary, because exists the separation the method 
        /// layer and the view layer. Information flows in this way between 
        /// the two layers.
        /// </remarks>
        private Bijection<PhoneLineInfo, IPhoneLine> PhoneLinesBijection;

        /// <summary>
        /// Phone lines have been changed somehow in the system, need to re-display in the view.
        /// </summary>
        /// <param name="sender">The SoftPhone.</param>
        /// <param name="e">The description of change.</param>
        void PhoneLinesBijection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (disposed)
                return;

            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs("PhoneLinesBijection"));
        }

        /// <summary>
        /// Phone Line event handler.
        /// </summary>
        /// <param name="sender">The Phone Line object.</param>
        /// <param name="e">The information provided by phone line about its state.</param>
        public void PhoneLineInformation(object sender, VoIPEventArgs<PhoneLineState> e)
        {
            if (disposed)
                return;

            IPhoneLine phoneLine = sender as IPhoneLine;
            if(phoneLine == null)
                return;

            if (e.Item == PhoneLineState.NoRegNeeded)
            {
                PhoneLineInfo pli = phoneLine.AsPhoneLineInfo();
                /* If a new phone line is associated in PhoneLineBijection
                 * that triggers the PropertyChanged event and
                 * PhoneLinesBijection_PropertyChanged will be activated.
                 * */
                PhoneLinesBijection.AddBijection(pli, phoneLine);
                return;
            }

            if (e.Item == PhoneLineState.RegistrationSucceeded)
            {
                PhoneLineInfo pli = phoneLine.AsPhoneLineInfo();
                //Only in case of reregistration the bijection already 
                //contains the the phone line.
                if (PhoneLinesBijection.ContainsKey(pli))
                    return;

                /* If a new phone line is associated in PhoneLineBijection
                 * that triggers the PropertyChanged event and
                 * PhoneLinesBijection_PropertyChanged will be activated.
                 * */
                PhoneLinesBijection.AddBijection(pli, phoneLine);
                return;
            }

            if (e.Item == PhoneLineState.RegistrationFailed)
            {
                //ReRegistration failed.
                PhoneLineInfo pli = phoneLine.AsPhoneLineInfo();
                if (!PhoneLinesBijection.ContainsKey(pli))
                    return;

                /* If PhoneLineBijection is changing
                 * that triggers the PropertyChanged event and
                 * PhoneLinesBijection_PropertyChanged will be activated.
                 * */
                PhoneLinesBijection.RemoveBijection(pli);
                return;
            }

            if (e.Item == PhoneLineState.UnregSucceeded)
            {
                PhoneLineInfo pli = phoneLine.AsPhoneLineInfo();
                if (!PhoneLinesBijection.ContainsKey(pli))
                    return;

                IPhoneLine line = PhoneLinesBijection.Get(pli);
                if (line == null)
                    return;

                //line.DetachListener(this);
                line.PhoneLineStateChanged -= PhoneLineInformation;
                line.Dispose();

                /* If PhoneLineBijection is changing
                 * that triggers the PropertyChanged event and
                 * PhoneLinesBijection_PropertyChanged will be activated.
                 * */
                PhoneLinesBijection.RemoveBijection(pli);
                return;
            }
        }

        #endregion

        #region Call

        /// <summary>
        /// It is an association between phonecall objects and objects that 
        /// represent the information of the call.
        /// </summary>
        /// <remarks>
        /// This is necessary, because exists the separation the method 
        /// layer and the view layer. Information flows in this way between 
        /// the two layers.
        /// </remarks>
        private Bijection<PhoneCallInfo, IPhoneCall> phoneCallsBijection;

        /// <summary>
        /// It is an association between phonecall objects and objects that 
        /// represent the information of the call.
        /// </summary>
        /// <remarks>
        /// This is necessary, because exists the separation the method 
        /// layer and the view layer. Information flows in this way between 
        /// the two layers.
        /// </remarks>
        private Bijection<PhoneCallInfo, IPhoneCall> incomingCallsBijection;

        /// <summary>
        /// Returns information about active calls.
        /// </summary>
        public IEnumerable<PhoneCallInfo> PhoneCalls
        {
            get //TODO
            {
                try
                {
                    var list = new List<PhoneCallInfo>();
                    foreach (var kvp in phoneCallsBijection)
                        list.Add(kvp.Key);
                    return list;
                }
                catch (InvalidOperationException)
                {
                    return PhoneCalls;
                }
            }
        }

        /// <summary>
        /// An incoming call is received to the system.
        /// </summary>
        public event EventHandler<GEventArgs<PhoneCallInfo>> IncomingCall;

        /// <summary>
        /// Incoming call has been cancelled.
        /// </summary>
        public event EventHandler<GEventArgs<PhoneCallInfo>> IncomingCallCancelled;

        /// <summary>
        /// Occurs when the phone call state has changed.
        /// </summary>
        public event EventHandler<GEventArgs<Ozeki.Common.OzTuple<PhoneCallInfo, CallState>>> PhoneCallStateHasChanged;

        /// <summary>
        /// Phone calls have been changed somehow in the system, need to re-display in the view.
        /// </summary>
        /// <param name="sender">The SoftPhone.</param>
        /// <param name="e">The description of change.</param>
        void PhoneCallsBijection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (disposed)
                return;

            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs("PhoneCallsBijection"));
        }

        /// <summary>
        /// Get the state of call.
        /// </summary>
        /// <param name="callInfo">The call object.</param>
        /// <returns>Null if the call couldn't be found, otherwise its state.</returns>
        public CallState? GetCallState(PhoneCallInfo callInfo)
        {
            IPhoneCall call = phoneCallsBijection.Get(callInfo);
            if (call == null)
                return null;

            return call.CallState;
        }

        /// <summary>
        /// Starts a call on the phone line described by phone line info.
        /// </summary>
        /// <param name="phoneLineInfo">The information about the phone line.</param>
        /// <param name="dial">The dialed number.</param>
        public void StartCall(PhoneLineInfo phoneLineInfo, string dial)
        {
            if (disposed)
                return;

            PhoneCallInfo callInfo = new PhoneCallInfo(phoneLineInfo, dial, CallDirection.Outgoing);
            if (phoneCallsBijection.ContainsKey(callInfo))
                return;

            if (!PhoneLinesBijection.ContainsKey(phoneLineInfo))
                return;

            IPhoneLine line = PhoneLinesBijection.Get(phoneLineInfo);
            if (line == null)
                return;

            /* When a call is created and started, the state of call is going to
             * change over time. The SoftphoneEngine needs to subscribe 
             * itself to this component before start the call.
             * */
            IPhoneCall call = softPhone.CreateCallObject(line, dial);
            call.CallStateChanged += call_CallStateChanged;
            call.CallErrorOccured += call_CallErrorOccured;
            call.Start();
        }

        /// <summary>
        /// Incoming call events that occur in SoftPhone.
        /// </summary>
        /// <param name="sender">The SoftPhone.</param>
        /// <param name="e">The incomming call.</param>
        void SoftPhone_IncomingCall(object sender, VoIPEventArgs<IPhoneCall> e)
        {
            if (disposed)
                return;

            if (Forwarding)
            {
                e.Item.ForwardCall(ForwardCallTo);
                return;
            }
            //checks Do Not Disturb is active.
            if (e.Item.CallState==CallState.Error )
                return;


            /* Only the description is necessary for the view of the modell.
             * The data are extracted from the call object directly.
             * */
            PhoneCallInfo pci = e.Item.AsPhoneCallInfo();
            e.Item.CallErrorOccured += call_CallErrorOccured;
            e.Item.CallStateChanged += call_CallStateChanged;
            e.Item.TransferStateChanged += call_TransferStateChanged;

            bool startDiscreteRingtone = incomingCallsBijection.Count == 0 && ActiveCall != null;
            if (startDiscreteRingtone)
                ringtoneWavePlayer.StartStreaming();

            incomingCallsBijection.Add(pci, e.Item);
            if (ActiveCall == null)
                SetActiveCall(e.Item);

            if (IncomingCall == null)
                return;

            IncomingCall(this, new GEventArgs<PhoneCallInfo>(pci));
        }

        void call_TransferStateChanged(object sender, VoIPEventArgs<Ozeki.VoIP.TransferState> e)
        {

        }

        /// <summary>
        /// Call error handler
        /// </summary>
        /// <param name="sender">The call object.</param>
        /// <param name="e">The error.</param>
        void call_CallErrorOccured(object sender, VoIPEventArgs<CallError> e)
        {
            IPhoneCall call = sender as IPhoneCall;
            if (call == null)
                return;

            if(phoneCallsBijection.ContainsInv(call))
                phoneCallsBijection.RemoveInv(call);

            /* When a call error just happened, unsubsrcibe
             * the SoftPhoneEngine and close the call object.
             * */
            call.CallErrorOccured -= call_CallErrorOccured;
            call.CallStateChanged -= call_CallStateChanged;

            /* If the activelistener is attached to phone call
             * it need to be removed also.
             * */
            
            //if (call.Equals(ActiveAudioCallListener.ActiveCall))
            //    ActiveAudioCallListener.Detach();
        }

        /// <summary>
        /// Call state handler.
        /// </summary>
        /// <param name="sender">The call object.</param>
        /// <param name="e">The new state.</param>
        /// <remarks>
        /// Everything in the engine is event driven. If a call alters its state
        /// the engine should have react. 
        /// </remarks>
        void call_CallStateChanged(object sender, VoIPEventArgs<CallState> e)
        {
            IPhoneCall call = sender as IPhoneCall;
            if (call == null)
                return;

            PhoneCallInfo pci = call.AsPhoneCallInfo();

            if (PhoneCallStateHasChanged != null)
                PhoneCallStateHasChanged(this, new GEventArgs<OzTuple<PhoneCallInfo, CallState>>(
                    new OzTuple<PhoneCallInfo, CallState>(
                        pci,
                        call.CallState)
                    )
                );

            /* Ringing, or incall the engine put the call into the bijection. */
            if (e.Item.IsRinging() || e.Item.IsInSetupCall() || e.Item == CallState.Answered)
            {
                if (!phoneCallsBijection.ContainsKey(pci))
                    /* If PhoneLineBijection is changing
                     * that triggers the PropertyChanged event and
                     * PhoneLinesBijection_PropertyChanged will be activated.
                     * */
                    phoneCallsBijection.AddBijection(pci, call);

                return;
            }

            /* If the call is finished the engine removes the call from the bijection. */
            if (e.Item.IsCallEnded())
             {
                /* If PhoneLineBijection is changing
                 * that triggers the PropertyChanged event and
                 * PhoneLinesBijection_PropertyChanged will be activated.
                 * */
                phoneCallsBijection.RemoveInv(call);
                call.CallErrorOccured -= call_CallErrorOccured;
                call.CallStateChanged -= call_CallStateChanged;

                if (ActiveCall == call)
                    ActiveCall = null;

                if (call.IsIncoming)
                {
                    incomingCallsBijection.RemoveBijection(pci);
                    if (IncomingCallCancelled != null)
                        IncomingCallCancelled(this, new GEventArgs<PhoneCallInfo>(pci));
                }

                SpeakerLevelChanged(this, new VoIPEventArgs<float>(0));
                MicrophoneLevelChanged(this, new VoIPEventArgs<float>(0));

                if (!IsIncommingRinging())
                    ringtoneWavePlayer.Detach();

                return;
            }
        }

        private bool IsIncommingRinging()
        {
            foreach (IPhoneCall call in incomingCallsBijection.Values)
                if (call.CallState == CallState.Ringing)
                    return true;

            return false;
        }

        /// <summary>
        /// Accept the incoming call described by the information.
        /// </summary>
        /// <param name="callInfo">The associated call info.</param> 
        public void AcceptCall(PhoneCallInfo callInfo)
        {
            if (disposed)
                return;

            if (callInfo.Direction == CallDirection.Outgoing)
                return;

            IPhoneCall call = null;
            if (incomingCallsBijection.ContainsKey(callInfo))
                call = incomingCallsBijection.Get(callInfo);

            if (call == null)
                return;

            /* Once the SoftPhoneEngine accepts a phone call, that call is not an incoming call anymore, if its state 
             * changes, the call_CallStateChanged event handler will activate.
             * */
            incomingCallsBijection.RemoveBijection(callInfo);
            call.Accept();
        }

        /// <summary>
        /// Transfer call described by the information.
        /// </summary>
        /// <param name="callInfo">The associated call info.</param>
        /// <param name="target"></param>
        public void BlindTransfer(PhoneCallInfo callInfo, string target)
        {
            if (!phoneCallsBijection.ContainsKey(callInfo))
                return;

            var call = phoneCallsBijection.Get(callInfo);

            call.BlindTransfer(target);
        }

        public void AttendedTransfer(PhoneCallInfo callInfo, PhoneCallInfo targetInfo)
        {
           if(!phoneCallsBijection.ContainsKey(callInfo))
               return;

            if(!phoneCallsBijection.ContainsKey(targetInfo))
                return;

            var call = phoneCallsBijection.Get(callInfo);

            var target = phoneCallsBijection.Get(targetInfo);

            call.AttendedTransfer(target);

        }

        /// <summary>
        /// Reject the incoming call described by the information.
        /// </summary>
        /// <param name="callInfo">The associated call info.</param>
        public void RejectCall(PhoneCallInfo callInfo)
        {
            if (disposed)
                return;

            if (callInfo.Direction == CallDirection.Outgoing)
                return;

            IPhoneCall call = null;
            if (incomingCallsBijection.ContainsKey(callInfo))
                call = incomingCallsBijection.Get(callInfo);
            
            if (call == null)
                return;

            /* Once the SoftPhoneEngine rejects a phone call, that call is not an incoming call anymore, if its state 
             * changes, the call_CallStateChanged event handler will activate.
             * */
            incomingCallsBijection.RemoveBijection(callInfo);
            call.Reject();
        }

        /// <summary>
        /// Hang up the call associated to given information.
        /// </summary>
        /// <param name="callInfo">Provided information about phone call.</param>
        public void HangUpCall(PhoneCallInfo callInfo)
        {
            if (disposed)
                return;

            IPhoneCall call = null;
            if (!phoneCallsBijection.ContainsKey(callInfo))
                return;

            call = phoneCallsBijection.Get(callInfo);
            if (call == null)
                return;

            /* Ask the call to hang up, there are many thing here to do,
             * once when it is successful the engine will get an event of the call
             * and the call_CallStateChanged event handler will activate.
             * */
            call.HangUp();
        }

        private IPhoneCall ActiveCall;

        /// <summary>
        /// Set the phone call to be active.
        /// </summary>
        /// <param name="callInfo">The associated call info.</param>
        /// <remarks>
        /// The active call has the focus of sound devices.
        /// </remarks>
        public void SetActiveCall(PhoneCallInfo callInfo)
        {
            if (disposed)
                return;

            if (!phoneCallsBijection.ContainsKey(callInfo))
                return;

            IPhoneCall call = phoneCallsBijection.Get(callInfo);

            if (call == null)
                return;

            SetActiveCall(call);
        }

        private void SetActiveCall(IPhoneCall call)
        {
            ActiveCall = call;
            /*
            if (PhoneCallAudioSender != null)
                PhoneCallAudioSender.AttachToCall(call);

            if (PhoneCallAudioReceiver != null)
                PhoneCallAudioReceiver.AttachToCall(call);
             * */

            activeAudioCallListener.AttachToCall(call);
            activeVideoCallListener.AttachToCall(call);
        }

        #endregion

        #region Hold, DND, Keep-alive

        /// <summary>
        /// Hold the described call.
        /// </summary>
        /// <param name="callInfo">Provided information about phone call.</param>
        public void HoldCall(PhoneCallInfo callInfo)
        {
            IPhoneCall call = phoneCallsBijection.Get(callInfo);
            if (call == null)
                return;

            call.ToggleHold();
        }

        /// <summary>
        /// Set DoNotDisturb mode to phone line.
        /// </summary>
        /// <param name="lineInfo">Phone line.</param>
        /// <param name="dnd">DND.</param>
        public void SetDND(PhoneLineInfo lineInfo, bool dnd)
        {
            IPhoneLine line = PhoneLinesBijection.Get(lineInfo);
            if (line == null)
                return;
            
            line.DoNotDisturb = dnd;
        }

        /// <summary>
        /// Returns the actual DoNotDisturb state of the given phone lines.
        /// </summary>
        /// <param name="lineInfo">The phone line.</param>
        /// <returns>Returns null if can't get info about DND.</returns>
        public bool? GetDNDInfo(PhoneLineInfo lineInfo)
        {
            IPhoneLine line = PhoneLinesBijection.Get(lineInfo);
            if (line == null)
                return null;

            return line.DoNotDisturb;
        }

        #endregion

        #region NAT Settings

        /// <summary>
        /// Changes the NAT Settings (traversal method, remote server address, username and password for the server)
        /// </summary>
        /// <param name="info"></param>
        public void ChangeNatSettings(NATSettingsInfo info)
        {
            natSettings = info;
            //   SoftPhone.ChangeNATSettings(info.TraversalMethodType, info.ServerAddress, info.UserName, info.Password);
        }

        #endregion

        #region DTMF

        /// <summary>
        /// Begins DTMF signal transmitting
        /// </summary>
        /// <param name="callInfo">The phone call.</param>
        /// <param name="dtmf">The DTMF signal</param>
        public void StartDTMFSignal(PhoneCallInfo callInfo, int dtmf)
        {
            IPhoneCall call = phoneCallsBijection.Get(callInfo);
            if (call == null)
                return;

            call.StartDTMFSignal((DtmfNamedEvents)dtmf);
        }

        /// <summary>
        /// Ends DTMF signal transmitting
        /// </summary>
        /// <param name="callInfo">The phone call.</param>
        /// <param name="dtmf">The DTMF signal</param>
        public void StopDTMFSignal(PhoneCallInfo callInfo, int dtmf)
        {
            IPhoneCall call = phoneCallsBijection.Get(callInfo);
            if (call == null)
                return;

            call.StopDTMFSignal((DtmfNamedEvents)dtmf);
        }

        #endregion

        #region Microphone, Speaker, Camera


        private Microphone microphone;

        private Speaker speaker;

        private WebCamera camera;

        private ImageProvider<Image> remoteImageHandler;

        private ImageProvider<Image> localImageHandler;

        /// <summary>
        /// Occurs when the audio level of the speaker has been changed
        /// </summary>
        public event EventHandler<VoIPEventArgs<float>> SpeakerLevelChanged;

        /// <summary>
        /// Occurs when the audio level of the microphone has been changed
        /// </summary>
        public event EventHandler<VoIPEventArgs<float>> MicrophoneLevelChanged;

        /// <summary>
        ///  Gets the playback settings.
        /// </summary>
        /// <returns></returns>
        public AudioSettingsInfo GetPlaybackSettings()
        {
            AudioSettingsInfo info = GetAudioDeviceSettings(DeviceType.Playback);
            return info;
        }

        /// <summary>
        /// Gets the recording settings.
        /// </summary>
        /// <returns></returns>
        public AudioSettingsInfo GetRecordingSettings()
        {
            AudioSettingsInfo info = GetAudioDeviceSettings(DeviceType.Recording);
            return info;
        }

        /// <summary>
        /// Gets the camera settings.
        /// </summary>
        /// <returns></returns>
        public VideoSettingsInfo GetCameraSettings()
        {
            List<VideoDeviceInfo> devices = new List<VideoDeviceInfo>();
            int selectedDeviceId = -1;
            Resolution selectedresolution = new Resolution(320, 240);
            devices = WebCamera.GetDevices();
            if (devices.Count > 0)
            {
                selectedDeviceId = devices[0].DeviceID;
                if (devices[0].Capabilities != null && devices[0].Capabilities.Count >0)
                    selectedresolution = devices[0].Capabilities[0].Resolution ;
                else
                {
                    selectedresolution = new Resolution(320, 240);
                }
            }
            
            return new VideoSettingsInfo(selectedresolution, devices, selectedDeviceId);
        }
        
        private AudioSettingsInfo GetAudioDeviceSettings(DeviceType type)
        {
            List<DeviceInfo> devices = new List<DeviceInfo>();
            string selectedDeviceId = "";

            if (type == DeviceType.Playback)
                devices = Speaker.GetDevices();
            else if (type == DeviceType.Recording)
                devices = Microphone.GetDevices();

            if (devices.Count > 0)
                selectedDeviceId = devices[0].DeviceID;

            float volume = 0.8f;
            bool mute = false;

            return new AudioSettingsInfo(volume, mute, devices, selectedDeviceId);
        }

        void Microphone_LevelChanged(object sender, VoIPEventArgs<float> e)
        {
            if (MicrophoneLevelChanged != null)
                MicrophoneLevelChanged(this, e);
        }

        void Speaker_LevelChanged(object sender, VoIPEventArgs<float> e)
        {
            if (SpeakerLevelChanged != null)
                SpeakerLevelChanged(this, e);
        }

        /// <summary>
        /// Occurs when the playback settings has been changed
        /// </summary>
        /// <param name="info">Contains information about the settings</param>
        public void SpeakerSettingsHasChanged(AudioSettingsInfo info)
        {
            if (speaker == null) return;

            speaker.Volume = info.Volume;
            speaker.Muted = info.Mute;
            if (speaker.DeviceInfo.DeviceID != info.SelectedDevice)
            {
                var list = Speaker.GetDevices();
                foreach (var speakerDeviceInfo in list)
                {
                    if (speakerDeviceInfo.DeviceID == info.SelectedDevice)
                    {
                        speaker.Stop();
                        speaker.LevelChanged -= (Speaker_LevelChanged);
                        mediaConnector.Disconnect(incomingDataMixer, speaker);
                        speaker = Speaker.GetDevice(speakerDeviceInfo);
                        mediaConnector.Connect(incomingDataMixer,speaker);
                        speaker.LevelChanged += (Speaker_LevelChanged);
                        speaker.Start();
                    }
                }
            }
        }

        /// <summary>
        /// Occurs when the recording settings has been changed.
        /// </summary>
        /// <param name="info">Contains information about the settings.</param>
        public void MicrophoneSettingsHasChanged(AudioSettingsInfo info)
        {
            if (microphone == null) return;

            microphone.Volume = info.Volume;
            microphone.Muted = info.Mute;

            if (microphone.DeviceInfo.DeviceID != info.SelectedDevice)
            {
                  var list = Microphone.GetDevices();
                  foreach (var audioDeviceInfo in list)
                  {
                      if (audioDeviceInfo.DeviceID == info.SelectedDevice)
                      {
                          microphone.Stop();
                          microphone.LevelChanged -= (Microphone_LevelChanged);
                          mediaConnector.Disconnect(microphone, AudioProcessor);
                          microphone = Microphone.GetDevice(audioDeviceInfo);
                          mediaConnector.Connect(microphone,AudioProcessor);
                          microphone.LevelChanged += (Microphone_LevelChanged);
                          microphone.Start();
                      }
                  }
            }
        }


        /// <summary>
        /// Occurs when the Camera settings has been changed.
        /// </summary>
        /// <param name="info">Contains information about the settings.</param>
        public void CameraSettingsHasChanged(VideoSettingsInfo info)
        {
            if (camera == null) return;

            camera.Resolution = info.SelectedResolution;
            if (camera.DeviceID != info.SelectedDevice)
            {
                var list = WebCamera.GetDevices();
                foreach (var videoDeviceInfo in list)
                {
                    if (videoDeviceInfo.DeviceID==info.SelectedDevice)
                    {
                        camera.Stop();
                        mediaConnector.Disconnect(camera, phonecallVideoSender);
                        mediaConnector.Disconnect(camera, localImageHandler);
                        camera = WebCamera.GetDevice(videoDeviceInfo);
                        camera.Start();
                        mediaConnector.Connect(camera, phonecallVideoSender);
                        mediaConnector.Connect(camera, localImageHandler);
                    }
                }
            }
        }


        
        #endregion

        #region Voice Enhancement

        /// <summary>
        /// Class for Digital Signal Processing
        /// </summary>
        private AudioQualityEnhancer AudioProcessor;

        /// <summary>
        /// Checks whether the Speex PreProcessor has been loaded.
        /// </summary>
        /// <returns>Speex PreProcessor has been loaded or not.</returns>
        public bool CheckSpeexDSP()
        {
            //AbstractDSP preprocessor = AudioProcessor.DSPCollection.GetDSP(DSPTypes.SpeexPreprocessor);
            //AbstractDSP aec = AudioProcessor.DSPCollection.GetDSP(DSPTypes.AcousticEchoCanceller);

            //if (preprocessor == null || aec == null)
            //    return false;

            return true;
        }

        /// <summary>
        /// Enables or disables the Echo Cancellation
        /// </summary>
        /// <param name="value"></param>
        public void EnableEchoCancellation(bool value)
        {
            if (AudioProcessor != null)
            {
                AudioProcessor.AcousticEchoCancellation = value;
            }
        }

        /// <summary>
        /// Enables or disables one of the Speex Preprocessor's components.
        /// 0=AGC, 1=VAD, 2=Denoise
        /// </summary>
        public void EnablePreProcessorComponent(int componentId, bool value)
        {
            if (AudioProcessor != null)
            {
                switch (componentId)
                {
                    case 0:
                        AudioProcessor.AutoGainControl = value;
                        break;
                    case 2:
                        if (value)
                            AudioProcessor.NoiseReductionLevel = NoiseReductionLevel.Medium;
                        else
                            AudioProcessor.NoiseReductionLevel = NoiseReductionLevel.NoReduction;
                        break;
                }
            }
        }

        /// <summary>
        /// Changes the Acoustic Echo Cancellation delay between the speaker and the microphone.
        /// </summary>
        public void ChangeAECDelay(int value)
        {
            if (AudioProcessor != null)
            {
                AudioProcessor.EchoDelay = value;
            }
        }

        /// <summary>
        /// Changes the Automatic Voice Gain maximal gain in dB
        /// </summary>
        public void ChangeAGCMaxGain(int value)
        {
            if (AudioProcessor != null)
            {
                AudioProcessor.MaxGain = value;
            }
        }

        #endregion

        #region Codecs
        /// <summary>
        /// Enumerates all CodecInfo handled by the model.
        /// </summary>
        /// <returns>The enumeration of codecs.</returns>
        public IEnumerable<CodecInfo> GetCodecs()
        {
            return softPhone.Codecs;
        }

        /// <summary>
        /// Enables a codec.
        /// </summary>
        /// <param name="payload">The identifier payload number of the codec.</param>
        public void EnableCodec(int payload)
        {
            softPhone.EnableCodec(payload);
        }

        /// <summary>
        /// Disables a codec.
        /// </summary>
        /// <param name="payload">The identifier payload number of the codec.</param>
        public void DisableCodec(int payload)
        {
            softPhone.DisableCodec(payload);
        }

        #endregion

        #region Playback

        private WaveStreamPlayback WaveStreamPlayback;

        public event EventHandler<EventArgs> PlaybackStopped;

        /// <summary>
        /// Sets an audio file for playing
        /// </summary>
        /// <param name="path">The path of the audio file</param>
        public void SetPlayAudioFile(string path)
        {
            if (WaveStreamPlayback != null)
            {
                // remove the connection from AudioStreamPlayback to OutgoingDataMixer
                mediaConnector.Disconnect(WaveStreamPlayback, outgoingDataMixer);
                WaveStreamPlayback.Stopped -= (WaveStreamPlayback_Stopped);
                WaveStreamPlayback.Dispose();
            }

            // create new AudioStreamPlayback and connect to the OutgoingDataMixer
            CreateWaveStreamPlayback(path, false, false);
        }


        private void CreateWaveStreamPlayback(string path, bool repeat, bool cache)
        {
            WaveStreamPlayback = new WaveStreamPlayback(path, false, false);
            WaveStreamPlayback.Stopped += (WaveStreamPlayback_Stopped);

            mediaConnector.Connect(WaveStreamPlayback, outgoingDataMixer);
        }

        void WaveStreamPlayback_Stopped(object sender, EventArgs e)
        {
            if (PlaybackStopped != null)
                PlaybackStopped(this, EventArgs.Empty);
        }

        /// <summary>
        /// Plays the previously setted audio file
        /// </summary>
        public void StartPlay()
        {
            if (WaveStreamPlayback != null)
            {
                if (!WaveStreamPlayback.IsStreaming)
                    WaveStreamPlayback.StartStreaming();
                else
                    WaveStreamPlayback.PauseStreaming();
            }
        }

        /// <summary>
        /// Stop playing selected file request by the user.
        /// </summary>
        public void StopPlay()
        {
            if (WaveStreamPlayback != null)
                WaveStreamPlayback.StopStreaming();
        }

        #endregion

        #region Record

        private WaveStreamRecorder WaveStreamRecorder;

        /// <summary>
        /// Mixes audio data from different sources. It will be connected to the AudioStreamRecorder.
        /// </summary>
        private AudioMixerMediaHandler RecordDataMixer;

        /// <summary>
        /// Sets an audio file for recording
        /// </summary>
        /// <param name="path">The path of the audio file</param>
        public void SetRecAudioFile(string path)
        {
            if (WaveStreamRecorder != null)
            {
                // remove the connection from RecordDataMixer to AudioStreamRecorder
                mediaConnector.Disconnect(RecordDataMixer, WaveStreamRecorder);
                WaveStreamRecorder.Dispose();
            }

            // create new recorder and connect the RecordDataMixer to it
            CreateWaveStreamRecorder(path);
        }

        private void CreateWaveStreamRecorder(string path)
        {
            WaveStreamRecorder = new WaveStreamRecorder(path);

            mediaConnector.Connect(RecordDataMixer, WaveStreamRecorder);
        }

        /// <summary>
        /// Begins capturing data into the given file. If the file is already capturing, the method has no effect. 
        /// </summary>
        public AudioRecordState StartRecord()
        {
            if (WaveStreamRecorder != null)
            {
                if (!WaveStreamRecorder.IsStreaming)
                {
                    WaveStreamRecorder.StartStreaming();
                    return AudioRecordState.Started;
                }
                else
                {
                    WaveStreamRecorder.PauseStreaming();
                    return AudioRecordState.Paused;
                }
            }
            return AudioRecordState.Stopped;
        }

        /// <summary>
        /// Stops the audio capturing
        /// </summary>
        public AudioRecordState StopRecord()
        {
            if (WaveStreamRecorder != null)
            {
                WaveStreamRecorder.StopStreaming();
                mediaConnector.Disconnect(RecordDataMixer, WaveStreamRecorder);
                WaveStreamRecorder.Dispose();
                WaveStreamRecorder = null;
            }
            return AudioRecordState.Stopped;
        }

        #endregion

        #region Call History

        /// <summary>
        /// Contains information about the previous calls
        /// </summary>
        private List<PhoneCallInfo> callHistory;

        /// <summary>
        /// Adds some information about the call to the call history
        /// </summary>
        /// <param name="info">The specified information</param>
        public void AddCallToHistory(PhoneCallInfo info)
        {
            callHistory.Add(info);
        }

        /// <summary>
        /// Gets some information about the last call
        /// </summary>
        /// <param name="type">The type of the call (incoming/outgoing)</param>
        /// <returns></returns>
        public PhoneCallInfo GetLastCall(CallDirection type)
        {
            return callHistory.FindLast(delegate(PhoneCallInfo info)
            {
                return info.Direction == type;
            });
        }

        #endregion

        #region Message Summary
        public VoIPMessageSummary GetMessageSummary(PhoneLineInfo info)
        {
            IPhoneLine line = PhoneLinesBijection.Get(info);
            if (line == null)
                return null;

            return line.MessageSummary;
        }
        #endregion

        /// <summary>
        /// If something has changed we emit the event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public bool AllowedId(int id)
        {
            return false;
        }

        /// <summary>
        /// Sets the ringtone for the demo application.
        /// </summary>
        /// <param name="filePath">The filepath of the wave file.</param>
        public void SetRingtone(string filePath)
        {
            ringtoneWavePlayer.UpdateIncomingStateStream(CallState.Ringing, filePath);
        }

        /// <summary>
        /// Sets the ringback tone for the demo application.
        /// </summary>
        /// <param name="filePath">The filepath of the wave file.</param>
        public void SetRingback(string filePath)
        {
            ringtoneWavePlayer.UpdateOutgoingStateStream(CallState.Ringing, filePath);
        }


        #region Camera Actions
        /// <summary>
        /// Connect camera to a viewer.
        /// </summary>
        public void EnableVideoLocalTestMode()
        {
            if (camera == null || phonecallVideoSender == null || phonecallVideoReceiver == null)
                return;
            
            camera.Start();
       
        }

        /// <summary>
        /// Connect camera back to the phone system. 
        /// </summary>
        public void DisableVideoLocalTestMode()
        {
            if (camera == null || phonecallVideoSender == null || phonecallVideoReceiver == null)
                return;
            camera.Stop();
     
        }

        public void StartCamera(PhoneCallInfo callInfo)
        {
            if (disposed ||camera== null)
                return;
            if (callInfo == null)
                return;
            IPhoneCall call = null;
            if (!phoneCallsBijection.ContainsKey(callInfo))
                return;

            call = phoneCallsBijection.Get(callInfo);
            if (call == null)
                return;
            camera.Start();
            activeVideoCallListener.Start();
            call.ModifyCallType(CallType.AudioVideo);
        }

        public void StopCamera(PhoneCallInfo callInfo)
        {
            if (disposed || camera == null)
                return;
            camera.Stop();
           
            if (callInfo== null)
                return;
            IPhoneCall call = null;
            if (!phoneCallsBijection.ContainsKey(callInfo))
                return;

            call = phoneCallsBijection.Get(callInfo);
            if (call == null)
                return;
            activeVideoCallListener.Stop();
            
            call.ModifyCallType(CallType.Audio);
        }

        public IImageProvider<Image> GetRemoteImageProvider()
        {
            return remoteImageHandler;
        }

        public IImageProvider<Image> GetLocalImageProvider()
        {
            return localImageHandler;
        }

        /// <summary>
        /// Sets the camera current resolution.
        /// </summary>
        /// <param name="capabilities"></param>
        public void SetResolution(VideoCapabilities capabilities)
        {
            if (camera!= null)
            {
                camera.Resolution=capabilities.Resolution;
            }
            
        }
        #endregion

        #region Used IpAddress

        /// <summary>
        /// Gets the computer local addresses.
        /// </summary>
        /// <returns></returns>
        public List<IPAddress> GetLocalAddressList()
        {
            return SoftPhoneFactory.GetAddressList();
        }

        public IPAddress GetDefaultIP()
        {
            return SoftPhoneFactory.GetLocalIP();
        }

        public void ChangeNetworkAdapter(IPAddress address)
        {
          restartSoftphoneWithNewIP(address);
        }

   
        /// <summary>
        /// Reinitialize the softphone with new selected IP.
        /// </summary>
        /// <param name="ip">new IP address.</param>
        private void restartSoftphoneWithNewIP(IPAddress ip)
        {
            if (ip == null)
                return;
             lock (softPhone)
            {
                softPhone.Close();
                softPhone.IncomingCall -= SoftPhone_IncomingCall;
            }
             softPhone = SoftPhoneFactory.CreateSoftPhone(ip, 5000, 7000, 5060);
             softPhone.IncomingCall += SoftPhone_IncomingCall;
        }

        #endregion

        #region Srtp actions

        public void ChangeSRTPSettings(PhoneLineInfo lineInfo)
        {
            IPhoneLine line = PhoneLinesBijection.Get(lineInfo);
            if (line == null)
                return;

            line.SRTPMode = lineInfo.SrtpMode;
        }

        #endregion
    }
}