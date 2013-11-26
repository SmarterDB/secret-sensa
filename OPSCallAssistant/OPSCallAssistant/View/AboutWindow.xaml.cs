using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;

namespace OPSCallAssistant.View
{
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();

            Closed += LoginWindow_Closed;
        }

        void LoginWindow_Closed(object sender, EventArgs e)
        {
            
        }

        private void OKClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AboutLinksRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(e.Uri.ToString());
        }
    }
}
