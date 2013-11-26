using System.Windows.Controls;
using OPSCallAssistant.Model;

namespace OPSCallAssistant.View
{
    /// <summary>
    /// Interaction logic for IncomingCallPopup.xaml
    /// </summary>
    public partial class IncomingCallPopup : UserControl
    {
        public IncomingCallPopup(CallInfo content)
        {
            InitializeComponent();
            DataContext = content;
        }
    }
}
