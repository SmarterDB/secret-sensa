using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Microsoft.Win32;
using Ozeki.Media.Audio;
//using Ozeki.Media.Codec;
using Ozeki.Media.Codec;
using Ozeki.Media.Video;
using Ozeki.Media.Video.Imaging;
using Ozeki.Network;
using Ozeki.Network.Nat;
using Ozeki.VoIP;
using OzekiDemoSoftphone.GUI.States;
using OzekiDemoSoftphone.PM;
using OzekiDemoSoftphone.PM.Data;
using Ozeki.VoIP.MessageSummary;
using OzekiDemoSoftphone.Softphone;
using OzekiDemoSoftphone.Utils;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;
using TransportType = Ozeki.Network.TransportType;

namespace OzekiDemoSoftphone.GUI
{
    /// <summary>
    /// The main window of the GUI application.
    /// </summary>
    public partial class MainForm : Form
    {
        #region Fields, Properties

        /// <summary>
        /// The state of GUI.
        /// </summary>
        /// <remarks>
        /// The GUI state is represented as State Machine, all changes of gui are handled there.
        /// </remarks>
        private GUIState currentState;

        /// <summary>
        /// The enumeration of the Numkeys.
        /// </summary>
        protected IEnumerable<Button> NumKeys;

        /// <summary>
        /// Array of phoneLine selector buttons.
        /// </summary>
        protected Button[] PhoneLineSelectors;

        /// <summary>
        /// Its display.
        /// </summary>
        internal TextBox ItsDisplay;

        private IPAddress usedIPaddress;

        private SoftphoneEngine softphoneEngine;

        /*/// <summary>
        /// Actual speaker settings information.
        /// </summary>
        private SpeakerSettingsInfo ActualSpeakerSettings;*/

        /*/// <summary>
        /// Actual microphone settings information.
        /// </summary>
        private MicrophoneSettingsInfo MicrophoneSettings;*/

        /// <summary>
        /// Returns the selected phone line in the phone lines list box.
        /// </summary>
        internal PhoneLineInfo SelectedLine { get { return lbPhoneLines.SelectedItem as PhoneLineInfo; } }

        /// <summary>
        /// Returns the selected phone call in the phone calls list box.
        /// </summary>
        internal PhoneCallInfo SelectedCall { get { return lbPhoneCalls.SelectedItem as PhoneCallInfo; } }

        /// <summary>
        /// Returns the number of active phone lines.
        /// </summary>
        /// <remarks>
        /// This information is needed for the GUI states.
        /// </remarks>
        internal int NumberOfActivePhoneLines { get { return lbPhoneLines.Items.Count; } }

        /// <summary>
        /// Returns the number of active phone calls.
        /// </summary>
        /// <remarks>
        /// This information is needed for the GUI states.
        /// </remarks>
        internal int NumberOfActivePhoneCalls { get { return lbPhoneCalls.Items.Count; } }

        /// <summary>
        /// Logic for changing the message
        /// </summary>
        /// <remarks>
        /// When a phone line is selected we get information about its voicemail mailbox.
        /// </remarks>
        internal MWILogic MWILogic { get; private set; }


        private ForwardCallForm forwardFrom;

        #endregion

        /// <summary>
        /// Constructs a new MainForm object.
        /// </summary>
        public MainForm(SoftphoneEngine softphoneEngine)
        {
            InitializeComponent();
            this.softphoneEngine = softphoneEngine;
            WireUpSoftphoneEngineEvents();
            string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.Text += (" " + version);
            CreateToolTips();

            cobTransport.SelectedIndex = 0;
            cobSRTPmode.SelectedIndex = 0;

            var numKeys = new List<Button>();
            numKeys.Add(btnKeypad1);
            numKeys.Add(btnKeypad2);
            numKeys.Add(btnKeypad3);
            numKeys.Add(btnKeypad4);
            numKeys.Add(btnKeypad5);
            numKeys.Add(btnKeypad6);
            numKeys.Add(btnKeypad7);
            numKeys.Add(btnKeypad8);
            numKeys.Add(btnKeypad9);
            numKeys.Add(btnKeypad0);
            numKeys.Add(btnKeypadSharp);
            numKeys.Add(btnKeypadAsterisk);
            NumKeys = numKeys;

            Button[] phoneLineSelectors = new Button[6];
            phoneLineSelectors[0] = btnPhoneLine1;
            phoneLineSelectors[1] = btnPhoneLine2;
            phoneLineSelectors[2] = btnPhoneLine3;
            phoneLineSelectors[3] = btnPhoneLine4;
            phoneLineSelectors[4] = btnPhoneLine5;
            phoneLineSelectors[5] = btnPhoneLine6;
            PhoneLineSelectors = phoneLineSelectors;

            ItsDisplay = tbDialNumber;
            currentState = new NoLineAndNoCall(this);
            MWILogic = new MWILogic(lblMessageWaitingIndication, this.BackColor);
            MWILogic.Reset();

            lbPhoneLines.SelectedValueChanged += (lbPhoneLines_SelectedValueChanged);
            lbPhoneCalls.SelectedValueChanged += (lbPhoneCalls_SelectedValueChanged);

            AcceptOnRegisterButton();

            //tbUserId.Text = tbDisplayName.Text = tbRegisterPassword.Text = tbRegisterName.Text = "884";
            //tbDomainServer.Text = "192.168.112.101";
        }

        internal void AcceptOnRegisterButton()
        {
            AcceptButton = btnSIPRegister;
            CancelButton = btnSIPUnregister;
            tbUserId.Focus();
        }

        internal void AcceptOnPickUpButton()
        {
            AcceptButton = btnPickUp;
            CancelButton = btnHangUp;
            tbDialNumber.Focus();
        }

        /// <summary>
        /// Sets a new GUI state.
        /// </summary>
        /// <param name="newState">The new GUI state object.</param>
        internal void SetGUIState(GUIState newState)
        {
            currentState = newState;
        }

        #region About box

        /// <summary>
        /// Shows an about box.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void btnAbout_Click(object sender, EventArgs e)
        {
            AboutBox box = new AboutBox();
            box.ShowDialog();
        }

        #endregion

        #region Tooltips

        private ToolTip ToolTips;

        /// <summary>
        /// Creates some tool tips.
        /// </summary>
        private void CreateToolTips()
        {
            ToolTips = new ToolTip();
            ToolTips.Active = chbxToolTip.Checked;
            ToolTips.InitialDelay = 500;
            ToolTips.ReshowDelay = 250;
            ToolTips.AutoPopDelay = 10000;
            ToolTips.IsBalloon = true;
            ToolTips.ShowAlways = true;
            ToolTips.ReshowDelay = 1000;

            ToolTips.SetToolTip(btnAbout, "Information about Ozeki Informatics ltd.");
            ToolTips.SetToolTip(chbxToolTip, "Turns on or off tooltips.");
            ToolTips.SetToolTip(tbDialNumber, "The phone number to call.");
            ToolTips.SetToolTip(tbDisplayName, "Display name on the called phone.");
            ToolTips.SetToolTip(tbDomainServer, "The address of VoIP PBX/Domain server.");
            ToolTips.SetToolTip(tbProxy, "The address of the outbound proxy. (Leave blank if no proxy will be used.)");
            ToolTips.SetToolTip(nudKeepAlive, "SIP NAT keep alive packets sending interval.");
            ToolTips.SetToolTip(tbPhoneStatus, "The status of the selected call.");
            ToolTips.SetToolTip(tbPlayFile, "The path of a wav file.");
            ToolTips.SetToolTip(tbRecordFile, "The path of a wav file.");
            ToolTips.SetToolTip(tbRegisterName, "SIP register name on PBX/Domain server, this could be different from SIP username.");
            ToolTips.SetToolTip(tbRegisterPassword, "SIP register password on PBX/Domain server for register name.");
            ToolTips.SetToolTip(tbUserId, "The SIP username.");
            ToolTips.SetToolTip(tbRingtoneName, "The wav file loaded as ringtone.");
            ToolTips.SetToolTip(tbRingbackTone, "The wav file loaded as ringback tone.");
            ToolTips.SetToolTip(rtbSIPMeassages, "All received and sent SIP messages by the softphone.");

            ToolTips.SetToolTip(chkbATAMode, "Analog Telephone Adapter mode does not require registration information.");
            ToolTips.SetToolTip(chkbEnableLogging, "Enable/Disable logging all system events.");
            ToolTips.SetToolTip(chkbMicrophoneMute, "Mutes the microphone.");
            ToolTips.SetToolTip(chkbRegistrationRequired, "Registration is requiered for PBX/Domain server.");
            ToolTips.SetToolTip(chkbSpeakerMute, "Mutes the speaker");
            ToolTips.SetToolTip(chbDND, "Do Not Disturb on selected line.");

            ToolTips.SetToolTip(btnCodecsDeselectAll, "Deselect all codecs for audio communication.");
            ToolTips.SetToolTip(btnCodecsSelectAll, "Select all codecs for audio communication.");

            ToolTips.SetToolTip(btnHangUp, "Hangs up the selected call.\r\nClears the dial string.");
            ToolTips.SetToolTip(btnHold, "Holds or unholds the line, this functionality depends on the other side as well.");

            ToolTips.SetToolTip(chbKeepAliveDisable, "Disable keep alive NAT packets.");

            ToolTips.SetToolTip(btnPhoneLine1, "Selects phone line #1 from the list.");
            ToolTips.SetToolTip(btnPhoneLine2, "Selects phone line #2 from the list.");
            ToolTips.SetToolTip(btnPhoneLine3, "Selects phone line #3 from the list.");
            ToolTips.SetToolTip(btnPhoneLine4, "Selects phone line #4 from the list.");
            ToolTips.SetToolTip(btnPhoneLine5, "Selects phone line #5 from the list.");
            ToolTips.SetToolTip(btnPhoneLine6, "Selects phone line #6 from the list.");
            ToolTips.SetToolTip(btnPickUp, "Calls or picks up an incomming call.");
            ToolTips.SetToolTip(btnRingtone, "Selects a wav file as ringtone");
            ToolTips.SetToolTip(btnRingbackTone, "Selects a wav file as ringback tone.");

            ToolTips.SetToolTip(btnSIPRegister, "Registers the phone line on PBX/Domain server based on given information.");
            ToolTips.SetToolTip(btnSIPUnregister, "Unregisters the phone line from PBX/Domain server.\r\nClear the registration fields if there is no registered phone line.");

            ToolTips.SetToolTip(lbCodecs, "List of all audio codecs supported by the softphone. If none or telephone-event is selected the call will not start.");
            ToolTips.SetToolTip(lbPhoneCalls, "All calls, please select the active one.");
            ToolTips.SetToolTip(lbPhoneLines, "All usable phone lines, please select one line to create calls.");

            ToolTips.SetToolTip(trckbMicrophoneVolumeControl, "The volume of microphone.");
            ToolTips.SetToolTip(trckbSpeakerVolumeControl, "The volume of speaker.");

            ToolTips.SetToolTip(chkbAutoAnswer, "Click to automatically answer the incoming calls.");
            ToolTips.SetToolTip(btnRedial, "Click to redial the last outgoing call.");
            ToolTips.SetToolTip(btnPlayFileBrowse, "Click to browse through folders to find the file you want.");
            ToolTips.SetToolTip(btnPlayFileStart, "Click to plays/pause the selected playback audio during the selected call.");
            ToolTips.SetToolTip(btnPlayFileStop, "Click to stop the selected playback audio.");
            ToolTips.SetToolTip(btnRecordFileBrowse, "Click to browse through folders to find the file you want.");
            ToolTips.SetToolTip(btnRecordFileStart, "Click to record/pause the selected call into the selected audio file.");
            ToolTips.SetToolTip(btnRecordFileStop, "Click to stop the recording.");

            ToolTips.SetToolTip(lbCallHistory, "Displays the previously dialed numbers and received calls.");
        }

        /// <summary>
        /// Turns on or off the tooltips.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void chbxToolTip_CheckedChanged(object sender, EventArgs e)
        {
            ToolTips.Active = chbxToolTip.Checked;
        }

        #endregion

        #region Form events

        private void MainForm_Load(object sender, EventArgs e)
        {
            SetAvailableCodecs();
            CheckedSpeexDSP();
            GetPlaybackSettings();
            GetRecordingSettings();
            GetCameraSettings();

            //Add videoProvider to the remote videoviewer control.
            videoViewerRemote.SetImageProvider(softphoneEngine.GetRemoteImageProvider());
            //Add videoProvider to the local videoviewer control.
            videoViewerLocal.SetImageProvider(softphoneEngine.GetLocalImageProvider());

            GetIPSettings();

            cobNatType.SelectedIndex = 3;
            ChangeNATSettings();
        }

        #region SoftphoneEngine eventhandlers

        private void WireUpSoftphoneEngineEvents()
        {
            /* Attach event handlers. Event handlers turns event
         * into commands to the other side.
         * */
            softphoneEngine.IncomingCall += (softphoneEngine_IncommingCall);
            softphoneEngine.IncomingCallCancelled += (softphoneEngine_IncomingCallCanceled);
            softphoneEngine.PropertyChanged += (softphoneEngine_PropertyChanged);
            softphoneEngine.SIPMessageReceived += (softphoneEngine_SIPMessageReceived);
            softphoneEngine.SIPMessageSent += (softphoneEngine_SIPMessageSent);

            softphoneEngine.PhoneCallStateHasChanged += (softphoneEngine_PhoneCallStateHasChanged);
            softphoneEngine.PlaybackStopped += (softphoneEngine_PlaybackStopped);
            softphoneEngine.MicrophoneLevelChanged += (softphoneEngine_MicrophoneLevelChanged);
            softphoneEngine.SpeakerLevelChanged += (softphoneEngine_SpeakerLevelChanged);
            softphoneEngine.Disposing += (softphoneEngine_Disposing);
        }

        /// <summary>
        /// Event handler: SoftPhoneEngine is disposing.
        /// </summary>
        /// <param name="sender">The softphoneEngine.</param>
        /// <param name="e">Bool value is used as in normal disposing.</param>
        void softphoneEngine_Disposing(object sender, GEventArgs<bool> e)
        {
            Dispose();
        }

        /// <summary>
        /// Event handler: Some container property has been changed in the model.
        /// </summary>
        /// <param name="sender">The model.</param>
        /// <param name="e">The name of the property that has changed.</param>
        /// <remarks>
        /// Either the phone call list or the phone line list can be changed. When one of these properties
        /// has been changed, the view must redisplay the actual state of the model.
        /// </remarks>
        void softphoneEngine_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("PhoneLinesBijection"))
            {
                ChangePhoneLineList(softphoneEngine.PhoneLines);
                return;
            }

            if (e.PropertyName.Equals("PhoneCallsBijection"))
            {
                ChangePhoneCallList(softphoneEngine.PhoneCalls);
                return;
            }
        }

        /// <summary>
        /// Event handler: Incoming call has arrived into the model.
        /// </summary>
        /// <param name="sender">The model.</param>
        /// <param name="e">The incoming call object.</param>
        /// <remarks>
        /// An incomming call is arrived to the model, the model presenter asks the view to visualize it.
        /// </remarks>
        void softphoneEngine_IncommingCall(object sender, GEventArgs<PhoneCallInfo> e) { IncomingCall(e.Item); }


        /// <summary>
        /// Event handler: SIP message was sent by the model.
        /// </summary>
        /// <param name="sender">The model.</param>
        /// <param name="e">The string represenation of the SIP message.</param>
        /// <remarks>
        /// It is a monitoring tool on SIP messages. The model emits the SIPMessageSent event and the model
        /// presenter asks the view to log that message.
        /// </remarks>
        void softphoneEngine_SIPMessageSent(object sender, GEventArgs<string> e) { SIPMessageSent(e.Item); }

        /// <summary>
        /// Event handler: SIP message was received from the model.
        /// </summary>
        /// <param name="sender">The model.</param>
        /// <param name="e">The string represenation of the SIP message.</param>
        /// <remarks>
        /// It is a monitoring tool on SIP messages. The model emits the SIPMessageReceived event and the model
        /// presenter asks the view to log that message.
        /// </remarks>
        void softphoneEngine_SIPMessageReceived(object sender, GEventArgs<string> e) { SIPMessageReceived(e.Item); }

        /// <summary>
        /// Event handler: A phone call has changed its state.
        /// </summary>
        /// <param name="sender">The model.</param>
        /// <param name="e">The phone call and its state.</param>
        /// <remarks>
        /// When a phone call changes his state, usually some SIP event inducate the change, the model arises the event of
        /// PhoneCallStateHasChanged, the model presenter asks the view to refresh its state, and the information about
        /// the given phone call.
        /// </remarks>
        void softphoneEngine_PhoneCallStateHasChanged(object sender, GEventArgs<Ozeki.Common.OzTuple<PhoneCallInfo, CallState>> e) { CallStateInfo(e.Item.Item1, e.Item.Item2); }



        void softphoneEngine_SpeakerLevelChanged(object sender, VoIPEventArgs<float> e)
        {
            UpdateSpeakerLevel(e.Item);
        }

        void softphoneEngine_MicrophoneLevelChanged(object sender, VoIPEventArgs<float> e)
        {
            UpdateMicrophoneLevel(e.Item);
        }

        void softphoneEngine_PlaybackStopped(object sender, EventArgs e)
        {
            PlaybackStopped();
        }

        void softphoneEngine_IncomingCallCanceled(object sender, GEventArgs<PhoneCallInfo> e)
        {
            IncomingCallCancelled(e.Item);
        }


        #endregion


        /// <summary>
        /// When the form is closing, it is going to emit a disposing request.
        /// </summary>
        /// <param name="e">Unused.</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            softphoneEngine.Dispose();
            base.OnClosing(e);
        }

        #endregion

        #region Registration


        /// <summary>
        /// Registration request by the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSIPRegister_Click(object sender, EventArgs e)
        {
            if (!ValidateRegFields())
            {
                ClearRegFields();
                return;
            }
            softphoneEngine.RegisterPhoneLine(PhoneLineInfoFromRegFields());
        }

        /// <summary>
        /// Starts unregistration process, or clears registration fields
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        /// <remarks>
        /// If the number of active phone line is zero, it simply clears the registration fields.
        /// If registration field are invalid, is not going to do anything.
        /// </remarks>
        private void btnSIPUnregister_Click(object sender, EventArgs e)
        {
            /* If the number of active phone line is zero, it simply clears the registration fields.
             * */
            if (NumberOfActivePhoneLines == 0)
            {
                ClearRegFields();
                return;
            }

            if (!ValidateRegFields())
                return;
            softphoneEngine.UnregisterPhoneLine(PhoneLineInfoFromRegFields());
        }

        /// <summary>
        /// Validates information contained by registration fields.
        /// </summary>
        /// <returns>Returns true if the information was valid, otherwise false.</returns>
        private bool ValidateRegFields()
        {
            return
                tbDisplayName.Text.Length != 0 &&
                tbUserId.Text.Length != 0 &&
                tbDomainServer.Text.Length != 0;
        }

        /// <summary>
        /// Calculates PhoneLineInfo objects from data held by the registration fields.
        /// </summary>
        /// <returns>The PhoneLineInfo.</returns>
        private PhoneLineInfo PhoneLineInfoFromRegFields()
        {
            bool regRequired = !chkbATAMode.Checked && chkbRegistrationRequired.Checked;
            var transportType = (TransportType)Enum.Parse(typeof(TransportType), cobTransport.Text, true);
            var srtpMode = (SRTPMode) Enum.Parse(typeof (SRTPMode), cobSRTPmode.Text, true);

            PhoneLineInfo SIPAccount = new PhoneLineInfo(
                tbDisplayName.Text,
                tbUserId.Text,
                tbRegisterName.Text,
                tbRegisterPassword.Text,
                tbDomainServer.Text,
                tbProxy.Text,
                regRequired,
                transportType,
                srtpMode);
            return SIPAccount;
        }


        /// <summary>
        /// Fills all registration fields with empty string.
        /// </summary>
        private void ClearRegFields()
        {
            tbDisplayName.Text = string.Empty;
            tbUserId.Text = string.Empty;
            tbRegisterName.Text = string.Empty;
            tbRegisterPassword.Text = string.Empty;
            tbDomainServer.Text = string.Empty;
            tbProxy.Text = string.Empty;
        }

        /// <summary>
        /// Fills all registration fields with information from PhoneLineInfo.
        /// </summary>
        /// <param name="info">The PhoneLineInfo object.</param>
        private void FillRegFields(PhoneLineInfo info)
        {
            tbDisplayName.Text = info.DisplayName;
            tbUserId.Text = info.Username;
            tbRegisterName.Text = info.RegisterName;
            tbRegisterPassword.Text = info.Password;
            tbDomainServer.Text = info.Domain;
            chkbRegistrationRequired.Checked = info.RegRequired;
            chkbATAMode.Checked = !info.RegRequired;
        }

        /// <summary>
        /// Changed registration requiered, password, registername fields active or inactive 
        /// depended the value of check box.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void cbATAMode_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbATAMode.Checked == true)
            {
                tbRegisterName.Enabled = false;
                tbRegisterPassword.Enabled = false;
                chkbRegistrationRequired.Enabled = false;
                lblRegisterName.Enabled = false;
                lblRegisterPassword.Enabled = false;
            }
            else
            {
                tbRegisterName.Enabled = true;
                tbRegisterPassword.Enabled = true;
                chkbRegistrationRequired.Enabled = true;
                lblRegisterName.Enabled = true;
                lblRegisterPassword.Enabled = true;
            }
        }

        #endregion

        #region PhoneLine

        /// <summary>
        /// Selects new line from the phone lines.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        /// <remarks>
        /// The GUI has only one listbox to show phone lines.
        /// </remarks>
        void lbPhoneLines_SelectedValueChanged(object sender, EventArgs e)
        {
            PhoneLineInfo line = lbPhoneLines.SelectedItem as PhoneLineInfo;
            if (line == null)
                return;

            bool? dnd = softphoneEngine.GetDNDInfo(line);
            if (dnd != null)
                PhoneLineDNDInfo(line, dnd.Value);

            MessageSummaryReceived(line, softphoneEngine.GetMessageSummary(line));

            /* Fill the registration fields with information about selected phone line.
             * */
            FillRegFields(line);
        }

        private void lbPhoneLines_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Update the phone line selector button box.
        /// </summary>
        /// <param name="info">The registered phone lines.</param>
        private void UpdatePhoneLineSelectorBlock(IEnumerable<PhoneLineInfo> info)
        {
            int n = info.Count();
            int l = PhoneLineSelectors.Length;
            for (int i = 0; i < l; ++i)
                PhoneLineSelectors[i].Enabled = i < n;
        }

        /// <summary>
        /// Updates information about phone lines.
        /// </summary>
        /// <param name="newPhoneLineList">The new phoneline list.</param>
        public void ChangePhoneLineList(IEnumerable<PhoneLineInfo> newPhoneLineList)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action<MainForm, IEnumerable<PhoneLineInfo>>)((t, e1) => t.ChangePhoneLineList(e1)), this, newPhoneLineList);
                return;
            }

            lbPhoneLines.UpdateFromIEnumerable(newPhoneLineList);
            UpdatePhoneLineSelectorBlock(newPhoneLineList);
            currentState.ActivePhoneLineNumberHasChanged();
        }

        /// <summary>
        /// Updates information about phone calls. 
        /// </summary>
        /// <param name="newPhoneCallList">The new phonecall list.</param>
        public void ChangePhoneCallList(IEnumerable<PhoneCallInfo> newPhoneCallList)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action<MainForm, IEnumerable<PhoneCallInfo>>)((t, e1) => t.ChangePhoneCallList(e1)), this, newPhoneCallList);
                return;
            }

            lbPhoneCalls.UpdateFromIEnumerable(newPhoneCallList);
            currentState.ActivePhoneCallNumberHasChanged();
            GetCallInfo();
        }

        /// <summary>
        /// Selects the i.th phone line.
        /// </summary>
        /// <param name="i">The ordinal number of phone line.</param>
        /// <remarks>
        /// Sets it automatically also in the listbox of phone lines.
        /// </remarks>
        private void SetSelectedPhoneLine(int i)
        {
            if (i < 0 || i >= lbPhoneLines.Items.Count)
                return;

            lbPhoneLines.SelectedItem = lbPhoneLines.Items[i];
        }

        /// <summary>
        /// Tries to set the selected line to the first one from the list.
        /// </summary>
        /// <remarks>
        /// If there is no line, nothing will happen.
        /// </remarks>
        internal void SetActivePhoneLineToFirstOne()
        {
            if (lbPhoneLines.Items.Count == 0)
                return;

            lbPhoneLines.SelectedItem = lbPhoneLines.Items[0];
        }

        #endregion

        #region Dialing Interface

        /// <summary>
        /// Enables all numeric keys on GUI.
        /// </summary>
        internal void EnableNumkeys()
        {
            foreach (Button button in NumKeys)
                button.Enabled = true;
        }

        /// <summary>
        /// Disables all numeric keys on GUI.
        /// </summary>
        internal void DisableNumkeys()
        {
            foreach (Button button in NumKeys)
                button.Enabled = false;
        }

        /// <summary>
        /// Enables PickUp button.
        /// </summary>
        internal void EnablePickUpButton() { btnPickUp.Enabled = true; }

        /// <summary>
        /// Disables PickUp button.
        /// </summary>
        internal void DisablePickUpButton() { btnPickUp.Enabled = false; }

        /// <summary>
        /// Enables HangUp button.
        /// </summary>
        internal void EnableHangUpButton() { btnHangUp.Enabled = true; }

        /// <summary>
        /// Disables HangUp button.
        /// </summary>
        internal void DisableHangUpButton() { btnHangUp.Enabled = false; }

        /// <summary>
        /// Disable SendVideo and StopVideo buttons.
        /// </summary>
        internal void DisableVideoControlButton()
        {
            btnStartVideo.Enabled = false;
            btnStopVideo.Enabled = false;
        }

        /// <summary>
        /// Enable SendVideo and StopVideo buttons.
        /// </summary>
        internal void EnableVideoControlButton()
        {
            btnStartVideo.Enabled = true;

            btnStopVideo.Enabled = localVideoTest;
        }

        /// <summary>
        /// Sends information about that PickUp button hs pressed to the state of GUI.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void btnPickUp_Click(object sender, EventArgs e) { currentState.PickUpPressed(); }

        /// <summary>
        /// Sends information about that HangUp button hs pressed to the state of GUI.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void btnHangUp_Click(object sender, EventArgs e) { currentState.HangUpPressed(); }



        /// <summary>
        /// Adds the appropriate keypad character to the dial display.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnKeypad_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
                ItsDisplay.Text += btn.Text;
        }


        /// <summary>
        /// Selects phone line.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void btnPhoneLines_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
                SetSelectedPhoneLine(Int32.Parse(btn.Tag.ToString()));
        }

        #endregion

        #region Logging


        /// <summary>
        /// Adds SIP message to the SIP message log.
        /// </summary>
        /// <param name="message">The SIP message.</param>
        public void SIPMessageReceived(string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action<MainForm, string>)((t, e1) => t.SIPMessageReceived(e1)), this, message);
                return;
            }
            SIPMessageTextBoxUpdate(message);
        }

        /// <summary>
        /// Adds SIP message to the SIP message log.
        /// </summary>
        /// <param name="message">The SIP message.</param>
        public void SIPMessageSent(string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action<MainForm, string>)((t, e1) => t.SIPMessageSent(e1)), this, message);
                return;
            }
            SIPMessageTextBoxUpdate(message);
        }


        /// <summary>
        /// Add SIP message string at the end of the SIP message text box, and scrolls there.
        /// </summary>
        /// <param name="text">The SIP message.</param>
        /// <remarks>
        /// If the containment is larger than 1MB the text is splitted to half.
        /// </remarks>
        private void SIPMessageTextBoxUpdate(string text)
        {
            try
            {
                string t = rtbSIPMeassages.Text;
                if (t.Length > 1024 * 1024)
                    t = t.Substring(512 * 1024);

                rtbSIPMeassages.Text = t + text;
                rtbSIPMeassages.SelectionStart = rtbSIPMeassages.Text.Length;
                rtbSIPMeassages.ScrollToCaret();
                rtbSIPMeassages.Refresh();
            }
            catch (Exception)
            {
            }

        }

        private void LogEvent(string logMsg)
        {
            rtbEvents.BeginInvoke(new MethodInvoker(delegate
            {
                rtbEvents.Text += string.Format("{0}: {1}\r\n", DateTime.Now, logMsg);
            }));
        }

        #endregion

        #region Call History

        private void AddCallToHistory(PhoneLineInfo line, string dial, CallDirection type)
        {
            PhoneCallInfo info = new PhoneCallInfo(line, dial, type);
            softphoneEngine.AddCallToHistory(info);
            lbCallHistory.Items.Add(info);
        }

        #endregion

        #region NAT Settings


        private void btnApply_Click(object sender, EventArgs e)
        {
            ChangeNATSettings();
            MessageBox.Show("You have to re-register your SIP account to apply the changes.", "Information");
        }

        private void ChangeNATSettings()
        {
            NatTraversalMethod traversalMethodType = (NatTraversalMethod)cobNatType.SelectedIndex;
            string serverAddress = tbNatServer.Text;
            string userName = tbNatUserName.Text;
            string password = tbNatPassword.Text;
            softphoneEngine.ChangeNatSettings(new NATSettingsInfo(traversalMethodType, serverAddress, userName, password));
        }

        #endregion

        #region IP adapter setting

        /// <summary>
        /// Updates network adapter settings.
        /// </summary>
        public void GetIPSettings()
        {
            List<IPAddress> allIP = softphoneEngine.GetLocalAddressList();
            IPAddress defaultIP = softphoneEngine.GetDefaultIP();

            foreach (var ipAddress in allIP)
            {
                cobAdaptersIP.Items.Add(ipAddress);
            }

            usedIPaddress = defaultIP;
            cobAdaptersIP.SelectedItem = defaultIP;
        }

        internal void DisableAdapterSettings()
        {
            btnAdapterApply.Enabled = false;
            cobAdaptersIP.Enabled = false;
        }

        internal void EnableAdapterSettings()
        {
            btnAdapterApply.Enabled = true;
            cobAdaptersIP.Enabled = true;
        }
        /// <summary>
        /// Creates an adapter change request if it necessary.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdapterApply_Click(object sender, EventArgs e)
        {
            IPAddress ip = cobAdaptersIP.SelectedItem as IPAddress;

            if (ip != usedIPaddress)
            {
                softphoneEngine.ChangeNetworkAdapter(ip);
            }
        }

        #endregion

        #region Call

        /// <summary>
        /// Actual Incoming call.
        /// </summary>
        /// <remarks>
        /// Only one incoming call exists in the system, if a new one is received, the new kicks out the previous one.
        /// </remarks>
        public PhoneCallInfo ActualIncomingCall { get; internal set; }

        /// <summary>
        /// Gets or sets the last dialed number
        /// </summary>
        private string LastDial { get; set; }

        /// <summary>
        /// Creates call request for the given dial string.
        /// </summary>
        /// <param name="dial">The dial string.</param>
        /// <remarks>
        /// The call request  is associated automatically with the selected phone line. If the phone line is
        /// not selected, nothing will happen.
        /// </remarks>
        internal void CreateCall(string dial)
        {
            if (lbPhoneLines.SelectedItem == null)
                return;

            PhoneLineInfo line = lbPhoneLines.SelectedItem as PhoneLineInfo;
            if (line == null)
                return;

            DisableRedialButton();

            softphoneEngine.StartCall(line, dial);
            AddCallToHistory(line, dial, CallDirection.Outgoing);

            string logMsg = string.Format("Dialing {0} @ line {1}", dial, line);
            LogEvent(logMsg);
        }


        /// <summary>
        /// Emits request to hang up the active phone call.
        /// </summary>
        internal void HangUpSelectedCall()
        {
            PhoneCallInfo callInfo = lbPhoneCalls.SelectedItem as PhoneCallInfo;
            if (callInfo == null)
                return;

            tbPhoneStatus.Text = string.Empty;
            softphoneEngine.HangUpCall(callInfo);

            StopRecord();
            StopPlayback();
        }

        /// <summary>
        /// Displays call state information for the given call.
        /// </summary>
        /// <param name="callInfo">The call object.</param>
        /// <param name="callState">The state of the call.</param>
        public void CallStateInfo(PhoneCallInfo callInfo, CallState callState)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action<MainForm, PhoneCallInfo, CallState>)((t, c, c1) => t.CallStateInfo(c, c1)), this, callInfo, callState);
                return;
            }

            PhoneCallInfo selectedCall = SelectedCall;
            if (selectedCall == null)
                return;

            if (!selectedCall.Equals(callInfo))
                return;

            switch (callState)
            {
                case CallState.InCall: 
                    btnHold.Text = "Hold"; 
                    btnHold.Enabled = true;
                    videoViewerRemote.Start();
                    break;
                case CallState.LocalHeld: 
                    btnHold.Text = "Unhold"; 
                    break;
                case CallState.RemoteHeld: 
                    btnHold.Text = "Holded"; 
                    btnHold.Enabled = false; 
                    break;
                case CallState.Completed:
                    softphoneEngine.StopCamera(callInfo);
                    videoViewerLocal.Stop(); 
                    videoViewerRemote.Stop();
                    StopRecord();
                    break;
            }

            tbPhoneStatus.Text = "Call " + callInfo.Dial + " is " + callState + ".";
        }

        /// <summary>
        /// Displays incoming call.
        /// </summary>
        /// <param name="call">The incoming call.</param>
        public void IncomingCall(PhoneCallInfo call)
        {
            if (call.Direction == CallDirection.Outgoing)
                return;

            if (InvokeRequired)
            {
                BeginInvoke((Action<MainForm, PhoneCallInfo>)((t, e1) => t.IncomingCall(e1)), this, call);
                return;
            }

            if (ActualIncomingCall != null)
            {
                softphoneEngine.RejectCall(call);
                ActualIncomingCall = null;
                ItsDisplay.Text = string.Empty;
            }

            ActualIncomingCall = call;
            ItsDisplay.Text = call.ToString();
            tbPhoneStatus.Text = "Incoming call!";

            string logMsg = string.Format("Incoming call from {0} [line: {1}]", call.Dial, call.PhoneLineInfo);
            LogEvent(logMsg);

            AddCallToHistory(call.PhoneLineInfo, call.Dial, call.Direction);

            SetGUIState(new IncommingCall(this));

            if (AutoAcceptCalls)
                currentState.PickUpPressed();
        }

        /// <summary>
        /// Accept the incoming call.
        /// </summary>
        /// <remarks>
        /// Only one incoming call exists in the system, if a new one is received, the new kicks out the previous one.
        /// </remarks>
        public void AcceptIncomingCall()
        {
            if (ActualIncomingCall == null)
                return;

            var call = ActualIncomingCall;
            ActualIncomingCall = null;

            softphoneEngine.AcceptCall(call);

            string logMsg = string.Format("Incoming call accepted {0}", ActualIncomingCall);
            LogEvent(logMsg);
        }

        /// <summary>
        /// Rejects the incoming call.
        /// </summary>
        /// <remarks>
        /// Only one incoming call exists in the system, if a new one is received, the new kicks out the previous one.
        /// </remarks>
        public void RejectIncomingCall()
        {
            if (ActualIncomingCall == null)
                return;

            var call = ActualIncomingCall;
            ActualIncomingCall = null;

            softphoneEngine.RejectCall(call);
        }

        /// <summary>
        /// Incoming call has been cancelled.
        /// </summary>
        /// <param name="call"></param>
        public void IncomingCallCancelled(PhoneCallInfo call)
        {
            ActualIncomingCall = null;

            BeginInvoke((Action)(() =>
            {
                SetGUIState(new LineAndNoCall(this));
                tbDialNumber.Text = string.Empty;
            }));
            string logMsg = string.Format("Incoming call {0} has been cancelled [line: {1}]", call.Dial, call.PhoneLineInfo);
            LogEvent(logMsg);
        }

        /// <summary>
        /// Requests information for the selected phone call.
        /// </summary>
        internal void GetCallInfo()
        {
            if (lbPhoneCalls.SelectedItem == null)
            {
                tbPhoneStatus.Text = string.Empty;
                return;
            }

            PhoneCallInfo call = lbPhoneCalls.SelectedItem as PhoneCallInfo;
            if (call == null)
                return;

            CallState? state = softphoneEngine.GetCallState(call);
            if (state == null)
                return;

            CallStateInfo(call, state.Value);
        }

        /// <summary>
        /// Selects new active call from the phone calls.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        /// <remarks>
        /// The GUI has only one listbox to show phone calls.
        /// </remarks>
        void lbPhoneCalls_SelectedValueChanged(object sender, EventArgs e)
        {
            if (lbPhoneCalls.SelectedItem == null)
                tbPhoneStatus.Text = string.Empty;

            /* Create request the get some information about 
             * the actual phone call, when the information will be received
             * the GUI will update the textbox of state of phone call.
             * */
            GetCallInfo();

            PhoneCallInfo call = lbPhoneCalls.SelectedItem as PhoneCallInfo;
            if (call == null)
                return;

            /* The selected call will be the active one.
             * */
            softphoneEngine.SetActiveCall(call);
        }

        /// <summary>
        /// Tries to set the active call to the first one from the list.
        /// </summary>
        /// <remarks>
        /// If there is no call, nothing will happen. When the lbPhoneCall
        /// changes the set request of active call is emitted automatically.
        /// </remarks>
        internal void SetActiveCallToFirstOne()
        {
            if (lbPhoneCalls.Items.Count == 0)
                return;

            PhoneCallInfo callInfo = lbPhoneCalls.Items[0] as PhoneCallInfo;
            if (callInfo == null)
                return;

            lbPhoneCalls.SelectedItem = callInfo;
        }

        #endregion

        #region Auto-Answer, Redial, Hold, DND, Transfer

        // Auto-answer

        /// <summary>
        /// Gets or sets a value indicating whether the application will accept the incoming calls automatically.
        /// </summary>
        private bool AutoAcceptCalls;

        internal void EnableAutoAnswerButton()
        {
            chkbAutoAnswer.Enabled = true;
            chkForward.Enabled = true;
        }

        internal void DisableAutoAnswerButton()
        {
            chkbAutoAnswer.Enabled = false;
            chkForward.Enabled = false;
        }

        private void chbAutoAnswer_CheckedChanged(object sender, EventArgs e)
        {
            AutoAcceptCalls = chkbAutoAnswer.Checked;

            string enabledState = (AutoAcceptCalls) ? "Enabled" : "Disabled";

            string logMsg = string.Format("Auto answer changed to: {0}", enabledState);
            LogEvent(logMsg);
        }

        // Redial
        internal void EnableRedialButton()
        {
            btnRedial.Enabled = true;
        }

        internal void DisableRedialButton()
        {
            btnRedial.Enabled = false;
        }

        private void btnRedial_Click(object sender, EventArgs e)
        {
            if (SelectedLine == null)
                return;

            LastDial = null;

            PhoneCallInfo info = softphoneEngine.GetLastCall(CallDirection.Outgoing);
            if (info != null)
                LastDial = info.Dial;

            if (LastDial != null)
                CreateCall(LastDial);
        }


        internal void EnableHoldButton(bool held)
        {
            btnHold.Enabled = true;
            btnHold.Text = held ? "Unhold" : "Hold";
        }

        internal void DisableHoldButton()
        {
            btnHold.Text = "Hold";
            btnHold.Enabled = false;
        }


        internal void DisableTransfer()
        {
            btnTransfer.Enabled = false;
        }

        internal void EnableTransfer()
        {
            btnTransfer.Enabled = true;
        }

        private void btnHold_Click(object sender, EventArgs e)
        {
            softphoneEngine.HoldCall(SelectedCall);
        }

        internal void EnableDNDButton()
        {
            chbDND.Enabled = true;
        }

        internal void DisableDNDButton()
        {
            chbDND.Checked = false;
            chbDND.Enabled = false;
        }

        private void chbDND_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox dnd = sender as CheckBox;
            softphoneEngine.SetDND(SelectedLine, dnd.Checked);
        }

        public void PhoneLineDNDInfo(PhoneLineInfo lineInfo, bool dnd)
        {
            if (!chbDND.Enabled)
                return;

            chbDND.Checked = dnd;
        }

        public void CallGetDNDInfo(PhoneLineInfo lineInfo)
        {
            bool? dnd = softphoneEngine.GetDNDInfo(lineInfo);
            if (dnd != null)
                PhoneLineDNDInfo(lineInfo, dnd.Value);
        }

        // Call history
        private void lbCallHistory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (SelectedLine == null)
                return;

            PhoneCallInfo info = lbCallHistory.SelectedItem as PhoneCallInfo;
            if (info == null)
                return;

            tbDialNumber.Text = info.Dial;
            btnPickUp_Click(this, EventArgs.Empty);
        }

        #endregion

        #region Keep-alive

        public void EnableKeepAliveBox()
        {
            grpbKeepAlive.Enabled = true;
            if (chbKeepAliveDisable.Checked)
                return;

            int interval = Decimal.ToInt32(nudKeepAlive.Value);
            
        }

        public void DisableKeepAliveBox()
        {
            grpbKeepAlive.Enabled = false;
        }

        private void nudKeepAlive_ValueChanged(object sender, EventArgs e)
        {
            if (chbKeepAliveDisable.Checked)
                return;

            int interval = Decimal.ToInt32(nudKeepAlive.Value);
        }

        private void chbKeepAliveDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (chbKeepAliveDisable.Checked)
            {
                chbKeepAliveDisable.Text = "Enable";
                return;
            }

            chbKeepAliveDisable.Text = "Disable";
            int interval = Decimal.ToInt32(nudKeepAlive.Value);
        }

        #endregion

        #region DTMF


        private void Keypad_MouseDown(object sender, MouseEventArgs e)
        {
            PhoneCallInfo call = this.SelectedCall;
            if (call == null)
                return;

            Button keyPadBtn = sender as Button;
            if (keyPadBtn != null)
            {
                int dtmf = 0;
                if (!(int.TryParse(keyPadBtn.Tag.ToString(), out dtmf)))
                    return;
                softphoneEngine.StartDTMFSignal(call, dtmf);
            }
        }

        private void Keypad_MouseUp(object sender, MouseEventArgs e)
        {
            PhoneCallInfo call = this.SelectedCall;
            if (call == null)
                return;

            Button keyPadBtn = sender as Button;
            if (keyPadBtn != null)
            {
                int dtmf = 0;
                if (!(int.TryParse(keyPadBtn.Tag.ToString(), out dtmf)))
                    return;

                softphoneEngine.StopDTMFSignal(call, dtmf);
            }
        }

        #endregion

        #region Codecs

        private void lbCodecs_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO
            //if (CodecStateChanged == null)
            //    return;

            foreach (CodecInfo ci in lbCodecs.Items)
                ci.Enabled = false;
            foreach (CodecInfo ci in lbCodecs.SelectedItems)
                ci.Enabled = true;
            foreach (CodecInfo ci in lbCodecs.Items)
                if (ci.Enabled)
                    softphoneEngine.EnableCodec(ci.PayloadType);
                else
                    softphoneEngine.DisableCodec(ci.PayloadType);
            //if (CodecStateChanged != null)
            //       CodecStateChanged(this, new GEventArgs<CodecInfo>(ci));
            //   else
            //       return;
        }

        private void btnCodecsSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0, n = lbCodecs.Items.Count; i < n; ++i)
                lbCodecs.SetSelected(i, true);
        }

        private void btnCodecsDeselectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0, n = lbCodecs.Items.Count; i < n; ++i)
                lbCodecs.SetSelected(i, false);
        }

        public void SetAvailableCodecs()
        {
            IEnumerable<CodecInfo> codecs = softphoneEngine.GetCodecs();
            lbCodecs.BeginUpdate();
            lbCodecs.Items.Clear();
            int index = 0;
            foreach (CodecInfo t in codecs)
            {
                lbCodecs.Items.Add(t);
                lbCodecs.SetSelected(index, t.Enabled);
                ++index;
            }
            lbCodecs.EndUpdate();
        }

        #endregion

        #region Playback, Record

        private void btnPlayFileBrowse_Click(object sender, EventArgs e)
        {
            PhoneCallInfo call = this.SelectedCall;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "Wave Files (*.wav)|*.wav";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                softphoneEngine.SetPlayAudioFile(ofd.FileName);
                tbPlayFile.Text = ofd.FileName;
            }
        }


        public void OpenFileToRecord()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Wave Files (*.wav)|*.wav";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                softphoneEngine.SetRecAudioFile(sfd.FileName);
                tbRecordFile.Text = sfd.FileName;
                softphoneEngine.StartRecord();
                btnRecordFileStart.Text = "Pause";
                btnRecordFileStop.Enabled = true;
            }
        }

        private void btnPlayFileStart_Click(object sender, EventArgs e)
        {
            softphoneEngine.StartPlay();
            if (btnPlayFileStart.Text == "Play")
                btnPlayFileStart.Text = "Pause";
            else
                btnPlayFileStart.Text = "Play";

            btnPlayFileStop.Enabled = true;
        }

        private void btnPlayFileStop_Click(object sender, EventArgs e)
        {
            StopPlayback();
        }

        private void StopPlayback()
        {
            softphoneEngine.StopPlay();
        }

        public void PlaybackStopped()
        {
            btnPlayFileStart.BeginInvoke(new MethodInvoker(delegate
            {
                btnPlayFileStart.Text = "Play";
                btnPlayFileStop.Enabled = false;
            }));
        }

        public void EnableAudioFileButtons()
        {
            btnRecordFileStart.Enabled = true;
            btnPlayFileStart.Enabled = true;
        }

        public void DisableAudioFileButtons()
        {
            btnRecordFileStart.Enabled = false;
            btnPlayFileStart.Enabled = false;
        }

        private void btnRecordFileBrowse_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Wave Files (*.wav)|*.wav";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                softphoneEngine.SetRecAudioFile(sfd.FileName);
                tbRecordFile.Text = sfd.FileName;
            }
        }

        private void btnRecordFileStart_Click(object sender, EventArgs e)
        {
              AudioRecordState state =  softphoneEngine.StartRecord();

            if (state == AudioRecordState.Stopped)
            {
                OpenFileToRecord();
            }

            if (state == AudioRecordState.Paused)
            {
                btnRecordFileStart.Text = "Continue";
            }
            if (state == AudioRecordState.Started)
            {
                btnRecordFileStart.Text = "Pause";
            }

            btnRecordFileStop.Enabled = true;
        }

        private void btnRecordFileStop_Click(object sender, EventArgs e)
        {
            StopRecord();
        }

        /// <summary>
        /// Stop recording request by the user.
        /// </summary>
        private void StopRecord()
        {
            softphoneEngine.StopRecord();
            btnRecordFileStop.Enabled = false;
            btnRecordFileStart.Text = "Record";
            tbRecordFile.Text = "";
        }

        #endregion

        #region Voice Enhancement

        /// <summary>
        /// If Speex PreProcessor cannot be loaded then disable the UI controls.
        /// </summary>
        public void CheckedSpeexDSP()
        {
            //Indicates whether the Speex PreProcessor has been loaded.
            bool available = softphoneEngine.CheckSpeexDSP();
            if (!available)
            {
                chkbAEC.Enabled = false;
                chkbAGC.Enabled = false;
                chkbDenoise.Enabled = false;
                numAECDelay.Enabled = false;
                numMaxAGCGain.Enabled = false;
            }
        }

        private void chkbAEC_CheckedChanged(object sender, EventArgs e)
        {
            bool isEnabled = ((CheckBox)sender).Checked;

            softphoneEngine.EnableEchoCancellation(isEnabled);
        }

        private void chkbAGC_CheckedChanged(object sender, EventArgs e)
        {
            bool isEnabled = ((CheckBox)sender).Checked;

            softphoneEngine.EnablePreProcessorComponent(0, isEnabled);
        }

        private void chkbDenoise_CheckedChanged(object sender, EventArgs e)
        {
            bool isEnabled = ((CheckBox)sender).Checked;

            softphoneEngine.EnablePreProcessorComponent(2, isEnabled);
        }

        private void tbAECDelay_TextChanged(object sender, EventArgs e)
        {
            int val = (int)(((NumericUpDown)sender).Value);

            softphoneEngine.ChangeAECDelay(val);
        }

        private void numMaxAGCGain_ValueChanged(object sender, EventArgs e)
        {
            int val = (int)(((NumericUpDown)sender).Value);

            softphoneEngine.ChangeAGCMaxGain(val);
        }

        #endregion

        #region Speaker, Microphone, Video Settings


        /// <summary>
        /// Changes settings of the speaker.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void chkbSpeakerMute_CheckedChanged(object sender, EventArgs e) { SpeakerChanged(); }

        /// <summary>
        /// Changes settings of the speaker.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void trckbSpeakerVolumeControll_Scroll(object sender, EventArgs e) { SpeakerChanged(); }

        /// <summary>
        /// Changes settings of the microphone.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void trckbMicrophoneVolumeControll_Scroll(object sender, EventArgs e) { MicrophoneChanged(); }

        /// <summary>
        /// Changes settings of the microphone.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void chkbMicrophoneMute_CheckedChanged(object sender, EventArgs e) { MicrophoneChanged(); }

        private void cobSpeakerDevice_SelectedIndexChanged(object sender, EventArgs e) { SpeakerChanged(); }

        private void cobMicrophoneDevice_SelectedIndexChanged(object sender, EventArgs e) { MicrophoneChanged(); }

        private void cobVideoResolutions_SelectedIndexChanged(object sender, EventArgs e)
        {
            VideoCapabilities currentCapabilities = cobVideoResolutions.SelectedItem as VideoCapabilities;


            if (currentCapabilities == null)
                return;


            softphoneEngine.SetResolution(currentCapabilities);
        }

        /// <summary>
        /// Changes settings of the Camera.
        /// </summary>
        /// <param name="sender">Unused.</param>
        /// <param name="e">Unused.</param>
        private void cobVideoDevices_SelectedIndexChanged(object sender, EventArgs e) { CameraChanged(); }


        /// <summary>
        /// Creates new MicrophoneSettingsInfo object from the microphone block.
        /// </summary>
        /// <returns>The MicrophoneSettingsInfo object.</returns>
        private AudioSettingsInfo GetMicrophoneSettings()
        {
            DeviceInfo selectedDevice = cobMicrophoneDevice.SelectedItem as DeviceInfo;
            string deviceId = (selectedDevice != null) ? selectedDevice.DeviceID : "";

            return new AudioSettingsInfo(
                trckbMicrophoneVolumeControl.Value / 100.0f,
                chkbMicrophoneMute.Checked,
                null,
                deviceId);
        }

        /// <summary>
        /// Creates new SpeakerSettingsInfo object from the speaker block.
        /// </summary>
        /// <returns>The SpeakerSettingsInfo object.</returns>
        private AudioSettingsInfo GetSpeakerSettings()
        {
            DeviceInfo selectedDevice = cobSpeakerDevice.SelectedItem as DeviceInfo;
            string deviceId = (selectedDevice != null) ? selectedDevice.DeviceID : "";

            return new AudioSettingsInfo(
                trckbSpeakerVolumeControl.Value / 100.0f,
                chkbSpeakerMute.Checked,
                null,
                deviceId);
        }

        /// <summary>
        /// Emits request about the change of speaker settings.
        /// </summary>
        private void SpeakerChanged()
        {
            softphoneEngine.SpeakerSettingsHasChanged(GetSpeakerSettings());
        }

        /// <summary>
        /// Emits request about the change of microphone settings.
        /// </summary>
        private void MicrophoneChanged()
        {
            softphoneEngine.MicrophoneSettingsHasChanged(GetMicrophoneSettings());
        }

        /// <summary>
        /// Emits request about the change of camera settings.
        /// </summary>
        private void CameraChanged()
        {
            softphoneEngine.CameraSettingsHasChanged(GetCameraSetting());
        }



        /// <summary>
        /// Gets the playback settings
        /// </summary>
        public void GetPlaybackSettings()
        {
            AudioSettingsInfo info = softphoneEngine.GetPlaybackSettings();
            foreach (DeviceInfo obj in info.Devices)
                cobSpeakerDevice.Items.Add(obj);

            if (info.SelectedDevice != null)
            {
                foreach (DeviceInfo obj in info.Devices)
                {
                    if (obj.DeviceID == info.SelectedDevice)
                    {
                        cobSpeakerDevice.SelectedItem = obj;
                        break;
                    }
                }
            }
            else
            {
                if (cobSpeakerDevice.Items.Count > 0)
                    cobSpeakerDevice.SelectedIndex = 0;
            }

            cobSpeakerDevice.Update();
            chkbSpeakerMute.Checked = info.Mute;
            trckbSpeakerVolumeControl.Value = (int)(info.Volume * 100);
        }

        /// <summary>
        /// Updates the recording settings
        /// </summary>
        public void GetRecordingSettings()
        {
            AudioSettingsInfo info = softphoneEngine.GetRecordingSettings();
            foreach (DeviceInfo obj in info.Devices)
                cobMicrophoneDevice.Items.Add(obj);

            if (info.SelectedDevice != null)
            {
                foreach (DeviceInfo obj in info.Devices)
                {
                    if (obj.DeviceID == info.SelectedDevice)
                    {
                        cobMicrophoneDevice.SelectedItem = obj;
                        break;
                    }
                }
            }
            else
            {
                if (cobMicrophoneDevice.Items.Count > 0)
                    cobMicrophoneDevice.SelectedIndex = 0;
            }

            cobMicrophoneDevice.Update();
            chkbMicrophoneMute.Checked = info.Mute;
            trckbMicrophoneVolumeControl.Value = (int)(info.Volume * 100);
        }

        /// <summary>
        /// Updates the volume of the microphone
        /// </summary>
        /// <param name="volume"></param>
        public void UpdateMicrophoneLevel(double volume)
        {
            if (double.IsInfinity(volume))
                return;

            if (volume < pbMicrophoneLevel.Minimum)
                return;

            if (volume > pbMicrophoneLevel.Maximum)
                return;

            if (pbMicrophoneLevel.InvokeRequired)
            {
                pbMicrophoneLevel.BeginInvoke(new MethodInvoker(delegate
                    {
                        pbMicrophoneLevel.Value = (int)volume;
                    }));
            }
        }

        /// <summary>
        /// Updates the volume of the speaker
        /// </summary>
        /// <param name="volume"></param>
        public void UpdateSpeakerLevel(double volume)
        {
            if (double.IsInfinity(volume))
                return;

            if (volume < pbSpeakerLevel.Minimum)
                return;

            if (volume > pbSpeakerLevel.Maximum)
                return;

            if (pbSpeakerLevel.InvokeRequired)
            {
                pbSpeakerLevel.BeginInvoke(new MethodInvoker(delegate
                {
                    pbSpeakerLevel.Value = (int)volume;
                }));
            }
        }

        /// <summary>
        /// Gets camera settings
        /// </summary>
        public void GetCameraSettings()
        {
            VideoSettingsInfo info = softphoneEngine.GetCameraSettings();
            foreach (VideoDeviceInfo obj in info.Devices)

                cobVideoDevices.Items.Add(obj);

            if (info != null)
            {
                foreach (VideoDeviceInfo obj in info.Devices)
                {
                    if (obj.DeviceID == info.SelectedDevice)
                    {
                        cobVideoDevices.SelectedItem = obj;
                        UpdateCameraResolution(obj);
                        break;
                    }
                }
            }
            else
            {
                if (cobVideoDevices.Items.Count > 0)
                    cobVideoDevices.SelectedIndex = 0;
            }

            cobVideoDevices.Update();
            //chkbMicrophoneMute.Checked = info.Mute;
            //trckbMicrophoneVolumeControl.Value = (int)(info.Volume * 100);
        }

        private void UpdateCameraResolution(VideoDeviceInfo device)
        {
            if (device == null)
                return;
            cobVideoResolutions.Items.Clear();
            if (device.Capabilities != null)
            {
                foreach (VideoCapabilities resolution in device.Capabilities)
                {
                    cobVideoResolutions.Items.Add(resolution);
                }
                if (cobVideoResolutions.Items.Count > 0)
                    cobVideoResolutions.SelectedItem = device.Capabilities[0];

                cobVideoResolutions.Update();
            }
        }

        #endregion

        #region Message Summary

        /// <summary>
        /// Message Summary Information received.
        /// </summary>
        /// <param name="info">The phone line associated with the message summary info.</param>
        /// <param name="summary">The summary information itself.</param>
        public void MessageSummaryReceived(PhoneLineInfo info, VoIPMessageSummary summary)
        {
            if (summary == null)
            {
                MWILogic.Reset();
                return;
            }

            MWILogic.ChangeState(summary);
        }
        #endregion

        #region Video Actions

        private bool localVideoTest;

        private void btnVideoLocalTest_Click(object sender, EventArgs e)
        {
            localVideoTest = !localVideoTest;

            // update gui
            if (localVideoTest)
            {
                currentState.CameraTestStateChanged(true);
                btnVideoLocalTest.Text = "Stop test";

                softphoneEngine.EnableVideoLocalTestMode();
                videoViewerLocal.Start();
            }
            else
            {
               // btnStartVideo.Enabled = true;
                //btnStopVideo.Enabled = true;
                currentState.CameraTestStateChanged(false);
                btnVideoLocalTest.Text = "Local test";

                softphoneEngine.DisableVideoLocalTestMode();
                videoViewerLocal.Stop();
            }
        }


        private void btnStartVideo_Click(object sender, EventArgs e)
        {
            PhoneCallInfo callInfo = lbPhoneCalls.SelectedItem as PhoneCallInfo;
         
            softphoneEngine.StartCamera(callInfo);
            videoViewerLocal.Start();
            videoViewerRemote.Start();
            btnStartVideo.Enabled = false;
            btnStopVideo.Enabled = true;
            btnVideoLocalTest.Enabled = false;
        }

        private void btnStopVideo_Click(object sender, EventArgs e)
        {
            PhoneCallInfo callInfo = lbPhoneCalls.SelectedItem as PhoneCallInfo;
            
            softphoneEngine.StopCamera(callInfo);
            videoViewerLocal.Stop();
            videoViewerRemote.Stop();

            btnStartVideo.Enabled = true;
            btnStopVideo.Enabled = false;
            btnVideoLocalTest.Enabled = true;
        }


        internal void VideoButtonsToNormalState()
        {
            btnVideoLocalTest.Enabled = true;
            btnStartVideo.Enabled = false;
            btnStopVideo.Enabled = false;
        }

        /// <summary>
        /// Creates new VideoSettingsInfo object from the microphone block.
        /// </summary>
        /// <returns>The VideoSettingsInfo object.</returns>
        private VideoSettingsInfo GetCameraSetting()
        {
            VideoDeviceInfo selectedDevice = cobVideoDevices.SelectedItem as VideoDeviceInfo;
            int deviceId = (selectedDevice != null) ? selectedDevice.DeviceID : -1;

            Resolution resolution = new Resolution(320, 240);
            if (cobVideoResolutions.Items.Count > 0 && cobVideoResolutions.SelectedIndex != -1)
            {
                VideoCapabilities capabilities = cobVideoResolutions.SelectedItem as VideoCapabilities;

                if (capabilities != null)
                {
                    resolution = capabilities.Resolution;
                }
            }

            return new VideoSettingsInfo(resolution, null, deviceId);
        }


        #endregion

        private void btnRingtone_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "Wave Files (*.wav)|*.wav";
            if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            tbRingtoneName.Text = ofd.FileName;

            softphoneEngine.SetRingtone(ofd.FileName);
        }

        private void btnRigbackTone_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "Wave Files (*.wav)|*.wav";
            if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            tbRingbackTone.Text = ofd.FileName;

            softphoneEngine.SetRingback(ofd.FileName);
        }

        private void OpenUrl(string url)
        {
            try
            {
                LogEvent("Open: " + url);
                Process.Start(url);
            }
            catch (Exception e)
            {
                LogEvent("Open: " + e.Message);
            }
        }


        private string path;
        private string Path
        {
            get
            {
                if (string.IsNullOrEmpty(path))
                {
                    try
                    {
                        path =
                            Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("Ozeki").OpenSubKey("VOIP SIP SDK").
                                GetValue("PATH").ToString();
                    }
                    catch
                    {
                        path = Application.StartupPath;
                    }
                }

                return path;
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            OpenUrl("http://www.voip-sip-sdk.com");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var myDocuments = System.IO.Path.Combine(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Ozeki"), "Ozeki VoIP SDK");

            //OpenUrl("file://" + Path + "\\projects.html");
            OpenUrl(myDocuments);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenUrl("file://" + Path + "Documentation\\SDKHelp\\index.html");
        }

        private void chkForward_CheckedChanged(object sender, EventArgs e)
        {
            if (chkForward.CheckState==CheckState.Checked)
            {
                forwardFrom = new ForwardCallForm(softphoneEngine);
                forwardFrom.Closed += new EventHandler(forwadFrom_Closed);
                forwardFrom.ShowDialog(this);
            }
            else
            {
                softphoneEngine.Forwarding = false;
            }
        }

        void forwadFrom_Closed(object sender, EventArgs e)
        {
            if (forwardFrom != null)
            {
                forwardFrom.Closed -= forwadFrom_Closed;
                forwardFrom = null;
            }
            chkForward.CheckState = softphoneEngine.Forwarding ? CheckState.Checked : CheckState.Unchecked;
        }

        TransferCallForm transferForm;
        void btnTransfer_Click(object sender, EventArgs e)
        {
            transferForm = new TransferCallForm(softphoneEngine.PhoneCalls.Where(callInfo => callInfo != SelectedCall).ToList());
            transferForm.Closed += transferForm_Closed;
            transferForm.ShowDialog(this);
        }

        void transferForm_Closed(object sender, EventArgs e)
        {
            switch (transferForm.TransferMode)
            {
                case TransferMode.Blind:
                    if(!string.IsNullOrEmpty(transferForm.BlindTransferTarget))
                        softphoneEngine.BlindTransfer(SelectedCall, transferForm.BlindTransferTarget);
                    break;
                case TransferMode.Attended:
                    if (SelectedCall != transferForm.AttendedTransferTarget)
                        softphoneEngine.AttendedTransfer(SelectedCall, transferForm.AttendedTransferTarget);
                    break;
            }


            transferForm = null;
        }

        private void cobSRTPmode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedLine!=null)
            {
                var srtpMode = (SRTPMode)Enum.Parse(typeof(SRTPMode), cobSRTPmode.Text, true);

                SelectedLine.SrtpMode = srtpMode;
                softphoneEngine.ChangeSRTPSettings(SelectedLine);
            }
        }
    }
}