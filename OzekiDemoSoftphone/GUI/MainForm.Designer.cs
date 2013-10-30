namespace OzekiDemoSoftphone.GUI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tcNetworkSettings = new System.Windows.Forms.TabControl();
            this.tpSipSettings = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.remotevideopanel = new System.Windows.Forms.Panel();
            this.videoViewerRemote = new Ozeki.Media.Video.Controls.VideoViewerWF();
            this.grpbVideo = new System.Windows.Forms.GroupBox();
            this.lVideoResolutions = new System.Windows.Forms.Label();
            this.cobVideoResolutions = new System.Windows.Forms.ComboBox();
            this.btnStopVideo = new System.Windows.Forms.Button();
            this.btnStartVideo = new System.Windows.Forms.Button();
            this.lVideoDevices = new System.Windows.Forms.Label();
            this.cobVideoDevices = new System.Windows.Forms.ComboBox();
            this.videoPanel = new System.Windows.Forms.Panel();
            this.videoViewerLocal = new Ozeki.Media.Video.Controls.VideoViewerWF();
            this.btnVideoLocalTest = new System.Windows.Forms.Button();
            this.grpbMicrophone = new System.Windows.Forms.GroupBox();
            this.chkbMicrophoneMute = new System.Windows.Forms.CheckBox();
            this.cobMicrophoneDevice = new System.Windows.Forms.ComboBox();
            this.lblMicrophoneDevice = new System.Windows.Forms.Label();
            this.lblMicrophoneVolume = new System.Windows.Forms.Label();
            this.trckbMicrophoneVolumeControl = new System.Windows.Forms.TrackBar();
            this.pbMicrophoneLevel = new System.Windows.Forms.ProgressBar();
            this.grpbRecordFile = new System.Windows.Forms.GroupBox();
            this.btnRecordFileStop = new System.Windows.Forms.Button();
            this.btnRecordFileStart = new System.Windows.Forms.Button();
            this.btnRecordFileBrowse = new System.Windows.Forms.Button();
            this.tbRecordFile = new System.Windows.Forms.TextBox();
            this.lblRecordFile = new System.Windows.Forms.Label();
            this.grpbSpeaker = new System.Windows.Forms.GroupBox();
            this.cobSpeakerDevice = new System.Windows.Forms.ComboBox();
            this.lblSpeakerDevice = new System.Windows.Forms.Label();
            this.chkbSpeakerMute = new System.Windows.Forms.CheckBox();
            this.lblSpeakerVolume = new System.Windows.Forms.Label();
            this.trckbSpeakerVolumeControl = new System.Windows.Forms.TrackBar();
            this.pbSpeakerLevel = new System.Windows.Forms.ProgressBar();
            this.grpbPlayFile = new System.Windows.Forms.GroupBox();
            this.btnPlayFileStart = new System.Windows.Forms.Button();
            this.btnPlayFileStop = new System.Windows.Forms.Button();
            this.tbPlayFile = new System.Windows.Forms.TextBox();
            this.btnPlayFileBrowse = new System.Windows.Forms.Button();
            this.lblPalyFile = new System.Windows.Forms.Label();
            this.grpbSipPhone = new System.Windows.Forms.GroupBox();
            this.chkForward = new System.Windows.Forms.CheckBox();
            this.lblMessageWaitingIndication = new System.Windows.Forms.Label();
            this.chkbAutoAnswer = new System.Windows.Forms.CheckBox();
            this.grpPhoneLines = new System.Windows.Forms.GroupBox();
            this.btnPhoneLine6 = new System.Windows.Forms.Button();
            this.btnPhoneLine5 = new System.Windows.Forms.Button();
            this.btnPhoneLine4 = new System.Windows.Forms.Button();
            this.btnPhoneLine3 = new System.Windows.Forms.Button();
            this.btnPhoneLine2 = new System.Windows.Forms.Button();
            this.btnPhoneLine1 = new System.Windows.Forms.Button();
            this.btnRedial = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbCallHistory = new System.Windows.Forms.ListBox();
            this.btnHold = new System.Windows.Forms.Button();
            this.chbDND = new System.Windows.Forms.CheckBox();
            this.chbxToolTip = new System.Windows.Forms.CheckBox();
            this.tbPhoneStatus = new System.Windows.Forms.TextBox();
            this.lbPhoneCalls = new System.Windows.Forms.ListBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbPhoneLines = new System.Windows.Forms.ListBox();
            this.btnPager = new System.Windows.Forms.Button();
            this.btnTransfer = new System.Windows.Forms.Button();
            this.btnHangUp = new System.Windows.Forms.Button();
            this.btnPickUp = new System.Windows.Forms.Button();
            this.tbDialNumber = new System.Windows.Forms.TextBox();
            this.btnKeypad8 = new System.Windows.Forms.Button();
            this.btnKeypad6 = new System.Windows.Forms.Button();
            this.btnKeypad5 = new System.Windows.Forms.Button();
            this.btnKeypad9 = new System.Windows.Forms.Button();
            this.btnKeypadSharp = new System.Windows.Forms.Button();
            this.btnKeypadAsterisk = new System.Windows.Forms.Button();
            this.btnKeypad7 = new System.Windows.Forms.Button();
            this.btnKeypad0 = new System.Windows.Forms.Button();
            this.btnKeypad3 = new System.Windows.Forms.Button();
            this.btnKeypad4 = new System.Windows.Forms.Button();
            this.btnKeypad2 = new System.Windows.Forms.Button();
            this.btnKeypad1 = new System.Windows.Forms.Button();
            this.tpNatSettings = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnAdapterApply = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cobAdaptersIP = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cobSRTPmode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbUserId = new System.Windows.Forms.TextBox();
            this.tbProxy = new System.Windows.Forms.TextBox();
            this.lblUserId = new System.Windows.Forms.Label();
            this.lblOutboundProxy = new System.Windows.Forms.Label();
            this.lblDisplayName = new System.Windows.Forms.Label();
            this.chkbATAMode = new System.Windows.Forms.CheckBox();
            this.tbDisplayName = new System.Windows.Forms.TextBox();
            this.chkbRegistrationRequired = new System.Windows.Forms.CheckBox();
            this.lblRegisterName = new System.Windows.Forms.Label();
            this.btnSIPUnregister = new System.Windows.Forms.Button();
            this.tbRegisterName = new System.Windows.Forms.TextBox();
            this.btnSIPRegister = new System.Windows.Forms.Button();
            this.lblRegisterPassword = new System.Windows.Forms.Label();
            this.lblTransport = new System.Windows.Forms.Label();
            this.tbRegisterPassword = new System.Windows.Forms.TextBox();
            this.cobTransport = new System.Windows.Forms.ComboBox();
            this.tbDomainServer = new System.Windows.Forms.TextBox();
            this.chkbEnableLogging = new System.Windows.Forms.CheckBox();
            this.lblDomainName = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cobNatType = new System.Windows.Forms.ComboBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.lblNatServer = new System.Windows.Forms.Label();
            this.tbNatServer = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.lblNatUsername = new System.Windows.Forms.Label();
            this.tbNatPassword = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbNatUserName = new System.Windows.Forms.TextBox();
            this.grpbDSPs = new System.Windows.Forms.GroupBox();
            this.lMaxAGCGain = new System.Windows.Forms.Label();
            this.numMaxAGCGain = new System.Windows.Forms.NumericUpDown();
            this.numAECDelay = new System.Windows.Forms.NumericUpDown();
            this.lDelay = new System.Windows.Forms.Label();
            this.chkbDenoise = new System.Windows.Forms.CheckBox();
            this.chkbAGC = new System.Windows.Forms.CheckBox();
            this.chkbAEC = new System.Windows.Forms.CheckBox();
            this.grpbCodecs = new System.Windows.Forms.GroupBox();
            this.btnCodecsSelectAll = new System.Windows.Forms.Button();
            this.btnCodecsDeselectAll = new System.Windows.Forms.Button();
            this.lbCodecs = new System.Windows.Forms.ListBox();
            this.gpRingback = new System.Windows.Forms.GroupBox();
            this.tbRingbackTone = new System.Windows.Forms.TextBox();
            this.lblRingBack = new System.Windows.Forms.Label();
            this.btnRingbackTone = new System.Windows.Forms.Button();
            this.grpbKeepAlive = new System.Windows.Forms.GroupBox();
            this.chbKeepAliveDisable = new System.Windows.Forms.CheckBox();
            this.nudKeepAlive = new System.Windows.Forms.NumericUpDown();
            this.lblKeepAliveInterval = new System.Windows.Forms.Label();
            this.gpRingtone = new System.Windows.Forms.GroupBox();
            this.tbRingtoneName = new System.Windows.Forms.TextBox();
            this.lblRingtone = new System.Windows.Forms.Label();
            this.btnRingtone = new System.Windows.Forms.Button();
            this.tpSIPMessages = new System.Windows.Forms.TabPage();
            this.rtbSIPMeassages = new System.Windows.Forms.RichTextBox();
            this.tpEvents = new System.Windows.Forms.TabPage();
            this.rtbEvents = new System.Windows.Forms.RichTextBox();
            this.btnAbout = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.tcNetworkSettings.SuspendLayout();
            this.tpSipSettings.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.remotevideopanel.SuspendLayout();
            this.grpbVideo.SuspendLayout();
            this.videoPanel.SuspendLayout();
            this.grpbMicrophone.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trckbMicrophoneVolumeControl)).BeginInit();
            this.grpbRecordFile.SuspendLayout();
            this.grpbSpeaker.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trckbSpeakerVolumeControl)).BeginInit();
            this.grpbPlayFile.SuspendLayout();
            this.grpbSipPhone.SuspendLayout();
            this.grpPhoneLines.SuspendLayout();
            this.tpNatSettings.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpbDSPs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxAGCGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAECDelay)).BeginInit();
            this.grpbCodecs.SuspendLayout();
            this.gpRingback.SuspendLayout();
            this.grpbKeepAlive.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudKeepAlive)).BeginInit();
            this.gpRingtone.SuspendLayout();
            this.tpSIPMessages.SuspendLayout();
            this.tpEvents.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcNetworkSettings
            // 
            this.tcNetworkSettings.Controls.Add(this.tpSipSettings);
            this.tcNetworkSettings.Controls.Add(this.tpNatSettings);
            this.tcNetworkSettings.Controls.Add(this.tpSIPMessages);
            this.tcNetworkSettings.Controls.Add(this.tpEvents);
            this.tcNetworkSettings.Location = new System.Drawing.Point(12, 40);
            this.tcNetworkSettings.Name = "tcNetworkSettings";
            this.tcNetworkSettings.SelectedIndex = 0;
            this.tcNetworkSettings.Size = new System.Drawing.Size(1002, 501);
            this.tcNetworkSettings.TabIndex = 0;
            // 
            // tpSipSettings
            // 
            this.tpSipSettings.BackColor = System.Drawing.SystemColors.Control;
            this.tpSipSettings.Controls.Add(this.groupBox3);
            this.tpSipSettings.Controls.Add(this.grpbVideo);
            this.tpSipSettings.Controls.Add(this.grpbMicrophone);
            this.tpSipSettings.Controls.Add(this.grpbRecordFile);
            this.tpSipSettings.Controls.Add(this.grpbSpeaker);
            this.tpSipSettings.Controls.Add(this.grpbPlayFile);
            this.tpSipSettings.Controls.Add(this.grpbSipPhone);
            this.tpSipSettings.Location = new System.Drawing.Point(4, 22);
            this.tpSipSettings.Name = "tpSipSettings";
            this.tpSipSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpSipSettings.Size = new System.Drawing.Size(994, 475);
            this.tpSipSettings.TabIndex = 0;
            this.tpSipSettings.Text = "SoftPhone";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.remotevideopanel);
            this.groupBox3.Location = new System.Drawing.Point(685, 7);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(305, 193);
            this.groupBox3.TabIndex = 36;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Remote Video";
            // 
            // remotevideopanel
            // 
            this.remotevideopanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.remotevideopanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.remotevideopanel.Controls.Add(this.videoViewerRemote);
            this.remotevideopanel.Location = new System.Drawing.Point(6, 19);
            this.remotevideopanel.Name = "remotevideopanel";
            this.remotevideopanel.Size = new System.Drawing.Size(294, 168);
            this.remotevideopanel.TabIndex = 0;
            // 
            // videoViewerRemote
            // 
            this.videoViewerRemote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videoViewerRemote.FlipMode = Ozeki.Media.Video.Imaging.FlipMode.None;
            this.videoViewerRemote.Location = new System.Drawing.Point(0, 0);
            this.videoViewerRemote.Name = "videoViewerRemote";
            this.videoViewerRemote.RotateAngle = 0;
            this.videoViewerRemote.Size = new System.Drawing.Size(292, 166);
            this.videoViewerRemote.TabIndex = 0;
            this.videoViewerRemote.Text = "videoViewerWF1";
            // 
            // grpbVideo
            // 
            this.grpbVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpbVideo.Controls.Add(this.lVideoResolutions);
            this.grpbVideo.Controls.Add(this.cobVideoResolutions);
            this.grpbVideo.Controls.Add(this.btnStopVideo);
            this.grpbVideo.Controls.Add(this.btnStartVideo);
            this.grpbVideo.Controls.Add(this.lVideoDevices);
            this.grpbVideo.Controls.Add(this.cobVideoDevices);
            this.grpbVideo.Controls.Add(this.videoPanel);
            this.grpbVideo.Controls.Add(this.btnVideoLocalTest);
            this.grpbVideo.Location = new System.Drawing.Point(685, 202);
            this.grpbVideo.Name = "grpbVideo";
            this.grpbVideo.Size = new System.Drawing.Size(306, 267);
            this.grpbVideo.TabIndex = 4;
            this.grpbVideo.TabStop = false;
            this.grpbVideo.Text = "Local Video";
            // 
            // lVideoResolutions
            // 
            this.lVideoResolutions.AutoSize = true;
            this.lVideoResolutions.Location = new System.Drawing.Point(6, 48);
            this.lVideoResolutions.Name = "lVideoResolutions";
            this.lVideoResolutions.Size = new System.Drawing.Size(60, 13);
            this.lVideoResolutions.TabIndex = 13;
            this.lVideoResolutions.Text = "Resolution:";
            // 
            // cobVideoResolutions
            // 
            this.cobVideoResolutions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobVideoResolutions.FormattingEnabled = true;
            this.cobVideoResolutions.Location = new System.Drawing.Point(76, 46);
            this.cobVideoResolutions.Name = "cobVideoResolutions";
            this.cobVideoResolutions.Size = new System.Drawing.Size(224, 21);
            this.cobVideoResolutions.TabIndex = 12;
            this.cobVideoResolutions.SelectedIndexChanged += new System.EventHandler(this.cobVideoResolutions_SelectedIndexChanged);
            // 
            // btnStopVideo
            // 
            this.btnStopVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStopVideo.Location = new System.Drawing.Point(88, 238);
            this.btnStopVideo.Name = "btnStopVideo";
            this.btnStopVideo.Size = new System.Drawing.Size(75, 23);
            this.btnStopVideo.TabIndex = 11;
            this.btnStopVideo.Text = "Stop video";
            this.btnStopVideo.UseVisualStyleBackColor = true;
            this.btnStopVideo.Click += new System.EventHandler(this.btnStopVideo_Click);
            // 
            // btnStartVideo
            // 
            this.btnStartVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStartVideo.Location = new System.Drawing.Point(7, 238);
            this.btnStartVideo.Name = "btnStartVideo";
            this.btnStartVideo.Size = new System.Drawing.Size(75, 23);
            this.btnStartVideo.TabIndex = 10;
            this.btnStartVideo.Text = "Send video";
            this.btnStartVideo.UseVisualStyleBackColor = true;
            this.btnStartVideo.Click += new System.EventHandler(this.btnStartVideo_Click);
            // 
            // lVideoDevices
            // 
            this.lVideoDevices.AutoSize = true;
            this.lVideoDevices.Location = new System.Drawing.Point(17, 22);
            this.lVideoDevices.Name = "lVideoDevices";
            this.lVideoDevices.Size = new System.Drawing.Size(49, 13);
            this.lVideoDevices.TabIndex = 9;
            this.lVideoDevices.Text = "Devices:";
            // 
            // cobVideoDevices
            // 
            this.cobVideoDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobVideoDevices.FormattingEnabled = true;
            this.cobVideoDevices.Location = new System.Drawing.Point(76, 19);
            this.cobVideoDevices.Name = "cobVideoDevices";
            this.cobVideoDevices.Size = new System.Drawing.Size(224, 21);
            this.cobVideoDevices.TabIndex = 7;
            this.cobVideoDevices.SelectedIndexChanged += new System.EventHandler(this.cobVideoDevices_SelectedIndexChanged);
            // 
            // videoPanel
            // 
            this.videoPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.videoPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.videoPanel.Controls.Add(this.videoViewerLocal);
            this.videoPanel.Location = new System.Drawing.Point(6, 73);
            this.videoPanel.Name = "videoPanel";
            this.videoPanel.Size = new System.Drawing.Size(294, 159);
            this.videoPanel.TabIndex = 6;
            // 
            // videoViewerLocal
            // 
            this.videoViewerLocal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videoViewerLocal.FlipMode = Ozeki.Media.Video.Imaging.FlipMode.None;
            this.videoViewerLocal.Location = new System.Drawing.Point(0, 0);
            this.videoViewerLocal.Name = "videoViewerLocal";
            this.videoViewerLocal.RotateAngle = 0;
            this.videoViewerLocal.Size = new System.Drawing.Size(292, 157);
            this.videoViewerLocal.TabIndex = 3;
            this.videoViewerLocal.Text = "videoImageWF1";
            // 
            // btnVideoLocalTest
            // 
            this.btnVideoLocalTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVideoLocalTest.Location = new System.Drawing.Point(224, 238);
            this.btnVideoLocalTest.Name = "btnVideoLocalTest";
            this.btnVideoLocalTest.Size = new System.Drawing.Size(75, 23);
            this.btnVideoLocalTest.TabIndex = 2;
            this.btnVideoLocalTest.Text = "Local test";
            this.btnVideoLocalTest.UseVisualStyleBackColor = true;
            this.btnVideoLocalTest.Click += new System.EventHandler(this.btnVideoLocalTest_Click);
            // 
            // grpbMicrophone
            // 
            this.grpbMicrophone.Controls.Add(this.chkbMicrophoneMute);
            this.grpbMicrophone.Controls.Add(this.cobMicrophoneDevice);
            this.grpbMicrophone.Controls.Add(this.lblMicrophoneDevice);
            this.grpbMicrophone.Controls.Add(this.lblMicrophoneVolume);
            this.grpbMicrophone.Controls.Add(this.trckbMicrophoneVolumeControl);
            this.grpbMicrophone.Controls.Add(this.pbMicrophoneLevel);
            this.grpbMicrophone.Location = new System.Drawing.Point(344, 246);
            this.grpbMicrophone.Name = "grpbMicrophone";
            this.grpbMicrophone.Size = new System.Drawing.Size(335, 131);
            this.grpbMicrophone.TabIndex = 32;
            this.grpbMicrophone.TabStop = false;
            this.grpbMicrophone.Text = "Microphone volume control";
            // 
            // chkbMicrophoneMute
            // 
            this.chkbMicrophoneMute.AutoSize = true;
            this.chkbMicrophoneMute.Location = new System.Drawing.Point(272, 33);
            this.chkbMicrophoneMute.Name = "chkbMicrophoneMute";
            this.chkbMicrophoneMute.Size = new System.Drawing.Size(50, 17);
            this.chkbMicrophoneMute.TabIndex = 2;
            this.chkbMicrophoneMute.Text = "Mute";
            this.chkbMicrophoneMute.UseVisualStyleBackColor = true;
            this.chkbMicrophoneMute.CheckedChanged += new System.EventHandler(this.chkbMicrophoneMute_CheckedChanged);
            // 
            // cobMicrophoneDevice
            // 
            this.cobMicrophoneDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobMicrophoneDevice.FormattingEnabled = true;
            this.cobMicrophoneDevice.Location = new System.Drawing.Point(56, 70);
            this.cobMicrophoneDevice.Name = "cobMicrophoneDevice";
            this.cobMicrophoneDevice.Size = new System.Drawing.Size(273, 21);
            this.cobMicrophoneDevice.TabIndex = 3;
            this.cobMicrophoneDevice.SelectedIndexChanged += new System.EventHandler(this.cobMicrophoneDevice_SelectedIndexChanged);
            // 
            // lblMicrophoneDevice
            // 
            this.lblMicrophoneDevice.AutoSize = true;
            this.lblMicrophoneDevice.Location = new System.Drawing.Point(6, 73);
            this.lblMicrophoneDevice.Name = "lblMicrophoneDevice";
            this.lblMicrophoneDevice.Size = new System.Drawing.Size(44, 13);
            this.lblMicrophoneDevice.TabIndex = 3;
            this.lblMicrophoneDevice.Text = "Device:";
            // 
            // lblMicrophoneVolume
            // 
            this.lblMicrophoneVolume.AutoSize = true;
            this.lblMicrophoneVolume.Location = new System.Drawing.Point(6, 32);
            this.lblMicrophoneVolume.Name = "lblMicrophoneVolume";
            this.lblMicrophoneVolume.Size = new System.Drawing.Size(45, 13);
            this.lblMicrophoneVolume.TabIndex = 2;
            this.lblMicrophoneVolume.Text = "Volume:";
            // 
            // trckbMicrophoneVolumeControl
            // 
            this.trckbMicrophoneVolumeControl.LargeChange = 10;
            this.trckbMicrophoneVolumeControl.Location = new System.Drawing.Point(57, 19);
            this.trckbMicrophoneVolumeControl.Maximum = 100;
            this.trckbMicrophoneVolumeControl.Name = "trckbMicrophoneVolumeControl";
            this.trckbMicrophoneVolumeControl.Size = new System.Drawing.Size(209, 45);
            this.trckbMicrophoneVolumeControl.SmallChange = 5;
            this.trckbMicrophoneVolumeControl.TabIndex = 1;
            this.trckbMicrophoneVolumeControl.TickFrequency = 10;
            this.trckbMicrophoneVolumeControl.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trckbMicrophoneVolumeControl.Value = 80;
            this.trckbMicrophoneVolumeControl.Scroll += new System.EventHandler(this.trckbMicrophoneVolumeControll_Scroll);
            // 
            // pbMicrophoneLevel
            // 
            this.pbMicrophoneLevel.Enabled = false;
            this.pbMicrophoneLevel.Location = new System.Drawing.Point(6, 97);
            this.pbMicrophoneLevel.Name = "pbMicrophoneLevel";
            this.pbMicrophoneLevel.Size = new System.Drawing.Size(323, 23);
            this.pbMicrophoneLevel.TabIndex = 4;
            // 
            // grpbRecordFile
            // 
            this.grpbRecordFile.Controls.Add(this.btnRecordFileStop);
            this.grpbRecordFile.Controls.Add(this.btnRecordFileStart);
            this.grpbRecordFile.Controls.Add(this.btnRecordFileBrowse);
            this.grpbRecordFile.Controls.Add(this.tbRecordFile);
            this.grpbRecordFile.Controls.Add(this.lblRecordFile);
            this.grpbRecordFile.Location = new System.Drawing.Point(345, 388);
            this.grpbRecordFile.Name = "grpbRecordFile";
            this.grpbRecordFile.Size = new System.Drawing.Size(334, 75);
            this.grpbRecordFile.TabIndex = 35;
            this.grpbRecordFile.TabStop = false;
            this.grpbRecordFile.Text = "Record audio to file";
            // 
            // btnRecordFileStop
            // 
            this.btnRecordFileStop.Enabled = false;
            this.btnRecordFileStop.Location = new System.Drawing.Point(253, 45);
            this.btnRecordFileStop.Name = "btnRecordFileStop";
            this.btnRecordFileStop.Size = new System.Drawing.Size(75, 23);
            this.btnRecordFileStop.TabIndex = 4;
            this.btnRecordFileStop.Text = "Stop";
            this.btnRecordFileStop.UseVisualStyleBackColor = true;
            this.btnRecordFileStop.Click += new System.EventHandler(this.btnRecordFileStop_Click);
            // 
            // btnRecordFileStart
            // 
            this.btnRecordFileStart.Enabled = false;
            this.btnRecordFileStart.Location = new System.Drawing.Point(172, 45);
            this.btnRecordFileStart.Name = "btnRecordFileStart";
            this.btnRecordFileStart.Size = new System.Drawing.Size(75, 23);
            this.btnRecordFileStart.TabIndex = 3;
            this.btnRecordFileStart.Text = "Record";
            this.btnRecordFileStart.UseVisualStyleBackColor = true;
            this.btnRecordFileStart.Click += new System.EventHandler(this.btnRecordFileStart_Click);
            // 
            // btnRecordFileBrowse
            // 
            this.btnRecordFileBrowse.Location = new System.Drawing.Point(298, 17);
            this.btnRecordFileBrowse.Name = "btnRecordFileBrowse";
            this.btnRecordFileBrowse.Size = new System.Drawing.Size(30, 23);
            this.btnRecordFileBrowse.TabIndex = 2;
            this.btnRecordFileBrowse.Text = "...";
            this.btnRecordFileBrowse.UseVisualStyleBackColor = true;
            this.btnRecordFileBrowse.Click += new System.EventHandler(this.btnRecordFileBrowse_Click);
            // 
            // tbRecordFile
            // 
            this.tbRecordFile.Location = new System.Drawing.Point(38, 19);
            this.tbRecordFile.Name = "tbRecordFile";
            this.tbRecordFile.ReadOnly = true;
            this.tbRecordFile.Size = new System.Drawing.Size(254, 20);
            this.tbRecordFile.TabIndex = 1;
            // 
            // lblRecordFile
            // 
            this.lblRecordFile.AutoSize = true;
            this.lblRecordFile.Location = new System.Drawing.Point(6, 22);
            this.lblRecordFile.Name = "lblRecordFile";
            this.lblRecordFile.Size = new System.Drawing.Size(26, 13);
            this.lblRecordFile.TabIndex = 0;
            this.lblRecordFile.Text = "File:";
            // 
            // grpbSpeaker
            // 
            this.grpbSpeaker.Controls.Add(this.cobSpeakerDevice);
            this.grpbSpeaker.Controls.Add(this.lblSpeakerDevice);
            this.grpbSpeaker.Controls.Add(this.chkbSpeakerMute);
            this.grpbSpeaker.Controls.Add(this.lblSpeakerVolume);
            this.grpbSpeaker.Controls.Add(this.trckbSpeakerVolumeControl);
            this.grpbSpeaker.Controls.Add(this.pbSpeakerLevel);
            this.grpbSpeaker.Location = new System.Drawing.Point(3, 246);
            this.grpbSpeaker.Name = "grpbSpeaker";
            this.grpbSpeaker.Size = new System.Drawing.Size(335, 131);
            this.grpbSpeaker.TabIndex = 31;
            this.grpbSpeaker.TabStop = false;
            this.grpbSpeaker.Text = "Speaker volume control";
            // 
            // cobSpeakerDevice
            // 
            this.cobSpeakerDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobSpeakerDevice.FormattingEnabled = true;
            this.cobSpeakerDevice.Location = new System.Drawing.Point(57, 70);
            this.cobSpeakerDevice.Name = "cobSpeakerDevice";
            this.cobSpeakerDevice.Size = new System.Drawing.Size(272, 21);
            this.cobSpeakerDevice.TabIndex = 3;
            this.cobSpeakerDevice.SelectedIndexChanged += new System.EventHandler(this.cobSpeakerDevice_SelectedIndexChanged);
            // 
            // lblSpeakerDevice
            // 
            this.lblSpeakerDevice.AutoSize = true;
            this.lblSpeakerDevice.Location = new System.Drawing.Point(6, 73);
            this.lblSpeakerDevice.Name = "lblSpeakerDevice";
            this.lblSpeakerDevice.Size = new System.Drawing.Size(44, 13);
            this.lblSpeakerDevice.TabIndex = 4;
            this.lblSpeakerDevice.Text = "Device:";
            // 
            // chkbSpeakerMute
            // 
            this.chkbSpeakerMute.AutoSize = true;
            this.chkbSpeakerMute.Location = new System.Drawing.Point(279, 31);
            this.chkbSpeakerMute.Name = "chkbSpeakerMute";
            this.chkbSpeakerMute.Size = new System.Drawing.Size(50, 17);
            this.chkbSpeakerMute.TabIndex = 2;
            this.chkbSpeakerMute.Text = "Mute";
            this.chkbSpeakerMute.UseVisualStyleBackColor = true;
            this.chkbSpeakerMute.CheckedChanged += new System.EventHandler(this.chkbSpeakerMute_CheckedChanged);
            // 
            // lblSpeakerVolume
            // 
            this.lblSpeakerVolume.AutoSize = true;
            this.lblSpeakerVolume.Location = new System.Drawing.Point(6, 32);
            this.lblSpeakerVolume.Name = "lblSpeakerVolume";
            this.lblSpeakerVolume.Size = new System.Drawing.Size(45, 13);
            this.lblSpeakerVolume.TabIndex = 2;
            this.lblSpeakerVolume.Text = "Volume:";
            // 
            // trckbSpeakerVolumeControl
            // 
            this.trckbSpeakerVolumeControl.LargeChange = 10;
            this.trckbSpeakerVolumeControl.Location = new System.Drawing.Point(57, 19);
            this.trckbSpeakerVolumeControl.Maximum = 100;
            this.trckbSpeakerVolumeControl.Name = "trckbSpeakerVolumeControl";
            this.trckbSpeakerVolumeControl.Size = new System.Drawing.Size(216, 45);
            this.trckbSpeakerVolumeControl.SmallChange = 5;
            this.trckbSpeakerVolumeControl.TabIndex = 1;
            this.trckbSpeakerVolumeControl.TickFrequency = 10;
            this.trckbSpeakerVolumeControl.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trckbSpeakerVolumeControl.Value = 80;
            this.trckbSpeakerVolumeControl.Scroll += new System.EventHandler(this.trckbSpeakerVolumeControll_Scroll);
            // 
            // pbSpeakerLevel
            // 
            this.pbSpeakerLevel.Enabled = false;
            this.pbSpeakerLevel.Location = new System.Drawing.Point(6, 97);
            this.pbSpeakerLevel.Name = "pbSpeakerLevel";
            this.pbSpeakerLevel.Size = new System.Drawing.Size(323, 23);
            this.pbSpeakerLevel.TabIndex = 4;
            // 
            // grpbPlayFile
            // 
            this.grpbPlayFile.Controls.Add(this.btnPlayFileStart);
            this.grpbPlayFile.Controls.Add(this.btnPlayFileStop);
            this.grpbPlayFile.Controls.Add(this.tbPlayFile);
            this.grpbPlayFile.Controls.Add(this.btnPlayFileBrowse);
            this.grpbPlayFile.Controls.Add(this.lblPalyFile);
            this.grpbPlayFile.Location = new System.Drawing.Point(3, 388);
            this.grpbPlayFile.Name = "grpbPlayFile";
            this.grpbPlayFile.Size = new System.Drawing.Size(335, 75);
            this.grpbPlayFile.TabIndex = 34;
            this.grpbPlayFile.TabStop = false;
            this.grpbPlayFile.Text = "Play audio file into a VoIP call";
            // 
            // btnPlayFileStart
            // 
            this.btnPlayFileStart.Enabled = false;
            this.btnPlayFileStart.Location = new System.Drawing.Point(172, 45);
            this.btnPlayFileStart.Name = "btnPlayFileStart";
            this.btnPlayFileStart.Size = new System.Drawing.Size(75, 23);
            this.btnPlayFileStart.TabIndex = 3;
            this.btnPlayFileStart.Text = "Play";
            this.btnPlayFileStart.UseVisualStyleBackColor = true;
            this.btnPlayFileStart.Click += new System.EventHandler(this.btnPlayFileStart_Click);
            // 
            // btnPlayFileStop
            // 
            this.btnPlayFileStop.Enabled = false;
            this.btnPlayFileStop.Location = new System.Drawing.Point(253, 45);
            this.btnPlayFileStop.Name = "btnPlayFileStop";
            this.btnPlayFileStop.Size = new System.Drawing.Size(75, 23);
            this.btnPlayFileStop.TabIndex = 4;
            this.btnPlayFileStop.Text = "Stop";
            this.btnPlayFileStop.UseVisualStyleBackColor = true;
            this.btnPlayFileStop.Click += new System.EventHandler(this.btnPlayFileStop_Click);
            // 
            // tbPlayFile
            // 
            this.tbPlayFile.Location = new System.Drawing.Point(38, 19);
            this.tbPlayFile.Name = "tbPlayFile";
            this.tbPlayFile.ReadOnly = true;
            this.tbPlayFile.Size = new System.Drawing.Size(255, 20);
            this.tbPlayFile.TabIndex = 1;
            // 
            // btnPlayFileBrowse
            // 
            this.btnPlayFileBrowse.Location = new System.Drawing.Point(299, 17);
            this.btnPlayFileBrowse.Name = "btnPlayFileBrowse";
            this.btnPlayFileBrowse.Size = new System.Drawing.Size(30, 23);
            this.btnPlayFileBrowse.TabIndex = 2;
            this.btnPlayFileBrowse.Text = "...";
            this.btnPlayFileBrowse.UseVisualStyleBackColor = true;
            this.btnPlayFileBrowse.Click += new System.EventHandler(this.btnPlayFileBrowse_Click);
            // 
            // lblPalyFile
            // 
            this.lblPalyFile.AutoSize = true;
            this.lblPalyFile.Location = new System.Drawing.Point(6, 22);
            this.lblPalyFile.Name = "lblPalyFile";
            this.lblPalyFile.Size = new System.Drawing.Size(26, 13);
            this.lblPalyFile.TabIndex = 0;
            this.lblPalyFile.Text = "File:";
            // 
            // grpbSipPhone
            // 
            this.grpbSipPhone.Controls.Add(this.chkForward);
            this.grpbSipPhone.Controls.Add(this.lblMessageWaitingIndication);
            this.grpbSipPhone.Controls.Add(this.chkbAutoAnswer);
            this.grpbSipPhone.Controls.Add(this.grpPhoneLines);
            this.grpbSipPhone.Controls.Add(this.btnRedial);
            this.grpbSipPhone.Controls.Add(this.label1);
            this.grpbSipPhone.Controls.Add(this.lbCallHistory);
            this.grpbSipPhone.Controls.Add(this.btnHold);
            this.grpbSipPhone.Controls.Add(this.chbDND);
            this.grpbSipPhone.Controls.Add(this.chbxToolTip);
            this.grpbSipPhone.Controls.Add(this.tbPhoneStatus);
            this.grpbSipPhone.Controls.Add(this.lbPhoneCalls);
            this.grpbSipPhone.Controls.Add(this.label13);
            this.grpbSipPhone.Controls.Add(this.label6);
            this.grpbSipPhone.Controls.Add(this.lbPhoneLines);
            this.grpbSipPhone.Controls.Add(this.btnPager);
            this.grpbSipPhone.Controls.Add(this.btnTransfer);
            this.grpbSipPhone.Controls.Add(this.btnHangUp);
            this.grpbSipPhone.Controls.Add(this.btnPickUp);
            this.grpbSipPhone.Controls.Add(this.tbDialNumber);
            this.grpbSipPhone.Controls.Add(this.btnKeypad8);
            this.grpbSipPhone.Controls.Add(this.btnKeypad6);
            this.grpbSipPhone.Controls.Add(this.btnKeypad5);
            this.grpbSipPhone.Controls.Add(this.btnKeypad9);
            this.grpbSipPhone.Controls.Add(this.btnKeypadSharp);
            this.grpbSipPhone.Controls.Add(this.btnKeypadAsterisk);
            this.grpbSipPhone.Controls.Add(this.btnKeypad7);
            this.grpbSipPhone.Controls.Add(this.btnKeypad0);
            this.grpbSipPhone.Controls.Add(this.btnKeypad3);
            this.grpbSipPhone.Controls.Add(this.btnKeypad4);
            this.grpbSipPhone.Controls.Add(this.btnKeypad2);
            this.grpbSipPhone.Controls.Add(this.btnKeypad1);
            this.grpbSipPhone.Location = new System.Drawing.Point(3, 7);
            this.grpbSipPhone.Name = "grpbSipPhone";
            this.grpbSipPhone.Size = new System.Drawing.Size(676, 231);
            this.grpbSipPhone.TabIndex = 1;
            this.grpbSipPhone.TabStop = false;
            this.grpbSipPhone.Text = "SIP Phone";
            // 
            // chkForward
            // 
            this.chkForward.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkForward.Location = new System.Drawing.Point(336, 82);
            this.chkForward.Name = "chkForward";
            this.chkForward.Size = new System.Drawing.Size(77, 23);
            this.chkForward.TabIndex = 36;
            this.chkForward.Text = "Forward";
            this.chkForward.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkForward.UseVisualStyleBackColor = true;
            this.chkForward.CheckedChanged += new System.EventHandler(this.chkForward_CheckedChanged);
            // 
            // lblMessageWaitingIndication
            // 
            this.lblMessageWaitingIndication.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMessageWaitingIndication.Location = new System.Drawing.Point(501, 100);
            this.lblMessageWaitingIndication.Name = "lblMessageWaitingIndication";
            this.lblMessageWaitingIndication.Size = new System.Drawing.Size(168, 20);
            this.lblMessageWaitingIndication.TabIndex = 35;
            this.lblMessageWaitingIndication.Text = "No information";
            this.lblMessageWaitingIndication.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkbAutoAnswer
            // 
            this.chkbAutoAnswer.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkbAutoAnswer.AutoSize = true;
            this.chkbAutoAnswer.Location = new System.Drawing.Point(336, 53);
            this.chkbAutoAnswer.Name = "chkbAutoAnswer";
            this.chkbAutoAnswer.Size = new System.Drawing.Size(77, 23);
            this.chkbAutoAnswer.TabIndex = 34;
            this.chkbAutoAnswer.Text = "Auto Answer";
            this.chkbAutoAnswer.UseVisualStyleBackColor = true;
            this.chkbAutoAnswer.CheckedChanged += new System.EventHandler(this.chbAutoAnswer_CheckedChanged);
            // 
            // grpPhoneLines
            // 
            this.grpPhoneLines.Controls.Add(this.btnPhoneLine6);
            this.grpPhoneLines.Controls.Add(this.btnPhoneLine5);
            this.grpPhoneLines.Controls.Add(this.btnPhoneLine4);
            this.grpPhoneLines.Controls.Add(this.btnPhoneLine3);
            this.grpPhoneLines.Controls.Add(this.btnPhoneLine2);
            this.grpPhoneLines.Controls.Add(this.btnPhoneLine1);
            this.grpPhoneLines.Location = new System.Drawing.Point(419, 19);
            this.grpPhoneLines.Name = "grpPhoneLines";
            this.grpPhoneLines.Size = new System.Drawing.Size(76, 205);
            this.grpPhoneLines.TabIndex = 27;
            this.grpPhoneLines.TabStop = false;
            this.grpPhoneLines.Text = "Phone lines";
            // 
            // btnPhoneLine6
            // 
            this.btnPhoneLine6.Enabled = false;
            this.btnPhoneLine6.Location = new System.Drawing.Point(6, 176);
            this.btnPhoneLine6.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.btnPhoneLine6.Name = "btnPhoneLine6";
            this.btnPhoneLine6.Size = new System.Drawing.Size(62, 23);
            this.btnPhoneLine6.TabIndex = 28;
            this.btnPhoneLine6.Tag = "5";
            this.btnPhoneLine6.Text = "Line 6";
            this.btnPhoneLine6.UseVisualStyleBackColor = true;
            this.btnPhoneLine6.Click += new System.EventHandler(this.btnPhoneLines_Click);
            // 
            // btnPhoneLine5
            // 
            this.btnPhoneLine5.Enabled = false;
            this.btnPhoneLine5.Location = new System.Drawing.Point(6, 145);
            this.btnPhoneLine5.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.btnPhoneLine5.Name = "btnPhoneLine5";
            this.btnPhoneLine5.Size = new System.Drawing.Size(62, 23);
            this.btnPhoneLine5.TabIndex = 27;
            this.btnPhoneLine5.Tag = "4";
            this.btnPhoneLine5.Text = "Line 5";
            this.btnPhoneLine5.UseVisualStyleBackColor = true;
            this.btnPhoneLine5.Click += new System.EventHandler(this.btnPhoneLines_Click);
            // 
            // btnPhoneLine4
            // 
            this.btnPhoneLine4.Enabled = false;
            this.btnPhoneLine4.Location = new System.Drawing.Point(6, 114);
            this.btnPhoneLine4.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.btnPhoneLine4.Name = "btnPhoneLine4";
            this.btnPhoneLine4.Size = new System.Drawing.Size(62, 23);
            this.btnPhoneLine4.TabIndex = 26;
            this.btnPhoneLine4.Tag = "3";
            this.btnPhoneLine4.Text = "Line 4";
            this.btnPhoneLine4.UseVisualStyleBackColor = true;
            this.btnPhoneLine4.Click += new System.EventHandler(this.btnPhoneLines_Click);
            // 
            // btnPhoneLine3
            // 
            this.btnPhoneLine3.Enabled = false;
            this.btnPhoneLine3.Location = new System.Drawing.Point(6, 83);
            this.btnPhoneLine3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.btnPhoneLine3.Name = "btnPhoneLine3";
            this.btnPhoneLine3.Size = new System.Drawing.Size(62, 23);
            this.btnPhoneLine3.TabIndex = 25;
            this.btnPhoneLine3.Tag = "2";
            this.btnPhoneLine3.Text = "Line 3";
            this.btnPhoneLine3.UseVisualStyleBackColor = true;
            this.btnPhoneLine3.Click += new System.EventHandler(this.btnPhoneLines_Click);
            // 
            // btnPhoneLine2
            // 
            this.btnPhoneLine2.Enabled = false;
            this.btnPhoneLine2.Location = new System.Drawing.Point(6, 52);
            this.btnPhoneLine2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.btnPhoneLine2.Name = "btnPhoneLine2";
            this.btnPhoneLine2.Size = new System.Drawing.Size(62, 23);
            this.btnPhoneLine2.TabIndex = 24;
            this.btnPhoneLine2.Tag = "1";
            this.btnPhoneLine2.Text = "Line 2";
            this.btnPhoneLine2.UseVisualStyleBackColor = true;
            this.btnPhoneLine2.Click += new System.EventHandler(this.btnPhoneLines_Click);
            // 
            // btnPhoneLine1
            // 
            this.btnPhoneLine1.Enabled = false;
            this.btnPhoneLine1.Location = new System.Drawing.Point(6, 21);
            this.btnPhoneLine1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.btnPhoneLine1.Name = "btnPhoneLine1";
            this.btnPhoneLine1.Size = new System.Drawing.Size(62, 23);
            this.btnPhoneLine1.TabIndex = 23;
            this.btnPhoneLine1.Tag = "0";
            this.btnPhoneLine1.Text = "Line 1";
            this.btnPhoneLine1.UseVisualStyleBackColor = true;
            this.btnPhoneLine1.Click += new System.EventHandler(this.btnPhoneLines_Click);
            // 
            // btnRedial
            // 
            this.btnRedial.Location = new System.Drawing.Point(9, 196);
            this.btnRedial.Name = "btnRedial";
            this.btnRedial.Size = new System.Drawing.Size(152, 24);
            this.btnRedial.TabIndex = 8;
            this.btnRedial.Text = "Redial last outgoing call";
            this.btnRedial.UseVisualStyleBackColor = true;
            this.btnRedial.Click += new System.EventHandler(this.btnRedial_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Call history:";
            // 
            // lbCallHistory
            // 
            this.lbCallHistory.FormattingEnabled = true;
            this.lbCallHistory.Location = new System.Drawing.Point(9, 38);
            this.lbCallHistory.Name = "lbCallHistory";
            this.lbCallHistory.Size = new System.Drawing.Size(152, 147);
            this.lbCallHistory.TabIndex = 33;
            this.lbCallHistory.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbCallHistory_MouseDoubleClick);
            // 
            // btnHold
            // 
            this.btnHold.Location = new System.Drawing.Point(336, 111);
            this.btnHold.Name = "btnHold";
            this.btnHold.Size = new System.Drawing.Size(77, 23);
            this.btnHold.TabIndex = 31;
            this.btnHold.Text = "Hold";
            this.btnHold.UseVisualStyleBackColor = true;
            this.btnHold.Click += new System.EventHandler(this.btnHold_Click);
            // 
            // chbDND
            // 
            this.chbDND.Appearance = System.Windows.Forms.Appearance.Button;
            this.chbDND.Location = new System.Drawing.Point(336, 198);
            this.chbDND.Name = "chbDND";
            this.chbDND.Size = new System.Drawing.Size(77, 23);
            this.chbDND.TabIndex = 8;
            this.chbDND.Text = "DND";
            this.chbDND.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chbDND.UseVisualStyleBackColor = true;
            this.chbDND.CheckedChanged += new System.EventHandler(this.chbDND_CheckedChanged);
            // 
            // chbxToolTip
            // 
            this.chbxToolTip.AutoSize = true;
            this.chbxToolTip.Location = new System.Drawing.Point(342, 26);
            this.chbxToolTip.MaximumSize = new System.Drawing.Size(80, 20);
            this.chbxToolTip.MinimumSize = new System.Drawing.Size(80, 20);
            this.chbxToolTip.Name = "chbxToolTip";
            this.chbxToolTip.Size = new System.Drawing.Size(80, 20);
            this.chbxToolTip.TabIndex = 17;
            this.chbxToolTip.Text = "ToolTips";
            this.chbxToolTip.UseVisualStyleBackColor = true;
            this.chbxToolTip.CheckedChanged += new System.EventHandler(this.chbxToolTip_CheckedChanged);
            // 
            // tbPhoneStatus
            // 
            this.tbPhoneStatus.Enabled = false;
            this.tbPhoneStatus.Location = new System.Drawing.Point(167, 22);
            this.tbPhoneStatus.Name = "tbPhoneStatus";
            this.tbPhoneStatus.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbPhoneStatus.Size = new System.Drawing.Size(162, 20);
            this.tbPhoneStatus.TabIndex = 1;
            // 
            // lbPhoneCalls
            // 
            this.lbPhoneCalls.FormattingEnabled = true;
            this.lbPhoneCalls.Location = new System.Drawing.Point(501, 138);
            this.lbPhoneCalls.Name = "lbPhoneCalls";
            this.lbPhoneCalls.Size = new System.Drawing.Size(168, 82);
            this.lbPhoneCalls.TabIndex = 30;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(498, 120);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(99, 13);
            this.label13.TabIndex = 20;
            this.label13.Text = "Active Phone Calls:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(498, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Phone Lines:";
            // 
            // lbPhoneLines
            // 
            this.lbPhoneLines.FormattingEnabled = true;
            this.lbPhoneLines.Location = new System.Drawing.Point(501, 38);
            this.lbPhoneLines.Name = "lbPhoneLines";
            this.lbPhoneLines.Size = new System.Drawing.Size(168, 56);
            this.lbPhoneLines.TabIndex = 29;
            this.lbPhoneLines.SelectedIndexChanged += new System.EventHandler(this.lbPhoneLines_SelectedIndexChanged);
            // 
            // btnPager
            // 
            this.btnPager.Enabled = false;
            this.btnPager.Location = new System.Drawing.Point(336, 169);
            this.btnPager.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.btnPager.Name = "btnPager";
            this.btnPager.Size = new System.Drawing.Size(77, 23);
            this.btnPager.TabIndex = 21;
            this.btnPager.Text = "Pager";
            this.btnPager.UseVisualStyleBackColor = true;
            // 
            // btnTransfer
            // 
            this.btnTransfer.Enabled = false;
            this.btnTransfer.Location = new System.Drawing.Point(336, 140);
            this.btnTransfer.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(77, 23);
            this.btnTransfer.TabIndex = 20;
            this.btnTransfer.Text = "Transfer";
            this.btnTransfer.UseVisualStyleBackColor = true;
            this.btnTransfer.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // btnHangUp
            // 
            this.btnHangUp.Location = new System.Drawing.Point(264, 74);
            this.btnHangUp.Name = "btnHangUp";
            this.btnHangUp.Size = new System.Drawing.Size(65, 23);
            this.btnHangUp.TabIndex = 4;
            this.btnHangUp.Text = "Hang up";
            this.btnHangUp.UseVisualStyleBackColor = true;
            this.btnHangUp.Click += new System.EventHandler(this.btnHangUp_Click);
            // 
            // btnPickUp
            // 
            this.btnPickUp.Location = new System.Drawing.Point(167, 74);
            this.btnPickUp.Name = "btnPickUp";
            this.btnPickUp.Size = new System.Drawing.Size(65, 23);
            this.btnPickUp.TabIndex = 3;
            this.btnPickUp.Text = "Pick up";
            this.btnPickUp.UseVisualStyleBackColor = true;
            this.btnPickUp.Click += new System.EventHandler(this.btnPickUp_Click);
            // 
            // tbDialNumber
            // 
            this.tbDialNumber.BackColor = System.Drawing.Color.White;
            this.tbDialNumber.Location = new System.Drawing.Point(167, 48);
            this.tbDialNumber.Name = "tbDialNumber";
            this.tbDialNumber.ReadOnly = true;
            this.tbDialNumber.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbDialNumber.Size = new System.Drawing.Size(162, 20);
            this.tbDialNumber.TabIndex = 2;
            // 
            // btnKeypad8
            // 
            this.btnKeypad8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKeypad8.Location = new System.Drawing.Point(223, 165);
            this.btnKeypad8.Name = "btnKeypad8";
            this.btnKeypad8.Size = new System.Drawing.Size(50, 25);
            this.btnKeypad8.TabIndex = 12;
            this.btnKeypad8.Tag = "8";
            this.btnKeypad8.Text = "8";
            this.btnKeypad8.UseVisualStyleBackColor = true;
            this.btnKeypad8.Click += new System.EventHandler(this.btnKeypad_Click);
            this.btnKeypad8.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Keypad_MouseDown);
            this.btnKeypad8.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Keypad_MouseUp);
            // 
            // btnKeypad6
            // 
            this.btnKeypad6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKeypad6.Location = new System.Drawing.Point(279, 134);
            this.btnKeypad6.Name = "btnKeypad6";
            this.btnKeypad6.Size = new System.Drawing.Size(50, 25);
            this.btnKeypad6.TabIndex = 10;
            this.btnKeypad6.Tag = "6";
            this.btnKeypad6.Text = "6";
            this.btnKeypad6.UseVisualStyleBackColor = true;
            this.btnKeypad6.Click += new System.EventHandler(this.btnKeypad_Click);
            this.btnKeypad6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Keypad_MouseDown);
            this.btnKeypad6.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Keypad_MouseUp);
            // 
            // btnKeypad5
            // 
            this.btnKeypad5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKeypad5.Location = new System.Drawing.Point(223, 134);
            this.btnKeypad5.Name = "btnKeypad5";
            this.btnKeypad5.Size = new System.Drawing.Size(50, 25);
            this.btnKeypad5.TabIndex = 9;
            this.btnKeypad5.Tag = "5";
            this.btnKeypad5.Text = "5";
            this.btnKeypad5.UseVisualStyleBackColor = true;
            this.btnKeypad5.Click += new System.EventHandler(this.btnKeypad_Click);
            this.btnKeypad5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Keypad_MouseDown);
            this.btnKeypad5.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Keypad_MouseUp);
            // 
            // btnKeypad9
            // 
            this.btnKeypad9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKeypad9.Location = new System.Drawing.Point(279, 165);
            this.btnKeypad9.Name = "btnKeypad9";
            this.btnKeypad9.Size = new System.Drawing.Size(50, 25);
            this.btnKeypad9.TabIndex = 13;
            this.btnKeypad9.Tag = "9";
            this.btnKeypad9.Text = "9";
            this.btnKeypad9.UseVisualStyleBackColor = true;
            this.btnKeypad9.Click += new System.EventHandler(this.btnKeypad_Click);
            this.btnKeypad9.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Keypad_MouseDown);
            this.btnKeypad9.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Keypad_MouseUp);
            // 
            // btnKeypadSharp
            // 
            this.btnKeypadSharp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKeypadSharp.Location = new System.Drawing.Point(279, 196);
            this.btnKeypadSharp.Name = "btnKeypadSharp";
            this.btnKeypadSharp.Size = new System.Drawing.Size(50, 25);
            this.btnKeypadSharp.TabIndex = 16;
            this.btnKeypadSharp.Tag = "11";
            this.btnKeypadSharp.Text = "#";
            this.btnKeypadSharp.UseVisualStyleBackColor = true;
            this.btnKeypadSharp.Click += new System.EventHandler(this.btnKeypad_Click);
            this.btnKeypadSharp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Keypad_MouseDown);
            this.btnKeypadSharp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Keypad_MouseUp);
            // 
            // btnKeypadAsterisk
            // 
            this.btnKeypadAsterisk.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKeypadAsterisk.Location = new System.Drawing.Point(167, 196);
            this.btnKeypadAsterisk.Name = "btnKeypadAsterisk";
            this.btnKeypadAsterisk.Size = new System.Drawing.Size(50, 25);
            this.btnKeypadAsterisk.TabIndex = 14;
            this.btnKeypadAsterisk.Tag = "10";
            this.btnKeypadAsterisk.Text = "*";
            this.btnKeypadAsterisk.UseVisualStyleBackColor = true;
            this.btnKeypadAsterisk.Click += new System.EventHandler(this.btnKeypad_Click);
            this.btnKeypadAsterisk.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Keypad_MouseDown);
            this.btnKeypadAsterisk.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Keypad_MouseUp);
            // 
            // btnKeypad7
            // 
            this.btnKeypad7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKeypad7.Location = new System.Drawing.Point(167, 165);
            this.btnKeypad7.Name = "btnKeypad7";
            this.btnKeypad7.Size = new System.Drawing.Size(50, 25);
            this.btnKeypad7.TabIndex = 11;
            this.btnKeypad7.Tag = "7";
            this.btnKeypad7.Text = "7";
            this.btnKeypad7.UseVisualStyleBackColor = true;
            this.btnKeypad7.Click += new System.EventHandler(this.btnKeypad_Click);
            this.btnKeypad7.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Keypad_MouseDown);
            this.btnKeypad7.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Keypad_MouseUp);
            // 
            // btnKeypad0
            // 
            this.btnKeypad0.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKeypad0.Location = new System.Drawing.Point(223, 196);
            this.btnKeypad0.Name = "btnKeypad0";
            this.btnKeypad0.Size = new System.Drawing.Size(50, 25);
            this.btnKeypad0.TabIndex = 15;
            this.btnKeypad0.Tag = "0";
            this.btnKeypad0.Text = "0";
            this.btnKeypad0.UseVisualStyleBackColor = true;
            this.btnKeypad0.Click += new System.EventHandler(this.btnKeypad_Click);
            this.btnKeypad0.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Keypad_MouseDown);
            this.btnKeypad0.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Keypad_MouseUp);
            // 
            // btnKeypad3
            // 
            this.btnKeypad3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKeypad3.Location = new System.Drawing.Point(279, 103);
            this.btnKeypad3.Name = "btnKeypad3";
            this.btnKeypad3.Size = new System.Drawing.Size(50, 25);
            this.btnKeypad3.TabIndex = 7;
            this.btnKeypad3.Tag = "3";
            this.btnKeypad3.Text = "3";
            this.btnKeypad3.UseVisualStyleBackColor = true;
            this.btnKeypad3.Click += new System.EventHandler(this.btnKeypad_Click);
            this.btnKeypad3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Keypad_MouseDown);
            this.btnKeypad3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Keypad_MouseUp);
            // 
            // btnKeypad4
            // 
            this.btnKeypad4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKeypad4.Location = new System.Drawing.Point(167, 134);
            this.btnKeypad4.Name = "btnKeypad4";
            this.btnKeypad4.Size = new System.Drawing.Size(50, 25);
            this.btnKeypad4.TabIndex = 8;
            this.btnKeypad4.Tag = "4";
            this.btnKeypad4.Text = "4";
            this.btnKeypad4.UseVisualStyleBackColor = true;
            this.btnKeypad4.Click += new System.EventHandler(this.btnKeypad_Click);
            this.btnKeypad4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Keypad_MouseDown);
            this.btnKeypad4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Keypad_MouseUp);
            // 
            // btnKeypad2
            // 
            this.btnKeypad2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKeypad2.Location = new System.Drawing.Point(223, 103);
            this.btnKeypad2.Name = "btnKeypad2";
            this.btnKeypad2.Size = new System.Drawing.Size(50, 25);
            this.btnKeypad2.TabIndex = 6;
            this.btnKeypad2.Tag = "2";
            this.btnKeypad2.Text = "2";
            this.btnKeypad2.UseVisualStyleBackColor = true;
            this.btnKeypad2.Click += new System.EventHandler(this.btnKeypad_Click);
            this.btnKeypad2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Keypad_MouseDown);
            this.btnKeypad2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Keypad_MouseUp);
            // 
            // btnKeypad1
            // 
            this.btnKeypad1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKeypad1.Location = new System.Drawing.Point(167, 103);
            this.btnKeypad1.Name = "btnKeypad1";
            this.btnKeypad1.Size = new System.Drawing.Size(50, 25);
            this.btnKeypad1.TabIndex = 5;
            this.btnKeypad1.Tag = "1";
            this.btnKeypad1.Text = "1";
            this.btnKeypad1.UseVisualStyleBackColor = true;
            this.btnKeypad1.Click += new System.EventHandler(this.btnKeypad_Click);
            this.btnKeypad1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Keypad_MouseDown);
            this.btnKeypad1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Keypad_MouseUp);
            // 
            // tpNatSettings
            // 
            this.tpNatSettings.BackColor = System.Drawing.SystemColors.Control;
            this.tpNatSettings.Controls.Add(this.groupBox4);
            this.tpNatSettings.Controls.Add(this.groupBox2);
            this.tpNatSettings.Controls.Add(this.groupBox1);
            this.tpNatSettings.Controls.Add(this.grpbDSPs);
            this.tpNatSettings.Controls.Add(this.grpbCodecs);
            this.tpNatSettings.Controls.Add(this.gpRingback);
            this.tpNatSettings.Controls.Add(this.grpbKeepAlive);
            this.tpNatSettings.Controls.Add(this.gpRingtone);
            this.tpNatSettings.Location = new System.Drawing.Point(4, 22);
            this.tpNatSettings.Name = "tpNatSettings";
            this.tpNatSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpNatSettings.Size = new System.Drawing.Size(994, 475);
            this.tpNatSettings.TabIndex = 1;
            this.tpNatSettings.Text = "Settings";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnAdapterApply);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.cobAdaptersIP);
            this.groupBox4.Location = new System.Drawing.Point(6, 147);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(665, 50);
            this.groupBox4.TabIndex = 40;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Network Adapter setting";
            // 
            // btnAdapterApply
            // 
            this.btnAdapterApply.Location = new System.Drawing.Point(227, 20);
            this.btnAdapterApply.Name = "btnAdapterApply";
            this.btnAdapterApply.Size = new System.Drawing.Size(75, 23);
            this.btnAdapterApply.TabIndex = 19;
            this.btnAdapterApply.Text = "Apply";
            this.btnAdapterApply.UseVisualStyleBackColor = true;
            this.btnAdapterApply.Click += new System.EventHandler(this.btnAdapterApply_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Used IP address:";
            // 
            // cobAdaptersIP
            // 
            this.cobAdaptersIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobAdaptersIP.FormattingEnabled = true;
            this.cobAdaptersIP.Location = new System.Drawing.Point(100, 20);
            this.cobAdaptersIP.Name = "cobAdaptersIP";
            this.cobAdaptersIP.Size = new System.Drawing.Size(121, 21);
            this.cobAdaptersIP.TabIndex = 18;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cobSRTPmode);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tbUserId);
            this.groupBox2.Controls.Add(this.tbProxy);
            this.groupBox2.Controls.Add(this.lblUserId);
            this.groupBox2.Controls.Add(this.lblOutboundProxy);
            this.groupBox2.Controls.Add(this.lblDisplayName);
            this.groupBox2.Controls.Add(this.chkbATAMode);
            this.groupBox2.Controls.Add(this.tbDisplayName);
            this.groupBox2.Controls.Add(this.chkbRegistrationRequired);
            this.groupBox2.Controls.Add(this.lblRegisterName);
            this.groupBox2.Controls.Add(this.btnSIPUnregister);
            this.groupBox2.Controls.Add(this.tbRegisterName);
            this.groupBox2.Controls.Add(this.btnSIPRegister);
            this.groupBox2.Controls.Add(this.lblRegisterPassword);
            this.groupBox2.Controls.Add(this.lblTransport);
            this.groupBox2.Controls.Add(this.tbRegisterPassword);
            this.groupBox2.Controls.Add(this.cobTransport);
            this.groupBox2.Controls.Add(this.tbDomainServer);
            this.groupBox2.Controls.Add(this.chkbEnableLogging);
            this.groupBox2.Controls.Add(this.lblDomainName);
            this.groupBox2.Location = new System.Drawing.Point(3, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(668, 135);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SIP Account Settings";
            // 
            // cobSRTPmode
            // 
            this.cobSRTPmode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobSRTPmode.FormattingEnabled = true;
            this.cobSRTPmode.Items.AddRange(new object[] {
            "None",
            "Prefer",
            "Force"});
            this.cobSRTPmode.Location = new System.Drawing.Point(307, 79);
            this.cobSRTPmode.Name = "cobSRTPmode";
            this.cobSRTPmode.Size = new System.Drawing.Size(72, 21);
            this.cobSRTPmode.TabIndex = 18;
            this.cobSRTPmode.SelectedIndexChanged += new System.EventHandler(this.cobSRTPmode_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(221, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "SRTP mode:";
            // 
            // tbUserId
            // 
            this.tbUserId.Location = new System.Drawing.Point(91, 21);
            this.tbUserId.Margin = new System.Windows.Forms.Padding(3, 5, 5, 5);
            this.tbUserId.Name = "tbUserId";
            this.tbUserId.Size = new System.Drawing.Size(120, 20);
            this.tbUserId.TabIndex = 0;
            // 
            // tbProxy
            // 
            this.tbProxy.Location = new System.Drawing.Point(543, 50);
            this.tbProxy.Margin = new System.Windows.Forms.Padding(3, 5, 5, 5);
            this.tbProxy.Name = "tbProxy";
            this.tbProxy.Size = new System.Drawing.Size(120, 20);
            this.tbProxy.TabIndex = 5;
            // 
            // lblUserId
            // 
            this.lblUserId.AutoSize = true;
            this.lblUserId.Location = new System.Drawing.Point(8, 24);
            this.lblUserId.Margin = new System.Windows.Forms.Padding(5);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new System.Drawing.Size(46, 13);
            this.lblUserId.TabIndex = 1;
            this.lblUserId.Text = "User ID:";
            // 
            // lblOutboundProxy
            // 
            this.lblOutboundProxy.AutoSize = true;
            this.lblOutboundProxy.Location = new System.Drawing.Point(437, 53);
            this.lblOutboundProxy.Margin = new System.Windows.Forms.Padding(5);
            this.lblOutboundProxy.Name = "lblOutboundProxy";
            this.lblOutboundProxy.Size = new System.Drawing.Size(86, 13);
            this.lblOutboundProxy.TabIndex = 16;
            this.lblOutboundProxy.Text = "Outbound Proxy:";
            // 
            // lblDisplayName
            // 
            this.lblDisplayName.AutoSize = true;
            this.lblDisplayName.Location = new System.Drawing.Point(8, 52);
            this.lblDisplayName.Margin = new System.Windows.Forms.Padding(5);
            this.lblDisplayName.Name = "lblDisplayName";
            this.lblDisplayName.Size = new System.Drawing.Size(75, 13);
            this.lblDisplayName.TabIndex = 2;
            this.lblDisplayName.Text = "Display Name:";
            // 
            // chkbATAMode
            // 
            this.chkbATAMode.AutoSize = true;
            this.chkbATAMode.Location = new System.Drawing.Point(11, 109);
            this.chkbATAMode.Name = "chkbATAMode";
            this.chkbATAMode.Size = new System.Drawing.Size(77, 17);
            this.chkbATAMode.TabIndex = 7;
            this.chkbATAMode.Text = "ATA Mode";
            this.chkbATAMode.UseVisualStyleBackColor = true;
            this.chkbATAMode.CheckedChanged += new System.EventHandler(this.cbATAMode_CheckedChanged);
            // 
            // tbDisplayName
            // 
            this.tbDisplayName.Location = new System.Drawing.Point(91, 49);
            this.tbDisplayName.Margin = new System.Windows.Forms.Padding(3, 3, 5, 5);
            this.tbDisplayName.Name = "tbDisplayName";
            this.tbDisplayName.Size = new System.Drawing.Size(120, 20);
            this.tbDisplayName.TabIndex = 3;
            // 
            // chkbRegistrationRequired
            // 
            this.chkbRegistrationRequired.AutoSize = true;
            this.chkbRegistrationRequired.Checked = true;
            this.chkbRegistrationRequired.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbRegistrationRequired.Location = new System.Drawing.Point(107, 109);
            this.chkbRegistrationRequired.Name = "chkbRegistrationRequired";
            this.chkbRegistrationRequired.Size = new System.Drawing.Size(123, 17);
            this.chkbRegistrationRequired.TabIndex = 8;
            this.chkbRegistrationRequired.Text = "Registration required";
            this.chkbRegistrationRequired.UseVisualStyleBackColor = true;
            // 
            // lblRegisterName
            // 
            this.lblRegisterName.AutoSize = true;
            this.lblRegisterName.Location = new System.Drawing.Point(221, 24);
            this.lblRegisterName.Margin = new System.Windows.Forms.Padding(5);
            this.lblRegisterName.Name = "lblRegisterName";
            this.lblRegisterName.Size = new System.Drawing.Size(78, 13);
            this.lblRegisterName.TabIndex = 4;
            this.lblRegisterName.Text = "Register name:";
            // 
            // btnSIPUnregister
            // 
            this.btnSIPUnregister.Location = new System.Drawing.Point(588, 79);
            this.btnSIPUnregister.Margin = new System.Windows.Forms.Padding(5);
            this.btnSIPUnregister.Name = "btnSIPUnregister";
            this.btnSIPUnregister.Size = new System.Drawing.Size(75, 23);
            this.btnSIPUnregister.TabIndex = 11;
            this.btnSIPUnregister.Text = "Unregister";
            this.btnSIPUnregister.UseVisualStyleBackColor = true;
            this.btnSIPUnregister.Click += new System.EventHandler(this.btnSIPUnregister_Click);
            // 
            // tbRegisterName
            // 
            this.tbRegisterName.Location = new System.Drawing.Point(307, 21);
            this.tbRegisterName.Margin = new System.Windows.Forms.Padding(3, 5, 5, 5);
            this.tbRegisterName.Name = "tbRegisterName";
            this.tbRegisterName.Size = new System.Drawing.Size(120, 20);
            this.tbRegisterName.TabIndex = 1;
            // 
            // btnSIPRegister
            // 
            this.btnSIPRegister.Location = new System.Drawing.Point(503, 79);
            this.btnSIPRegister.Margin = new System.Windows.Forms.Padding(5);
            this.btnSIPRegister.Name = "btnSIPRegister";
            this.btnSIPRegister.Size = new System.Drawing.Size(75, 23);
            this.btnSIPRegister.TabIndex = 10;
            this.btnSIPRegister.Text = "Register";
            this.btnSIPRegister.UseVisualStyleBackColor = true;
            this.btnSIPRegister.Click += new System.EventHandler(this.btnSIPRegister_Click);
            // 
            // lblRegisterPassword
            // 
            this.lblRegisterPassword.AutoSize = true;
            this.lblRegisterPassword.Location = new System.Drawing.Point(437, 24);
            this.lblRegisterPassword.Margin = new System.Windows.Forms.Padding(5);
            this.lblRegisterPassword.Name = "lblRegisterPassword";
            this.lblRegisterPassword.Size = new System.Drawing.Size(98, 13);
            this.lblRegisterPassword.TabIndex = 6;
            this.lblRegisterPassword.Text = "Register Password:";
            // 
            // lblTransport
            // 
            this.lblTransport.AutoSize = true;
            this.lblTransport.Location = new System.Drawing.Point(8, 82);
            this.lblTransport.Margin = new System.Windows.Forms.Padding(5);
            this.lblTransport.Name = "lblTransport";
            this.lblTransport.Size = new System.Drawing.Size(55, 13);
            this.lblTransport.TabIndex = 14;
            this.lblTransport.Text = "Transport:";
            // 
            // tbRegisterPassword
            // 
            this.tbRegisterPassword.Location = new System.Drawing.Point(543, 21);
            this.tbRegisterPassword.Margin = new System.Windows.Forms.Padding(3, 5, 5, 5);
            this.tbRegisterPassword.Name = "tbRegisterPassword";
            this.tbRegisterPassword.PasswordChar = '*';
            this.tbRegisterPassword.Size = new System.Drawing.Size(120, 20);
            this.tbRegisterPassword.TabIndex = 2;
            // 
            // cobTransport
            // 
            this.cobTransport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobTransport.FormattingEnabled = true;
            this.cobTransport.Items.AddRange(new object[] {
            "UDP",
            "TCP",
            "TLS"});
            this.cobTransport.Location = new System.Drawing.Point(91, 79);
            this.cobTransport.Margin = new System.Windows.Forms.Padding(5);
            this.cobTransport.Name = "cobTransport";
            this.cobTransport.Size = new System.Drawing.Size(72, 21);
            this.cobTransport.TabIndex = 6;
            this.cobTransport.Tag = "";
            // 
            // tbDomainServer
            // 
            this.tbDomainServer.Location = new System.Drawing.Point(307, 49);
            this.tbDomainServer.Margin = new System.Windows.Forms.Padding(3, 3, 5, 5);
            this.tbDomainServer.Name = "tbDomainServer";
            this.tbDomainServer.Size = new System.Drawing.Size(120, 20);
            this.tbDomainServer.TabIndex = 4;
            // 
            // chkbEnableLogging
            // 
            this.chkbEnableLogging.AutoSize = true;
            this.chkbEnableLogging.Checked = true;
            this.chkbEnableLogging.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbEnableLogging.Enabled = false;
            this.chkbEnableLogging.Location = new System.Drawing.Point(246, 109);
            this.chkbEnableLogging.Margin = new System.Windows.Forms.Padding(5);
            this.chkbEnableLogging.Name = "chkbEnableLogging";
            this.chkbEnableLogging.Size = new System.Drawing.Size(64, 17);
            this.chkbEnableLogging.TabIndex = 9;
            this.chkbEnableLogging.Text = "Logging";
            this.chkbEnableLogging.UseVisualStyleBackColor = true;
            // 
            // lblDomainName
            // 
            this.lblDomainName.AutoSize = true;
            this.lblDomainName.Location = new System.Drawing.Point(221, 52);
            this.lblDomainName.Margin = new System.Windows.Forms.Padding(5);
            this.lblDomainName.Name = "lblDomainName";
            this.lblDomainName.Size = new System.Drawing.Size(78, 13);
            this.lblDomainName.TabIndex = 10;
            this.lblDomainName.Text = "Domain server:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cobNatType);
            this.groupBox1.Controls.Add(this.btnApply);
            this.groupBox1.Controls.Add(this.lblNatServer);
            this.groupBox1.Controls.Add(this.tbNatServer);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.lblNatUsername);
            this.groupBox1.Controls.Add(this.tbNatPassword);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.tbNatUserName);
            this.groupBox1.Location = new System.Drawing.Point(6, 203);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(665, 110);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "NAT settings";
            // 
            // cobNatType
            // 
            this.cobNatType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobNatType.FormattingEnabled = true;
            this.cobNatType.Items.AddRange(new object[] {
            "Local Address (No NAT)",
            "Public Address (STUN)",
            "Media Relay (TURN)",
            "Auto-Detect"});
            this.cobNatType.Location = new System.Drawing.Point(51, 21);
            this.cobNatType.Margin = new System.Windows.Forms.Padding(3, 5, 5, 5);
            this.cobNatType.Name = "cobNatType";
            this.cobNatType.Size = new System.Drawing.Size(198, 21);
            this.cobNatType.TabIndex = 9;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(540, 78);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(61, 23);
            this.btnApply.TabIndex = 12;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // lblNatServer
            // 
            this.lblNatServer.AutoSize = true;
            this.lblNatServer.Location = new System.Drawing.Point(313, 24);
            this.lblNatServer.Name = "lblNatServer";
            this.lblNatServer.Size = new System.Drawing.Size(41, 13);
            this.lblNatServer.TabIndex = 0;
            this.lblNatServer.Text = "Server:";
            // 
            // tbNatServer
            // 
            this.tbNatServer.Location = new System.Drawing.Point(360, 21);
            this.tbNatServer.Margin = new System.Windows.Forms.Padding(3, 10, 5, 5);
            this.tbNatServer.Name = "tbNatServer";
            this.tbNatServer.Size = new System.Drawing.Size(160, 20);
            this.tbNatServer.TabIndex = 1;
            this.tbNatServer.Text = "stun.ozekiphone.com";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(11, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "Type:";
            // 
            // lblNatUsername
            // 
            this.lblNatUsername.AutoSize = true;
            this.lblNatUsername.Location = new System.Drawing.Point(296, 54);
            this.lblNatUsername.Name = "lblNatUsername";
            this.lblNatUsername.Size = new System.Drawing.Size(58, 13);
            this.lblNatUsername.TabIndex = 4;
            this.lblNatUsername.Text = "Username:";
            // 
            // tbNatPassword
            // 
            this.tbNatPassword.Location = new System.Drawing.Point(360, 81);
            this.tbNatPassword.Margin = new System.Windows.Forms.Padding(3, 5, 5, 5);
            this.tbNatPassword.Name = "tbNatPassword";
            this.tbNatPassword.Size = new System.Drawing.Size(160, 20);
            this.tbNatPassword.TabIndex = 7;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(296, 84);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Password:";
            // 
            // tbNatUserName
            // 
            this.tbNatUserName.Location = new System.Drawing.Point(360, 51);
            this.tbNatUserName.Margin = new System.Windows.Forms.Padding(3, 5, 5, 5);
            this.tbNatUserName.Name = "tbNatUserName";
            this.tbNatUserName.Size = new System.Drawing.Size(160, 20);
            this.tbNatUserName.TabIndex = 6;
            // 
            // grpbDSPs
            // 
            this.grpbDSPs.Controls.Add(this.lMaxAGCGain);
            this.grpbDSPs.Controls.Add(this.numMaxAGCGain);
            this.grpbDSPs.Controls.Add(this.numAECDelay);
            this.grpbDSPs.Controls.Add(this.lDelay);
            this.grpbDSPs.Controls.Add(this.chkbDenoise);
            this.grpbDSPs.Controls.Add(this.chkbAGC);
            this.grpbDSPs.Controls.Add(this.chkbAEC);
            this.grpbDSPs.Location = new System.Drawing.Point(692, 66);
            this.grpbDSPs.Name = "grpbDSPs";
            this.grpbDSPs.Size = new System.Drawing.Size(146, 191);
            this.grpbDSPs.TabIndex = 37;
            this.grpbDSPs.TabStop = false;
            this.grpbDSPs.Text = "Voice Quality Enhancement";
            // 
            // lMaxAGCGain
            // 
            this.lMaxAGCGain.AutoSize = true;
            this.lMaxAGCGain.Location = new System.Drawing.Point(7, 165);
            this.lMaxAGCGain.Name = "lMaxAGCGain";
            this.lMaxAGCGain.Size = new System.Drawing.Size(55, 13);
            this.lMaxAGCGain.TabIndex = 7;
            this.lMaxAGCGain.Text = "Max Gain:";
            // 
            // numMaxAGCGain
            // 
            this.numMaxAGCGain.Location = new System.Drawing.Point(78, 165);
            this.numMaxAGCGain.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numMaxAGCGain.Name = "numMaxAGCGain";
            this.numMaxAGCGain.Size = new System.Drawing.Size(58, 20);
            this.numMaxAGCGain.TabIndex = 4;
            this.numMaxAGCGain.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numMaxAGCGain.ValueChanged += new System.EventHandler(this.numMaxAGCGain_ValueChanged);
            // 
            // numAECDelay
            // 
            this.numAECDelay.Location = new System.Drawing.Point(78, 139);
            this.numAECDelay.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numAECDelay.Name = "numAECDelay";
            this.numAECDelay.Size = new System.Drawing.Size(58, 20);
            this.numAECDelay.TabIndex = 3;
            this.numAECDelay.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numAECDelay.ValueChanged += new System.EventHandler(this.tbAECDelay_TextChanged);
            // 
            // lDelay
            // 
            this.lDelay.AutoSize = true;
            this.lDelay.Location = new System.Drawing.Point(6, 138);
            this.lDelay.Name = "lDelay";
            this.lDelay.Size = new System.Drawing.Size(63, 13);
            this.lDelay.TabIndex = 4;
            this.lDelay.Text = "Echo delay:";
            // 
            // chkbDenoise
            // 
            this.chkbDenoise.AutoSize = true;
            this.chkbDenoise.Checked = true;
            this.chkbDenoise.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbDenoise.Location = new System.Drawing.Point(9, 76);
            this.chkbDenoise.Name = "chkbDenoise";
            this.chkbDenoise.Size = new System.Drawing.Size(105, 17);
            this.chkbDenoise.TabIndex = 2;
            this.chkbDenoise.Text = "Noise Reduction";
            this.chkbDenoise.UseVisualStyleBackColor = true;
            this.chkbDenoise.CheckedChanged += new System.EventHandler(this.chkbDenoise_CheckedChanged);
            // 
            // chkbAGC
            // 
            this.chkbAGC.AutoSize = true;
            this.chkbAGC.Checked = true;
            this.chkbAGC.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbAGC.Location = new System.Drawing.Point(9, 53);
            this.chkbAGC.Name = "chkbAGC";
            this.chkbAGC.Size = new System.Drawing.Size(128, 17);
            this.chkbAGC.TabIndex = 1;
            this.chkbAGC.Text = "Automatic Voice Gain";
            this.chkbAGC.UseVisualStyleBackColor = true;
            this.chkbAGC.CheckedChanged += new System.EventHandler(this.chkbAGC_CheckedChanged);
            // 
            // chkbAEC
            // 
            this.chkbAEC.AutoSize = true;
            this.chkbAEC.Checked = true;
            this.chkbAEC.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbAEC.Location = new System.Drawing.Point(9, 29);
            this.chkbAEC.Name = "chkbAEC";
            this.chkbAEC.Size = new System.Drawing.Size(112, 17);
            this.chkbAEC.TabIndex = 0;
            this.chkbAEC.Text = "Echo Cancellation";
            this.chkbAEC.UseVisualStyleBackColor = true;
            this.chkbAEC.CheckedChanged += new System.EventHandler(this.chkbAEC_CheckedChanged);
            // 
            // grpbCodecs
            // 
            this.grpbCodecs.Controls.Add(this.btnCodecsSelectAll);
            this.grpbCodecs.Controls.Add(this.btnCodecsDeselectAll);
            this.grpbCodecs.Controls.Add(this.lbCodecs);
            this.grpbCodecs.Location = new System.Drawing.Point(840, 66);
            this.grpbCodecs.Name = "grpbCodecs";
            this.grpbCodecs.Size = new System.Drawing.Size(148, 191);
            this.grpbCodecs.TabIndex = 36;
            this.grpbCodecs.TabStop = false;
            this.grpbCodecs.Text = "Codecs";
            // 
            // btnCodecsSelectAll
            // 
            this.btnCodecsSelectAll.Location = new System.Drawing.Point(6, 162);
            this.btnCodecsSelectAll.Name = "btnCodecsSelectAll";
            this.btnCodecsSelectAll.Size = new System.Drawing.Size(67, 23);
            this.btnCodecsSelectAll.TabIndex = 1;
            this.btnCodecsSelectAll.Text = "Select all";
            this.btnCodecsSelectAll.UseVisualStyleBackColor = true;
            this.btnCodecsSelectAll.Click += new System.EventHandler(this.btnCodecsSelectAll_Click);
            // 
            // btnCodecsDeselectAll
            // 
            this.btnCodecsDeselectAll.Location = new System.Drawing.Point(79, 162);
            this.btnCodecsDeselectAll.Name = "btnCodecsDeselectAll";
            this.btnCodecsDeselectAll.Size = new System.Drawing.Size(60, 23);
            this.btnCodecsDeselectAll.TabIndex = 2;
            this.btnCodecsDeselectAll.Text = "Deselect all";
            this.btnCodecsDeselectAll.UseVisualStyleBackColor = true;
            this.btnCodecsDeselectAll.Click += new System.EventHandler(this.btnCodecsDeselectAll_Click);
            // 
            // lbCodecs
            // 
            this.lbCodecs.FormattingEnabled = true;
            this.lbCodecs.Location = new System.Drawing.Point(6, 17);
            this.lbCodecs.Name = "lbCodecs";
            this.lbCodecs.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbCodecs.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbCodecs.Size = new System.Drawing.Size(133, 134);
            this.lbCodecs.TabIndex = 0;
            this.lbCodecs.SelectedIndexChanged += new System.EventHandler(this.lbCodecs_SelectedIndexChanged);
            // 
            // gpRingback
            // 
            this.gpRingback.Controls.Add(this.tbRingbackTone);
            this.gpRingback.Controls.Add(this.lblRingBack);
            this.gpRingback.Controls.Add(this.btnRingbackTone);
            this.gpRingback.Location = new System.Drawing.Point(502, 320);
            this.gpRingback.Name = "gpRingback";
            this.gpRingback.Size = new System.Drawing.Size(489, 42);
            this.gpRingback.TabIndex = 39;
            this.gpRingback.TabStop = false;
            this.gpRingback.Text = "Ringback";
            // 
            // tbRingbackTone
            // 
            this.tbRingbackTone.Location = new System.Drawing.Point(38, 19);
            this.tbRingbackTone.Name = "tbRingbackTone";
            this.tbRingbackTone.ReadOnly = true;
            this.tbRingbackTone.Size = new System.Drawing.Size(401, 20);
            this.tbRingbackTone.TabIndex = 1;
            this.tbRingbackTone.Text = "Default";
            // 
            // lblRingBack
            // 
            this.lblRingBack.AutoSize = true;
            this.lblRingBack.Location = new System.Drawing.Point(6, 22);
            this.lblRingBack.Name = "lblRingBack";
            this.lblRingBack.Size = new System.Drawing.Size(26, 13);
            this.lblRingBack.TabIndex = 0;
            this.lblRingBack.Text = "File:";
            // 
            // btnRingbackTone
            // 
            this.btnRingbackTone.Location = new System.Drawing.Point(450, 16);
            this.btnRingbackTone.Name = "btnRingbackTone";
            this.btnRingbackTone.Size = new System.Drawing.Size(30, 23);
            this.btnRingbackTone.TabIndex = 9;
            this.btnRingbackTone.Text = "...";
            this.btnRingbackTone.UseVisualStyleBackColor = true;
            this.btnRingbackTone.Click += new System.EventHandler(this.btnRigbackTone_Click);
            // 
            // grpbKeepAlive
            // 
            this.grpbKeepAlive.Controls.Add(this.chbKeepAliveDisable);
            this.grpbKeepAlive.Controls.Add(this.nudKeepAlive);
            this.grpbKeepAlive.Controls.Add(this.lblKeepAliveInterval);
            this.grpbKeepAlive.Location = new System.Drawing.Point(692, 6);
            this.grpbKeepAlive.Name = "grpbKeepAlive";
            this.grpbKeepAlive.Size = new System.Drawing.Size(296, 46);
            this.grpbKeepAlive.TabIndex = 33;
            this.grpbKeepAlive.TabStop = false;
            this.grpbKeepAlive.Text = "Keep-Alive Settings";
            // 
            // chbKeepAliveDisable
            // 
            this.chbKeepAliveDisable.Appearance = System.Windows.Forms.Appearance.Button;
            this.chbKeepAliveDisable.Location = new System.Drawing.Point(215, 14);
            this.chbKeepAliveDisable.Name = "chbKeepAliveDisable";
            this.chbKeepAliveDisable.Size = new System.Drawing.Size(75, 23);
            this.chbKeepAliveDisable.TabIndex = 38;
            this.chbKeepAliveDisable.Text = "Disable";
            this.chbKeepAliveDisable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chbKeepAliveDisable.UseVisualStyleBackColor = true;
            this.chbKeepAliveDisable.CheckedChanged += new System.EventHandler(this.chbKeepAliveDisable_CheckedChanged);
            // 
            // nudKeepAlive
            // 
            this.nudKeepAlive.Location = new System.Drawing.Point(88, 17);
            this.nudKeepAlive.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.nudKeepAlive.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudKeepAlive.Name = "nudKeepAlive";
            this.nudKeepAlive.Size = new System.Drawing.Size(121, 20);
            this.nudKeepAlive.TabIndex = 38;
            this.nudKeepAlive.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudKeepAlive.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudKeepAlive.ValueChanged += new System.EventHandler(this.nudKeepAlive_ValueChanged);
            // 
            // lblKeepAliveInterval
            // 
            this.lblKeepAliveInterval.AutoSize = true;
            this.lblKeepAliveInterval.Location = new System.Drawing.Point(6, 22);
            this.lblKeepAliveInterval.Name = "lblKeepAliveInterval";
            this.lblKeepAliveInterval.Size = new System.Drawing.Size(76, 13);
            this.lblKeepAliveInterval.TabIndex = 0;
            this.lblKeepAliveInterval.Text = "Interval (secs):";
            // 
            // gpRingtone
            // 
            this.gpRingtone.Controls.Add(this.tbRingtoneName);
            this.gpRingtone.Controls.Add(this.lblRingtone);
            this.gpRingtone.Controls.Add(this.btnRingtone);
            this.gpRingtone.Location = new System.Drawing.Point(6, 320);
            this.gpRingtone.Name = "gpRingtone";
            this.gpRingtone.Size = new System.Drawing.Size(489, 42);
            this.gpRingtone.TabIndex = 38;
            this.gpRingtone.TabStop = false;
            this.gpRingtone.Text = "Ringtone";
            // 
            // tbRingtoneName
            // 
            this.tbRingtoneName.Location = new System.Drawing.Point(38, 19);
            this.tbRingtoneName.Name = "tbRingtoneName";
            this.tbRingtoneName.ReadOnly = true;
            this.tbRingtoneName.Size = new System.Drawing.Size(401, 20);
            this.tbRingtoneName.TabIndex = 1;
            this.tbRingtoneName.Text = "Default";
            // 
            // lblRingtone
            // 
            this.lblRingtone.AutoSize = true;
            this.lblRingtone.Location = new System.Drawing.Point(6, 22);
            this.lblRingtone.Name = "lblRingtone";
            this.lblRingtone.Size = new System.Drawing.Size(26, 13);
            this.lblRingtone.TabIndex = 0;
            this.lblRingtone.Text = "File:";
            // 
            // btnRingtone
            // 
            this.btnRingtone.Location = new System.Drawing.Point(450, 16);
            this.btnRingtone.Name = "btnRingtone";
            this.btnRingtone.Size = new System.Drawing.Size(30, 23);
            this.btnRingtone.TabIndex = 8;
            this.btnRingtone.Text = "...";
            this.btnRingtone.UseVisualStyleBackColor = true;
            this.btnRingtone.Click += new System.EventHandler(this.btnRingtone_Click);
            // 
            // tpSIPMessages
            // 
            this.tpSIPMessages.BackColor = System.Drawing.SystemColors.Control;
            this.tpSIPMessages.Controls.Add(this.rtbSIPMeassages);
            this.tpSIPMessages.Location = new System.Drawing.Point(4, 22);
            this.tpSIPMessages.Name = "tpSIPMessages";
            this.tpSIPMessages.Padding = new System.Windows.Forms.Padding(3);
            this.tpSIPMessages.Size = new System.Drawing.Size(994, 475);
            this.tpSIPMessages.TabIndex = 2;
            this.tpSIPMessages.Text = "SIP Messages";
            // 
            // rtbSIPMeassages
            // 
            this.rtbSIPMeassages.Location = new System.Drawing.Point(6, 6);
            this.rtbSIPMeassages.Name = "rtbSIPMeassages";
            this.rtbSIPMeassages.ReadOnly = true;
            this.rtbSIPMeassages.Size = new System.Drawing.Size(982, 463);
            this.rtbSIPMeassages.TabIndex = 0;
            this.rtbSIPMeassages.Text = "";
            // 
            // tpEvents
            // 
            this.tpEvents.Controls.Add(this.rtbEvents);
            this.tpEvents.Location = new System.Drawing.Point(4, 22);
            this.tpEvents.Name = "tpEvents";
            this.tpEvents.Padding = new System.Windows.Forms.Padding(3);
            this.tpEvents.Size = new System.Drawing.Size(994, 475);
            this.tpEvents.TabIndex = 3;
            this.tpEvents.Text = "Events";
            // 
            // rtbEvents
            // 
            this.rtbEvents.Location = new System.Drawing.Point(6, 6);
            this.rtbEvents.Name = "rtbEvents";
            this.rtbEvents.ReadOnly = true;
            this.rtbEvents.Size = new System.Drawing.Size(982, 463);
            this.rtbEvents.TabIndex = 1;
            this.rtbEvents.Text = "";
            // 
            // btnAbout
            // 
            this.btnAbout.BackColor = System.Drawing.Color.White;
            this.btnAbout.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnAbout.Location = new System.Drawing.Point(672, 13);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(80, 23);
            this.btnAbout.TabIndex = 18;
            this.btnAbout.Text = "About Ozeki";
            this.btnAbout.UseVisualStyleBackColor = false;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(7, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 25);
            this.label2.TabIndex = 40;
            this.label2.Text = "Ozeki VoIP SDK";
            // 
            // button1
            // 
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.ImageKey = "folder_verified.ico";
            this.button1.Location = new System.Drawing.Point(758, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 23);
            this.button1.TabIndex = 41;
            this.button1.Text = "Projects";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.ImageKey = "ie.ico";
            this.button2.Location = new System.Drawing.Point(844, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 23);
            this.button2.TabIndex = 42;
            this.button2.Text = "Website";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(930, 13);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(80, 23);
            this.button3.TabIndex = 43;
            this.button3.Text = "Help";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 548);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tcNetworkSettings);
            this.Controls.Add(this.btnAbout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1024, 576);
            this.MinimumSize = new System.Drawing.Size(1024, 576);
            this.Name = "MainForm";
            this.Text = "Ozeki VoIP SIP SDK - Demo Application";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tcNetworkSettings.ResumeLayout(false);
            this.tpSipSettings.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.remotevideopanel.ResumeLayout(false);
            this.grpbVideo.ResumeLayout(false);
            this.grpbVideo.PerformLayout();
            this.videoPanel.ResumeLayout(false);
            this.grpbMicrophone.ResumeLayout(false);
            this.grpbMicrophone.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trckbMicrophoneVolumeControl)).EndInit();
            this.grpbRecordFile.ResumeLayout(false);
            this.grpbRecordFile.PerformLayout();
            this.grpbSpeaker.ResumeLayout(false);
            this.grpbSpeaker.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trckbSpeakerVolumeControl)).EndInit();
            this.grpbPlayFile.ResumeLayout(false);
            this.grpbPlayFile.PerformLayout();
            this.grpbSipPhone.ResumeLayout(false);
            this.grpbSipPhone.PerformLayout();
            this.grpPhoneLines.ResumeLayout(false);
            this.tpNatSettings.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpbDSPs.ResumeLayout(false);
            this.grpbDSPs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxAGCGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAECDelay)).EndInit();
            this.grpbCodecs.ResumeLayout(false);
            this.gpRingback.ResumeLayout(false);
            this.gpRingback.PerformLayout();
            this.grpbKeepAlive.ResumeLayout(false);
            this.grpbKeepAlive.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudKeepAlive)).EndInit();
            this.gpRingtone.ResumeLayout(false);
            this.gpRingtone.PerformLayout();
            this.tpSIPMessages.ResumeLayout(false);
            this.tpEvents.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tcNetworkSettings;
        private System.Windows.Forms.TabPage tpSipSettings;
        private System.Windows.Forms.TabPage tpNatSettings;
        private System.Windows.Forms.GroupBox grpbSipPhone;
        private System.Windows.Forms.GroupBox grpbKeepAlive;
        private System.Windows.Forms.GroupBox grpbPlayFile;
        private System.Windows.Forms.GroupBox grpbRecordFile;
        private System.Windows.Forms.GroupBox grpbCodecs;
        private System.Windows.Forms.Label lblKeepAliveInterval;
        private System.Windows.Forms.Label lblPalyFile;
        private System.Windows.Forms.Button btnPlayFileStart;
        private System.Windows.Forms.Button btnPlayFileStop;
        private System.Windows.Forms.TextBox tbPlayFile;
        private System.Windows.Forms.Button btnPlayFileBrowse;
        private System.Windows.Forms.TabPage tpSIPMessages;
        private System.Windows.Forms.Button btnRecordFileStop;
        private System.Windows.Forms.Button btnRecordFileStart;
        private System.Windows.Forms.Button btnRecordFileBrowse;
        private System.Windows.Forms.TextBox tbRecordFile;
        private System.Windows.Forms.Label lblRecordFile;
        private System.Windows.Forms.ListBox lbCodecs;
        private System.Windows.Forms.Button btnCodecsSelectAll;
        private System.Windows.Forms.Button btnCodecsDeselectAll;
        private System.Windows.Forms.Button btnSIPUnregister;
        private System.Windows.Forms.Button btnSIPRegister;
        private System.Windows.Forms.Label lblTransport;
        private System.Windows.Forms.ComboBox cobTransport;
        private System.Windows.Forms.CheckBox chkbEnableLogging;
        private System.Windows.Forms.Label lblDomainName;
        private System.Windows.Forms.TextBox tbDomainServer;
        private System.Windows.Forms.TextBox tbRegisterPassword;
        private System.Windows.Forms.Label lblRegisterPassword;
        private System.Windows.Forms.TextBox tbRegisterName;
        private System.Windows.Forms.Label lblRegisterName;
        private System.Windows.Forms.TextBox tbDisplayName;
        private System.Windows.Forms.Label lblDisplayName;
        private System.Windows.Forms.Label lblUserId;
        private System.Windows.Forms.TextBox tbUserId;
        private System.Windows.Forms.TextBox tbNatPassword;
        private System.Windows.Forms.TextBox tbNatUserName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblNatUsername;
        private System.Windows.Forms.TextBox tbNatServer;
        private System.Windows.Forms.Label lblNatServer;
        private System.Windows.Forms.ComboBox cobNatType;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RichTextBox rtbSIPMeassages;
        private System.Windows.Forms.CheckBox chkbRegistrationRequired;
        private System.Windows.Forms.ListBox lbPhoneCalls;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox lbPhoneLines;
        private System.Windows.Forms.Button btnPager;
        private System.Windows.Forms.Button btnTransfer;
        private System.Windows.Forms.Button btnHangUp;
        private System.Windows.Forms.Button btnPickUp;
        private System.Windows.Forms.Button btnKeypad8;
        private System.Windows.Forms.Button btnKeypad6;
        private System.Windows.Forms.Button btnKeypad5;
        private System.Windows.Forms.Button btnKeypad9;
        private System.Windows.Forms.Button btnKeypadSharp;
        private System.Windows.Forms.Button btnKeypadAsterisk;
        private System.Windows.Forms.Button btnKeypad7;
        private System.Windows.Forms.Button btnKeypad0;
        private System.Windows.Forms.Button btnKeypad3;
        private System.Windows.Forms.Button btnKeypad4;
        private System.Windows.Forms.Button btnKeypad2;
        private System.Windows.Forms.Button btnKeypad1;
        private System.Windows.Forms.GroupBox grpbSpeaker;
        private System.Windows.Forms.GroupBox grpbMicrophone;
        private System.Windows.Forms.ComboBox cobSpeakerDevice;
        private System.Windows.Forms.Label lblSpeakerDevice;
        private System.Windows.Forms.CheckBox chkbSpeakerMute;
        private System.Windows.Forms.Label lblSpeakerVolume;
        private System.Windows.Forms.TrackBar trckbSpeakerVolumeControl;
        private System.Windows.Forms.ProgressBar pbSpeakerLevel;
        private System.Windows.Forms.ProgressBar pbMicrophoneLevel;
        private System.Windows.Forms.CheckBox chkbATAMode;
        private System.Windows.Forms.Label lblMicrophoneVolume;
        private System.Windows.Forms.TrackBar trckbMicrophoneVolumeControl;
        private System.Windows.Forms.CheckBox chkbMicrophoneMute;
        private System.Windows.Forms.ComboBox cobMicrophoneDevice;
        private System.Windows.Forms.Label lblMicrophoneDevice;
        private System.Windows.Forms.GroupBox grpPhoneLines;
        private System.Windows.Forms.Button btnPhoneLine6;
        private System.Windows.Forms.Button btnPhoneLine5;
        private System.Windows.Forms.Button btnPhoneLine4;
        private System.Windows.Forms.Button btnPhoneLine3;
        private System.Windows.Forms.Button btnPhoneLine2;
        private System.Windows.Forms.Button btnPhoneLine1;
        internal System.Windows.Forms.TextBox tbPhoneStatus;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.CheckBox chbxToolTip;
        private System.Windows.Forms.GroupBox grpbDSPs;
        private System.Windows.Forms.CheckBox chkbDenoise;
        private System.Windows.Forms.CheckBox chkbAGC;
        private System.Windows.Forms.CheckBox chkbAEC;
        private System.Windows.Forms.Label lDelay;
        private System.Windows.Forms.NumericUpDown numAECDelay;
        private System.Windows.Forms.Label lMaxAGCGain;
        private System.Windows.Forms.NumericUpDown numMaxAGCGain;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.CheckBox chbDND;
        private System.Windows.Forms.Button btnHold;
        private System.Windows.Forms.NumericUpDown nudKeepAlive;
        private System.Windows.Forms.CheckBox chbKeepAliveDisable;
        private System.Windows.Forms.TabPage tpEvents;
        private System.Windows.Forms.RichTextBox rtbEvents;
        private System.Windows.Forms.Button btnRedial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbCallHistory;
        private System.Windows.Forms.CheckBox chkbAutoAnswer;
        private System.Windows.Forms.TextBox tbProxy;
        private System.Windows.Forms.Label lblOutboundProxy;
        private System.Windows.Forms.Label lblMessageWaitingIndication;
        private System.Windows.Forms.Button btnRingbackTone;
        private System.Windows.Forms.Button btnRingtone;
        private System.Windows.Forms.GroupBox gpRingtone;
        private System.Windows.Forms.TextBox tbRingtoneName;
        private System.Windows.Forms.Label lblRingtone;
        private System.Windows.Forms.GroupBox gpRingback;
        private System.Windows.Forms.TextBox tbRingbackTone;
        private System.Windows.Forms.Label lblRingBack;
        internal System.Windows.Forms.TextBox tbDialNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox grpbVideo;
        private System.Windows.Forms.Label lVideoResolutions;
        private System.Windows.Forms.ComboBox cobVideoResolutions;
        private System.Windows.Forms.Button btnStopVideo;
        private System.Windows.Forms.Button btnStartVideo;
        private System.Windows.Forms.Label lVideoDevices;
        private System.Windows.Forms.ComboBox cobVideoDevices;
        private System.Windows.Forms.Panel videoPanel;
        private Ozeki.Media.Video.Controls.VideoViewerWF videoViewerLocal;
        private System.Windows.Forms.Button btnVideoLocalTest;
        private System.Windows.Forms.Panel remotevideopanel;
        private Ozeki.Media.Video.Controls.VideoViewerWF videoViewerRemote;
        private System.Windows.Forms.ComboBox cobAdaptersIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAdapterApply;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkForward;
        private System.Windows.Forms.ComboBox cobSRTPmode;
        private System.Windows.Forms.Label label4;
    }
}

