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
using Ozeki.VoIP.MessageSummary;

namespace secret_sensa.GUI
{
    /// <summary>
    /// Interaction logic for MessageSummaryWindow.xaml
    /// </summary>
    public partial class MessageSummaryWindow : Window
    {
        public VoIPMessageSummary MessageSummary
        {
            get;
            private set;
        }

        public MessageSummaryWindow(Window owner, VoIPMessageSummary messageSummary)
        {
            Owner = owner;
            MessageSummary = messageSummary;

            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
