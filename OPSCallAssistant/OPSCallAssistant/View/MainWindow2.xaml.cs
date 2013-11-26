using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using OPSCallAssistant.ViewModel;

// Import some references
using System.Runtime.InteropServices;
using System.Windows.Interop;
using OPSCallAssistant.Model;
using System.Windows.Controls;

namespace OPSCallAssistant.View
{
    /// <summary>
    /// Interaction logic for MainWindow2.xaml
    /// </summary>
    public partial class MainWindow2 : Window, IWindow
    {
        CallNotification callNotification;
        LogWindow logWindow;
 
        // Place these inside the class definition...
        [DllImport("user32.dll")]
        private extern static Int32 SetWindowLong(IntPtr hWnd, Int32 nIndex, Int32 dwNewLong);
        [DllImport("user32.dll")]
        private extern static Int32 GetWindowLong(IntPtr hWnd, Int32 nIndex);
 
        const Int32 GWL_STYLE = -16;
        const Int32 WS_MAXIMIZEBOX = 0x10000;
        const Int32 WS_MINIMIZEBOX = 0x20000;

        public MainWindow2()
        {
            Loaded += new RoutedEventHandler(MainWindow2_Loaded);
            Closed += new EventHandler(MainWindow2_Closed);
            InitializeComponent();
            callNotification = new CallNotification();
            callNotification.Init();

            if (!(App.Args.Length == 0 || (App.Args.Length == 1 && App.Args[0] == "-reconnect")))
                ShowWindow();

            Messenger.Default.Register<NotificationMessage<object>>(this, MessageReceived);
        }

        void MainWindow2_Closed(object sender, EventArgs e)
        {
            taskBarIcon.Dispose();
        }

        void MainWindow2_Loaded(object sender, RoutedEventArgs e)
        {
            IntPtr hWnd = new WindowInteropHelper(this).Handle;
            Int32 windowLong = GetWindowLong(hWnd, GWL_STYLE);
            windowLong = windowLong & ~WS_MAXIMIZEBOX;
            SetWindowLong(hWnd, GWL_STYLE, windowLong);
        }

        private void MessageReceived(NotificationMessage<object> notificationMessage)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                if (notificationMessage.Notification == MainViewModel.ShowCall)
                {
                   callNotification.LoadPage((CallInfo)notificationMessage.Content);
                    //var incomingCallPopUp =
                    //  new IncomingCallPopup((CallInfo) notificationMessage.Content);
                    //taskBarIcon.ShowCustomBalloon(incomingCallPopUp,
                    //                            PopupAnimation.Fade, 10000);


                }
            }));
            
        }

        private void HelpClick(object sender, RoutedEventArgs e)
        {
            Process.Start("http://www.ozekiphone.com");
        }

        private void AboutClick(object sender, RoutedEventArgs e)
        {
            AboutWindow window = new AboutWindow();
            window.Owner = this;
            window.ShowDialog();
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("Are you sure you want to exit?", Constants.ApplicationName, MessageBoxButton.YesNo);

            if(res == MessageBoxResult.Yes)
                Environment.Exit(0);
        }

        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            Logout();
        }

        private void Logout()
        {
            var res = MessageBox.Show("Are you sure you want to logout?", Constants.ApplicationName, MessageBoxButton.YesNo);

            if (res != MessageBoxResult.Yes)
                return;

            Process.Start(Assembly.GetEntryAssembly().Location, "-logout");
            Environment.Exit(0);
        }

        private void MainWindow2_OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Hide(); 
        }

        private void ActivateClick(object sender, RoutedEventArgs e)
        {
            ShowWindow();
        }

        public void ShowWindow()
        {
            Visibility = Visibility.Visible;
            WindowStyle = System.Windows.WindowStyle.ThreeDBorderWindow;
            Activate();
            ShowInTaskbar = true;            
            Show();
            WindowState = WindowState.Normal;
            Focus();
        }

        private void MainWindow2_OnStateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                Hide();
            }
        }

        private void ConfigClick(object sender, RoutedEventArgs e)
        {
            var config = new ConfigWindow();
            config.Owner = this;
            config.ShowDialog();
        }

        private void LogClick(object sender, RoutedEventArgs e)
        {
            if (logWindow != null)
            {
                logWindow.Activate();
                return;
            }


            logWindow = new LogWindow();
            logWindow.Closed += new EventHandler(logWindow_Closed);
            logWindow.Show();
        }

        void logWindow_Closed(object sender, EventArgs e)
        {
            logWindow = null;
        }

        private void Label_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var label = (Label)sender;

            var emailAddress = label.Content as string;

            if (string.IsNullOrEmpty(emailAddress))
                return;

            try
            {
                Process.Start("mailto://" + emailAddress);
            }
            catch
            {
            }
        }
    }
}
