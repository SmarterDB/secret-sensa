using System;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using OPSCallAssistant.Model;
using Ozeki.VoIP;
using IClient = OPSCallAssistant.Model.IClient;
using System.Timers;

namespace OPSCallAssistant.ViewModel
{
    class LoginViewModel : ViewModelBase
    {
        public static readonly string ShowWaitWindow = Guid.NewGuid().ToString();
        public static readonly string ShowLoginWindow = Guid.NewGuid().ToString();

        public static readonly string ShowError = Guid.NewGuid().ToString();
        public static readonly string NavigateToMainWindow = Guid.NewGuid().ToString();

        public static readonly string Reconnect = Guid.NewGuid().ToString();


        IClient _client;
        ISettingsRepository settingsRepository;
        Timer reconnectionTimer;
        Random rand;
        int reconnectionTime;
        public LoginViewModel()
        {
            _client = SimpleIoc.Default.GetInstance<IClient>();
            settingsRepository = SimpleIoc.Default.GetInstance<ISettingsRepository>();

            UserInfo = settingsRepository.GetUserInfo();

            if (!string.IsNullOrEmpty(UserInfo.Username))
                RememberMe = true;

            
            Login = new RelayCommand(() =>
                                         {
                                             canceled = false;
                                             LoginToServer();
                                         }, () => UserInfo.IsValid);

            _client.LoginCompleted += LoginCompleted;
            rand = new Random();

            InitReconnectionTimer();
        }

        void InitReconnectionTimer()
        {
            reconnectionTime += rand.Next(5000, 10000);
            reconnectionTimer = new Timer(reconnectionTime);
            reconnectionTimer.Elapsed += timer_Elapsed;
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            reconnectionTimer.Dispose();

            if (canceled)
                return;

            _client.Login(UserInfo);
        }

        public RelayCommand Login { get; private set; }
        public UserInfo UserInfo { get; private set; }
        public bool RememberMe { get; set; }

        public void Init()
        {
            if (UserInfo.Username != null && (App.Args.Length == 0 || (App.Args.Length == 1 && App.Args[0] == "-reconnect")))
                LoginToServer();
            else
                Messenger.Default.Send(new NotificationMessage(ShowLoginWindow));
        }

        bool canceled;
        public void CancelLogin()
        {
            reconnectionTimer.Dispose();

            canceled = true;
            _client.Logout();
        }

        private void LoginToServer()
        {
            Messenger.Default.Send(new NotificationMessage(ShowWaitWindow));

            _client.Login(UserInfo);

        }

        private void LoginCompleted(object sender, VoIPEventArgs<LoginResult> e)
        {
            switch (e.Item)
            {
                case LoginResult.Success:
                    if(RememberMe)
                        settingsRepository.SetUserInfo(UserInfo);
                    else
                        settingsRepository.SetUserInfo(null);

                    _client.LoginCompleted -= LoginCompleted;

                    Messenger.Default.Send(new NotificationMessage(NavigateToMainWindow));
                    break;
                case LoginResult.ConnectionFailure:
                    InitReconnectionTimer();
                    Messenger.Default.Send(new NotificationMessage<string>(string.Format("Connection failed. Trying to reconnect after {0} seconds.", reconnectionTime / 1000), Reconnect));
                    reconnectionTimer.Start();
                    break;
                case LoginResult.VersionMismatch:
                    Messenger.Default.Send(new NotificationMessage<string>("Ozeki Phone System SDK version mismatch", ShowError));
                    break;
                case LoginResult.UsernameOrPassword:
                    Messenger.Default.Send(new NotificationMessage<string>("Bad username or password", ShowError));
                    break;
                case LoginResult.UnkownError:
                    Messenger.Default.Send(new NotificationMessage<string>("Unknown error", ShowError));
                    break;
            }


        }
       
    }
}
