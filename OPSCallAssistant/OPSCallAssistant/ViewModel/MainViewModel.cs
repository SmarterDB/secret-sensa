using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Forms;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using OPSCallAssistant.Model;
using OPSSDK;
using OPSSDKCommon.Model;
using OPSSDKCommon.Model.Call;
using Ozeki.VoIP;
using IClient = OPSCallAssistant.Model.IClient;
using OPSCallAssistant.Utils;
using Ozeki.Media.MediaHandlers;
using System.Reflection;
using System.IO;
using System.Threading;
using System.Diagnostics;


namespace OPSCallAssistant.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public static string ShowCall = Guid.NewGuid().ToString();

        IClient _client;
        ISettingsRepository _settingsRepository;
        object _sync;
        ICollection<PhoneBookItem> _internalPhoneBookItems;
        PhoneBookItem _currentUser;
        private ICall initiatedCall;
        Random random;
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            try
            {
                random = new Random();
                _sync = new object();
                _client = SimpleIoc.Default.GetInstance<IClient>();
                _settingsRepository = SimpleIoc.Default.GetInstance<ISettingsRepository>();
                
                apiExt = _client.GetAPIExtension(null);

                CallCommand = new RelayCommand<string>(CallPressed,
                                                       phoneNumber => !string.IsNullOrEmpty(UsedPhoneNumber) && !string.IsNullOrEmpty(phoneNumber));

                _internalPhoneBookItems = new List<PhoneBookItem>();
                PhoneBookItems = CollectionViewSource.GetDefaultView(_internalPhoneBookItems);

                _client.PhoneBookChanged += OnPhoneBookChanged;
                _client.SessionCreated += ClientOnSessionCreated;
                _client.ErrorOccurred += ClientOnErrorOccurred;
                _client.GetPhoneBook();
            }
            catch (Exception ex)
            {

            }
        }

        private void ClientOnErrorOccurred(object sender, VoIPEventArgs<ErrorInfo> e)
        {
            Process.Start(Assembly.GetEntryAssembly().Location, "-reconnect");
            Environment.Exit(0);
        }

        private void CallPressed(string phoneNumber)
        {
            Logger.Log("Calling " + phoneNumber);

            WaveStreamPlayback waveStream = null;
            
            initiatedCall = apiExt.CreateCall(UsedPhoneNumber, phoneNumber, phoneNumber);
            if (initiatedCall == null)
                return;
            bool transferStarted = false;
            initiatedCall.CallStateChanged += (sender, e) =>
            {
                try
                {
                    
                    if (e.Item == CallState.Answered)
                    {
                        if (transferStarted)
                        {
                            initiatedCall.DisconnectAudioSender(waveStream);
                            waveStream.Dispose();

                            var tts = new TextToSpeech();

                            tts.Stopped += (sender1, e1)=>{
                                                              tts.Dispose();
                                                              initiatedCall.HangUp();
                                                          };

                            initiatedCall.ConnectAudioSender(tts);
                            tts.AddAndStartText(string.Format("Calling {0} has failed. Please try again later.", phoneNumber));
                            return;
                        }

                        var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OPSCallAssistant.Resources.ringback.wav");
                        waveStream = new WaveStreamPlayback(stream, true, true);

                        initiatedCall.ConnectAudioSender(waveStream);
                        waveStream.StartStreaming();

                        transferStarted = true;
                        initiatedCall.BlindTransfer(phoneNumber);
             
                    }
                    if (e.Item.IsCallEnded())
                    {
                        if (waveStream != null)
                            waveStream.Dispose();
                    }
                }
                catch(Exception ex)
                {
                    Logger.Log(ex.Message);
                    Logger.Log(ex.StackTrace);
                }

                var k = 65;

            };
            initiatedCall.Start();
        }

        
        private void ClientOnSessionCreated(object sender, VoIPEventArgs<ISession> e)
        {
            var session = e.Item;
            Logger.Log(string.Format("Session created: source:{0}, callerId:{1}, dialed:{2}, destination:{3}", e.Item.Source, e.Item.CallerId, e.Item.DialedNumber, e.Item.Destination));
            
            if (CurrentUser != null && (CurrentUser.Extensions.Contains(e.Item.Source) || CurrentUser.Extensions.Contains(e.Item.Destination)))
            {
                Logger.Log("Current user will be notified.");

                    try
                    {

                        var url = string.Format(_settingsRepository.GetUserInfo().ServerURL, Uri.EscapeDataString(e.Item.Source),
                                           Uri.EscapeDataString(e.Item.CallerId), Uri.EscapeDataString(e.Item.DialedNumber), 
                                           Uri.EscapeDataString(e.Item.Destination), Uri.EscapeDataString(e.Item.CallDirection.ToString()), 
                                           Uri.EscapeDataString(UsedPhoneNumber), Uri.EscapeDataString(e.Item.SessionID));

                        new Uri(url);

                        Messenger.Default.Send(new NotificationMessage<object>(new CallInfo(e.Item, url, UsedPhoneNumber), ShowCall));
                    }
                    catch(Exception ex)
                    {
                        Logger.Log(ex.Message);
                        Logger.Log(ex.StackTrace);
                    }

            }
            else
                Logger.Log("Current user will not be notified.");
        }

        private void OnPhoneBookChanged(object sender, VoIPEventArgs<List<PhoneBookItem>> e)
        {
            Logger.Log("Phonebook changed.");

            UpdatePhoneBook(e.Item);
        }

        private void UpdatePhoneBook(ICollection<PhoneBookItem> phoneBook)
        {
            lock (_sync)
            {
                var currentUser = phoneBook.FirstOrDefault(i => i.Username == _client.User.Username);
                if (currentUser == null)
                {
                    LoginInfo = "Current user does not exists in phonebook";
                }
                else
                {
                    phoneBook.Remove(currentUser);
                    LoginInfo =
                        string.Format(
                            "{0} logged in\nUsed phone number in assistant: ",
                            currentUser.Name);
                    CurrentUser = currentUser;

                    var userInfo = _settingsRepository.GetUserInfo();

                    if (!string.IsNullOrEmpty(userInfo.CurrentPhoneNumber) && currentUser.Extensions.Contains(userInfo.CurrentPhoneNumber))
                        UsedPhoneNumber = userInfo.CurrentPhoneNumber;
                    else
                        UsedPhoneNumber = currentUser.Extensions.FirstOrDefault();
                  
                }

                _internalPhoneBookItems = phoneBook;
                PhoneBookItems = CollectionViewSource.GetDefaultView(phoneBook);
            }
        }

        #region Properties

        ICollectionView _phoneBookItems;
        public ICollectionView PhoneBookItems
        {
            get { return _phoneBookItems; }
            private set
            {
                _phoneBookItems = value;
                _phoneBookItems.Filter = FilterItem;

                RaisePropertyChanged(() => PhoneBookItems);
            }
        }

        string _filter;
        public string Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                PhoneBookItems.Refresh();
            }
        }

        bool FilterItem(object o)
        {
            var phoneBookItem = (PhoneBookItem)o;

            if (string.IsNullOrEmpty(Filter))
                return true;

            return phoneBookItem.Name.ToLower().Contains(Filter.ToLower());
        }

        string _loginInfo;
        public string LoginInfo
        {
            get { return _loginInfo; }
            set
            {
                _loginInfo = value;
                RaisePropertyChanged(() => LoginInfo);
            }
        }

        PhoneBookItem _selectedUser;
        public PhoneBookItem SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;

                RaisePropertyChanged(() => SelectedUser);
                //SelectedPhoneNumber = _selectedUser.PhoneNumbers.FirstOrDefault();
            }
        }

        string _selectedPhoneNumber;
        private IAPIExtension apiExt;

        public string SelectedPhoneNumber
        {
            get { return _selectedPhoneNumber; }
            set
            {

                _selectedPhoneNumber = value;
                RaisePropertyChanged(() => SelectedPhoneNumber);
            }
        }


        public PhoneBookItem CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                RaisePropertyChanged(() => CurrentUser);
            }
        }

        string _usedPhoneNumber;
        public string UsedPhoneNumber
        {
            get { return _usedPhoneNumber; }
            set
            {
                _usedPhoneNumber = value;
                
                var userInfo = _settingsRepository.GetUserInfo();
                userInfo.CurrentPhoneNumber = value;

                _settingsRepository.SetUserInfo(userInfo);

                RaisePropertyChanged(() => UsedPhoneNumber);
            }
        }

        public RelayCommand<string> CallCommand { get; private set; }

        #endregion
    }
}