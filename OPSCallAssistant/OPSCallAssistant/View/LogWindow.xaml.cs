using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OPSCallAssistant.Utils;
using System.Collections.ObjectModel;

namespace OPSCallAssistant.View
{
    /// <summary>
    /// Interaction logic for LogWindow.xaml
    /// </summary>
    public partial class LogWindow : Window
    {

        object sync;
        public LogWindow()
        {
            Logs = new ObservableCollection<string>(Logger.LastMessages);
            sync = new object();
            DataContext = this;
            InitializeComponent();
            Logger.LogReceived += Logger_LogReceived;
        }

        public ObservableCollection<string> Logs { get; set; }

        void Logger_LogReceived(object sender, OPSSDKCommon.GenericEventArgs<string> e)
        {
            lock (sync)
            {
                if (Logs.Count > 500)
                {
                    while (Logs.Count > 400)
                        Logs.RemoveAt(0);
                }

                if (Dispatcher.CheckAccess())
                    AddLog(e.Item);
                else
                    Dispatcher.BeginInvoke(new Action(() =>{AddLog(e.Item);}));
            }
        }

        private void AddLog(string message)
        {
            lock (sync)
                Logs.Add(message);
        }

        private void Window_Closing(object sender, EventArgs e)
        {
            Logger.LogReceived -= Logger_LogReceived;

        }
    }
}
