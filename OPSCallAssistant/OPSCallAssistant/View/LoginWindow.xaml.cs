using System;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using OPSCallAssistant.Utils;
using OPSCallAssistant.ViewModel;
using System.Windows.Forms;
using Hardcodet.Wpf.TaskbarNotification;

namespace OPSCallAssistant.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window, IWindow
    {
        WaitWindow waitWindow;
        LoginViewModel viewModel;
        bool waitWindowVisible;
        bool showWaitWindow;

        public LoginWindow()
        {
            InitializeComponent();
       

            Messenger.Default.Register<NotificationMessage>(this, MessageReceived);
            Messenger.Default.Register<NotificationMessage<string>>(this, MessageReceivedS);
            viewModel = (LoginViewModel)DataContext;
            viewModel.Init();

            Closing += new System.ComponentModel.CancelEventHandler(LoginWindow_Closing);
            ServerAddress.Focus();
        }

        void LoginWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Messenger.Default.Unregister<NotificationMessage>(this, MessageReceived);
            Messenger.Default.Unregister<NotificationMessage<string>>(this, MessageReceivedS);

            taskBarIcon.Dispose();
        }


        private void MessageReceived(NotificationMessage notificationMessage)
        {
            Dispatcher.BeginInvoke(new Action(() =>
                                       {
                                           if (notificationMessage.Notification == LoginViewModel.ShowLoginWindow)
                                           {
                                               ShowWindow();
                                           }
                                           else if (notificationMessage.Notification == LoginViewModel.ShowWaitWindow)
                                           {
                                               showWaitWindow = true;
                                               if (ShowActivated)
                                               {
                                                   ShowWaitWindow();
                                                   
                                               }
                                               else
                                               {
                                                   //var serverAddress = viewModel.UserInfo.ServerAddress;
                                                   //taskBarIcon.ShowBalloonTip("Ozeki call assistant", "Trying to connect to " + serverAddress, BalloonIcon.Info);
                                               }
                                              
                                           }
                                           else if (notificationMessage.Notification == LoginViewModel.NavigateToMainWindow)
                                           {
                                               CloseWaitWindow();

                                               var mainWindow = new MainWindow2();
                                               mainWindow.Show();

                                               System.Windows.Application.Current.MainWindow = mainWindow;
                                    
                                               Close();
                                           }
                                       }));
          
        }


        private void MessageReceivedS(NotificationMessage<string> message)
        {
            Dispatcher.BeginInvoke(new Action(() => {
                                                        if (message.Notification == LoginViewModel.ShowError)
                                                        {
                                                            showWaitWindow = false;
                                                            CloseWaitWindow();

                                                            System.Windows.MessageBox.Show(message.Content, "Ozeki call assistant", MessageBoxButton.OK, MessageBoxImage.Error);
                                                        }
                                                        else if (message.Notification == LoginViewModel.Reconnect)
                                                        {
                                                            if (ShowActivated)
                                                                ShowWaitWindow();
                                                            

                                                            taskBarIcon.ShowBalloonTip("Ozeki call assistant", message.Content, BalloonIcon.Info);


                                                        }
                                                    }));
        }

        private void CloseWaitWindow()
        {
            waitWindowVisible = false;
            try
            {
                if (waitWindow != null)
                {
                    waitWindow.Closed -= WaitWindowClosed;
                    waitWindow.Close();
                    waitWindow = null;
                }
            }
            catch
            { 
            }
            
        }

        private void ShowWaitWindow()
        {
            if (waitWindowVisible)
                return;

            waitWindowVisible = true;

            waitWindow = new WaitWindow();
            waitWindow.Closed += WaitWindowClosed;
            waitWindow.Owner = this;
            waitWindow.ShowDialog();
        }

        private void WaitWindowClosed(object sender, EventArgs empty)
        {
            waitWindowVisible = false;
            viewModel.CancelLogin();
            //TODO cancel login
        }

        public void ShowWindow()
        {
            WindowStyle = System.Windows.WindowStyle.ThreeDBorderWindow;
            ShowActivated = true;
            WindowState = System.Windows.WindowState.Normal;
            Visibility = Visibility.Visible;
            ShowInTaskbar = true;
            Activate();
            Show();
        }

        void taskBarIcon_TrayLeftMouseUp(object sender, RoutedEventArgs e)
        {
            ShowWindow();

            if (showWaitWindow)
                ShowWaitWindow();
            

        }
    }
}
