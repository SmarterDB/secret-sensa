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
using Ozeki.VoIP;
using secret_sensa.GUI.GUIModels;
using secret_sensa.Model;

namespace secret_sensa.GUI
{
    /// <summary>
    /// Interaction logic for TransferWindow.xaml
    /// </summary>
    public partial class TransferWindow : Window
    {
        public TransferModel Model { get; set; }

        public TransferWindow(Window owner, TransferModel model)
        {
            Owner = owner;
            Model = model;
            InitializeComponent();
        }

        private void btnBlindTransfer_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
