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
using Ozeki.Network.Nat;

namespace OzekiDemoSoftphoneWPF.GUI
{
    /// <summary>
    /// Interaction logic for NatDiscoveryWindow.xaml
    /// </summary>
    public partial class NatDiscoveryWindow : Window
    {
        public NatDiscoveryWindow(Window owner)
        {
            Owner = owner;
            InitializeComponent();
        }
    }
}
