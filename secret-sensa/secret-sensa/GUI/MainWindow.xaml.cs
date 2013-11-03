using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using Ozeki.Media.Audio;
using Ozeki.Media.Codec;
using Ozeki.VoIP.Media;
using secret_sensa.GUI.GUIModels;
using secret_sensa.Model;
using Ozeki.VoIP;
using System;
using secret_sensa.Model.Data;
using TestSoftphone;
using System.ComponentModel;
using Ozeki.Media.Video;
using Ozeki.Network.Nat;
using System.Text;
using System.Collections.Generic;

namespace secret_sensa.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public SoftphoneEngine Model { get; private set; }
        public MediaHandlers MediaHandlers { get { return Model.MediaHandlers; } }

        #region GUI Properties

        //dinamikus ablakméret
        private int _height;
        public int CustomHeight
        {
            get { return _height; }
            set
            {
                if (value != _height)
                {
                    _height = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("CustomHeight"));
                }
            }
        }

        private void chkSettings_Click(object sender, RoutedEventArgs e)
        {
            if (this.Height == 750)
            {
                CustomHeight = 550;
            }
            else
            {
                CustomHeight = 750;
            }
        }

        public string TitleString
        {
            get { return string.Format("COMIC.SYS Ozeki Demo {0}", Assembly.GetExecutingAssembly().GetName().Version); }
        }

        public IPhoneLine SelectedLine
        {
            get { return Model.SelectedLine ; }
        }

        private string dialNumber;
        private string wavPlaybackFileName;
        private string wavRecordFileName;
        private string mp3PlaybackFileName;
        private string ringtoneFileName;
        private string ringbackFileName;
        private string recipient;
        private string instantMessage;

        public string WavPlaybackFileName
        {
            get { return wavPlaybackFileName; }
            private set
            {
                wavPlaybackFileName = value;
                OnNotifyPropertyChanged("WavPlaybackFileName");
            }
        }

        public string WavRecordFileName
        {
            get { return wavRecordFileName; }
            private set
            {
                wavRecordFileName = value;
                OnNotifyPropertyChanged("WavRecordFileName");
            }
        }

        public string MP3PlaybackFileName
        {
            get { return mp3PlaybackFileName; }
            private set
            {
                mp3PlaybackFileName = value;
                OnNotifyPropertyChanged("MP3PlaybackFileName");
            }
        }

        public string DialNumber
        {
            get { return dialNumber; }
            set
            {
                dialNumber = value;
                OnNotifyPropertyChanged("DialNumber");
            }
        }

        public string RingtoneFileName
        {
            get { return ringtoneFileName; }
            private set
            {
                ringtoneFileName = value;
                OnNotifyPropertyChanged("RingtoneFileName");
            }
        }

        public string RingbackFileName
        {
            get { return ringbackFileName; }
            private set
            {
                ringbackFileName = value;
                OnNotifyPropertyChanged("RingbackFileName");
            }
        }

        public string Recipient
        {
            get { return recipient; }
            set
            {
                recipient = value;
                OnNotifyPropertyChanged("Recipient");
            }
        }

        public string InstantMessage
        {
            get { return instantMessage; }
            set
            {
                instantMessage = value;
                OnNotifyPropertyChanged("InstantMessage");
            }
        }

        public List<KeepAliveMode> KeepAliveValues { get; private set; }
        public List<VideoQuality> VideoQualities { get; private set; }

        #endregion

        public MainWindow(SoftphoneEngine model)
        {
            Model = model;

            KeepAliveValues = new List<KeepAliveMode>();
            var keelAliveValues = Enum.GetValues(typeof(KeepAliveMode));
            foreach (KeepAliveMode mode in keelAliveValues)
                KeepAliveValues.Add(mode);

            VideoQualities = new List<VideoQuality>();
            var qualities = Enum.GetValues(typeof(VideoQuality));
            foreach (VideoQuality quality in qualities)
                VideoQualities.Add(quality);
            VideoEncoderQuality = VideoQuality.High;

            InitializeComponent();

            CustomHeight = 550;

            Model.PhoneLineStateChanged += (Model_PhoneLineStateChanged);
            Model.PhoneCallStateChanged += (Model_PhoneCallStateChanged);
            Model.MediaHandlers.MicrophoneStopped += MediaHandlers_MicrophoneStopped;
            Model.MediaHandlers.SpeakerStopped += MediaHandlers_SpeakerStopped;
        }

        #region Window events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            remoteVideoViewer.SetImageProvider(MediaHandlers.RemoteImageProvider);
            localVideoViewer.SetImageProvider(MediaHandlers.LocalImageProvider);

            remoteVideoViewer.Start();
            localVideoViewer.Start();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Model.PhoneLineStateChanged -= (Model_PhoneLineStateChanged);
            Model.PhoneCallStateChanged -= (Model_PhoneCallStateChanged);
            Model.Dispose();
        }

        #endregion

        #region Model events

        private void Model_PhoneLineStateChanged(object sender, GEventArgs<IPhoneLine> e)
        {
            tbPhoneLineStatus.Dispatcher.Invoke(new Action(() => tbPhoneLineStatus.GetBindingExpression(Label.ContentProperty).UpdateTarget()));
        }

        private void Model_PhoneCallStateChanged(object sender, GEventArgs<IPhoneCall> e)
        {
            UpdatePhoneCalls();
        }

        private void MediaHandlers_SpeakerStopped(object sender, EventArgs e)
        {
            RefreshSpeakerDevices();
        }

        private void MediaHandlers_MicrophoneStopped(object sender, EventArgs e)
        {
            RefreshMicrophoneDevices();
        }

        private void UpdatePhoneCalls()
        {
            lblSipAccount.Dispatcher.Invoke(new Action(() => lblSipAccount.GetBindingExpression(Label.ContentProperty).UpdateTarget()));
            lblOtherParty.Dispatcher.Invoke(new Action(() => lblOtherParty.GetBindingExpression(Label.ContentProperty).UpdateTarget()));
            lblIsIncoming.Dispatcher.Invoke(new Action(() => lblIsIncoming.GetBindingExpression(Label.ContentProperty).UpdateTarget()));
            lblReasonOfState.Dispatcher.Invoke(new Action(() => lblReasonOfState.GetBindingExpression(Label.ContentProperty).UpdateTarget()));
        }

        #endregion

        #region Dialpad

        private void btnDialVideo_Click(object sender, RoutedEventArgs e)
        {
            //uno button für alles
            if (Model.SelectedCall != null)
            {
                btnAnswer_Click(sender, e);
            }
            else
            {
                Dial(CallType.AudioVideo, false);
            }
        }

        private void Dial(CallType callType, bool dialIP)
        {
            if (SelectedLine == null)
                return;

            if (!SelectedLine.LineState.IsRegistered())
            {
                MessageBox.Show("Cannot start calls while the selected line is not registered.");
                return;
            }
            try
            {
                if (!dialIP)
                    Model.Dial(DialNumber, callType);
                else
                    Model.DialIP(DialNumber, callType);
            }
            catch (Exception ex)
            {
                ShowLicenseError(ex.Message);
            }

            DialNumber = string.Empty;
        }

        #endregion

        #region Phone Call Functions

        private void btnAnswer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Model.AnswerCall();
            }
            catch (Exception ex)
            {
                ShowLicenseError(ex.Message);
            }
        }

        private void btnHangup_Click(object sender, RoutedEventArgs e)
        {
            Model.HangUpCall();
        }

        #endregion

        #region Microphone, Speaker

        private void btnRefreshMicrophones_Click(object sender, RoutedEventArgs e)
        {
            RefreshMicrophoneDevices();
        }

        private void RefreshMicrophoneDevices()
        {
            Dispatcher.BeginInvoke(new Action(() => cbMicrophoneDevices.GetBindingExpression(ComboBox.ItemsSourceProperty).UpdateTarget()));
        }

        private void cbMicrophoneDevices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeviceInfo selected = cbMicrophoneDevices.SelectedItem as DeviceInfo;
            if (selected == null)
                return;

            MediaHandlers.ChangeMicrophone(selected);
        }

        private void btnRefreshSpeakers_Click(object sender, RoutedEventArgs e)
        {
            RefreshSpeakerDevices();
        }

        private void RefreshSpeakerDevices()
        {
            Dispatcher.BeginInvoke(new Action(() => cbSpeakerDevices.GetBindingExpression(ComboBox.ItemsSourceProperty).UpdateTarget()));
        }

        private void cbSpeakerDevices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeviceInfo selected = cbSpeakerDevices.SelectedItem as DeviceInfo;
            if (selected == null)
                return;

            MediaHandlers.ChangeSpeaker(selected);
        }

        #endregion


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnNotifyPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Camera

        private void cbCameraDevices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VideoDeviceInfo deviceInfo = cbCameraDevices.SelectedItem as VideoDeviceInfo;
            if (deviceInfo == null)
                return;

            MediaHandlers.ChangeCamera(deviceInfo.DeviceID);
        }

        private void btnRefreshCameras_Click(object sender, RoutedEventArgs e)
        {
            cbCameraDevices.GetBindingExpression(ComboBox.ItemsSourceProperty).UpdateTarget();
        }

        private void btnStartVideo_Click(object sender, RoutedEventArgs e)
        {
            // start camera
            MediaHandlers.StartVideo();

            // start GUI control
            localVideoViewer.Start();
        }

        private void btnStopVideo_Click(object sender, RoutedEventArgs e)
        {
            // stop camera
            MediaHandlers.StopVideo();

            // stop GUI control
            localVideoViewer.Stop();
        }

        #endregion


        #region Codecs

        private void CodecCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox == null)
                return;

            // determine payload type        
            int payloadType;
            if (!int.TryParse(checkBox.Tag.ToString(), out payloadType))
                return;

            // enable codec
            Model.EnableCodec(payloadType);
        }

        private void CodecCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox == null)
                return;

            // determine payload type        
            int payloadType;
            if (!int.TryParse(checkBox.Tag.ToString(), out payloadType))
                return;

            // disable codec
            Model.DisableCodec(payloadType);
        }

        private void btnSelectAllAudioCodecs_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in lbAudioCodecs.Items)
            {
                CodecInfo info = item as CodecInfo;
                if (info == null)
                    continue;

                Model.EnableCodec(info.PayloadType);
            }

            lbAudioCodecs.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
        }

        private void btnDeselectAllAudioCodecs_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in lbAudioCodecs.Items)
            {
                CodecInfo info = item as CodecInfo;
                if (info == null)
                    continue;

                Model.DisableCodec(info.PayloadType);
            }

            lbAudioCodecs.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
        }

        private void btnSelectAllVideoCodecs_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in lbVideoCodecs.Items)
            {
                CodecInfo info = item as CodecInfo;
                if (info == null)
                    continue;

                Model.EnableCodec(info.PayloadType);
            }

            lbVideoCodecs.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
        }

        private void btnDeselectAllVideoCodecs_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in lbVideoCodecs.Items)
            {
                CodecInfo info = item as CodecInfo;
                if (info == null)
                    continue;

                Model.DisableCodec(info.PayloadType);
            }

            lbVideoCodecs.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
        }

        VideoQuality videoEncoderQuality;
        public VideoQuality VideoEncoderQuality
        {
            get { return videoEncoderQuality; }
            set
            {
                videoEncoderQuality = value;
                Model.SetVideoEncoderQuality(videoEncoderQuality);
            }
        }

        #endregion

        #region Network

        public bool UseFixIP { get; set; }

        //private void btnApply_Click(object sender, RoutedEventArgs e)
        //{
        //    Model.InitSoftphone(UseFixIP);
        //}

        #endregion

        private void ShowLicenseError(string message)
        {
            MessageBox.Show(message, "Ozeki VoIP SIP SDK", MessageBoxButton.OK, MessageBoxImage.Error);
        }


    }
}
