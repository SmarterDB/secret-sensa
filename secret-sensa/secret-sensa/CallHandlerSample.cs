using System;
using Ozeki.Media;
using Ozeki.Media.MediaHandlers;
using Ozeki.Network.Nat;
using Ozeki.VoIP;
using Ozeki.VoIP.SDK;


namespace ozeki.voip.sip.client
{
    //These classes is just an example how to use OZEKI VoIP SIP SDK, what dll is added to the References in Solution Explorer
    //Feel free to use the other components of this SDK
    //For More information please visit : http://www.voip-sip-sdk.com and http://www.ozekiphone.com/ozeki-voip-sip-client-997.html
    class CallHandlerSample
    {
        private ISoftPhone softPhone;
        private IPhoneLine phoneLine;
        private PhoneLineState phoneLineInformation;
        private IPhoneCall call;
        private readonly Microphone microphone;
        private readonly Speaker speaker;
        private readonly MediaConnector connector;
        private readonly PhoneCallAudioSender mediaSender;
        private readonly PhoneCallAudioReceiver mediaReceiver;

        /// <summary>
        /// Event triggered when the registered softphone has called
        /// </summary>
        public event EventHandler<VoIPEventArgs<IPhoneCall>> IncomingCallReceived;

        /// <summary>
        /// Event the softphone has successfully registered
        /// </summary>
        public event EventHandler RegistrationSucceded;

        /// <summary>
        /// Handler of making call and receiving call
        /// </summary>
        /// <param name="registerName">The SIP ID what will registered into your PBX</param>
        /// <param name="domainHost">The address of your PBX</param>
        public CallHandlerSample(string registerName, string domainHost)
        {
            microphone = Microphone.GetDefaultDevice();
            speaker = Speaker.GetDefaultDevice();
            connector = new MediaConnector();
            mediaSender = new PhoneCallAudioSender();
            mediaReceiver = new PhoneCallAudioReceiver();

            InitializeSoftPhone(registerName, domainHost);
        }


        /// <summary>
        ///It initializes a softphone object with a SIP PBX, and it is for requisiting a SIP account that is nedded for a SIP PBX service. It registers this SIP
        ///account to the SIP PBX through an ’IphoneLine’ object which represents the telephoneline. 
        ///If the telephone registration is successful we have a call ready softphone. In this example the softphone can be reached by dialing the registername.
        /// </summary>
        /// <param name="registerName">The SIP ID what will registered into your PBX</param>
        /// <param name="domainHost">The address of your PBX</param>
        private void InitializeSoftPhone(string registerName, string domainHost)
        {
            try
            {
                softPhone = SoftPhoneFactory.CreateSoftPhone(SoftPhoneFactory.GetLocalIP(), 5700, 5750, 5700);
                softPhone.IncomingCall += softPhone_IncomingCall;
                phoneLine = softPhone.CreatePhoneLine(new SIPAccount(true, registerName, registerName, registerName, registerName, domainHost, 5060), new NatConfiguration(NatTraversalMethod.None));
                phoneLine.PhoneLineStateChanged += phoneLine_PhoneLineInformation;

                softPhone.RegisterPhoneLine(phoneLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine("You didn't give your local IP adress, so the program won't run properly.\n {0}", ex.Message);
            }
        }

        /// <summary>
        /// Create and start the call to the dialed number
        /// </summary>
        /// <param name="dialedNumber"></param>
        public void Call(string dialedNumber)
        {
            if (phoneLineInformation != PhoneLineState.RegistrationSucceeded && phoneLineInformation != PhoneLineState.NoRegNeeded)
            {
                Console.WriteLine("Phone line state is not valid!");
                return;
            }

            if (string.IsNullOrEmpty(dialedNumber))
                return;

            if (call != null)
                return;

            call = softPhone.CreateCallObject(phoneLine, dialedNumber);
            WireUpCallEvents();
            call.Start();
        }

        /// <summary>
        /// Occurs when phone line state has changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void phoneLine_PhoneLineInformation(object sender, VoIPEventArgs<PhoneLineState> e)
        {
            phoneLineInformation = e.Item;
            Console.WriteLine("Register name:" + ((IPhoneLine)sender).SIPAccount.RegisterName);

            if (e.Item == PhoneLineState.RegistrationSucceeded)
            {
                Console.WriteLine("Registration succeeded. Online.");
                OnRegistrationSucceded();
            }
            else
            {
                Console.WriteLine("Current state:" + e.Item);
            }
        }

        /// <summary>
        /// Occurs when an incoming call request has received.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void softPhone_IncomingCall(object sender, VoIPEventArgs<IPhoneCall> e)
        {
            Console.WriteLine("Incoming call from {0}", e.Item.DialInfo);
            call = e.Item;
            WireUpCallEvents();
            OnIncomingCallReceived(e.Item);
        }

        /// <summary>
        /// Occurs when the phone call state has changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void call_CallStateChanged(object sender, VoIPEventArgs<CallState> e)
        {
            Console.WriteLine("Call state changed: " + e.Item);

            switch (e.Item)
            {
                case CallState.InCall:
                    ConnectDevicesToCall();
                    break;
                case CallState.Completed:
                    DisconnectDevicesFromCall();
                    WireDownCallEvents();
                    call = null;
                    break;
                case CallState.Cancelled:
                    WireDownCallEvents();
                    call = null;
                    break;
            }
        }

        /// <summary>
        /// There are certain situations when the call cannot be created, for example the dialed number is not available 
        /// or maybe there is no endpoint to the dialed PBX, or simply the telephone line is busy. 
        /// This event handling is for displaying these events.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void call_CallErrorOccured(object sender, VoIPEventArgs<CallError> e)
        {
            Console.WriteLine(e.Item + " Press Enter to Continue!");
            Console.ReadLine();

            WireDownCallEvents();
            DisconnectDevicesFromCall();
            call = null;
        }

        private void OnRegistrationSucceded()
        {
            var handler = RegistrationSucceded;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        private void OnIncomingCallReceived(IPhoneCall item)
        {
            var handler = IncomingCallReceived;

            if (handler != null)
                handler(this, new VoIPEventArgs<IPhoneCall>(item));
        }

        /// <summary>
        /// Connecting the microphone and speaker to the call
        /// </summary>
        private void ConnectDevicesToCall()
        {
            if (microphone != null)
                microphone.Start();
            connector.Connect(microphone, mediaSender);

            if (speaker != null)
                speaker.Start();
            connector.Connect(mediaReceiver, speaker);

            mediaSender.AttachToCall(call);
            mediaReceiver.AttachToCall(call);
        }

        /// <summary>
        /// Disconnecting the microphone and speaker from the call
        /// </summary>
        private void DisconnectDevicesFromCall()
        {
            if (microphone != null)
                microphone.Stop();
            connector.Disconnect(microphone, mediaSender);

            if (speaker != null)
                speaker.Stop();
            connector.Disconnect(mediaReceiver, speaker);

            mediaSender.Detach();
            mediaReceiver.Detach();
        }

        /// <summary>
        ///  It signs up to the necessary events of a call transact.
        /// </summary>
        private void WireUpCallEvents()
        {
            if (call != null)
            {
                call.CallStateChanged += ( call_CallStateChanged );
                call.CallErrorOccured += ( call_CallErrorOccured );
            }
        }

        /// <summary>
        /// It signs down from the necessary events of a call transact.
        /// </summary>
        private void WireDownCallEvents()
        {
            if (call != null)
            {
                call.CallStateChanged -= (call_CallStateChanged);
                call.CallErrorOccured -= (call_CallErrorOccured);
            }
        }

        ~CallHandlerSample()
        {
            if (softPhone != null)
                softPhone.Close();
            WireDownCallEvents();
        }
    }
}
