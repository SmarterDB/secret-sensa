using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using secret_sensa.GUI;
using secret_sensa.Model;

namespace secret_sensa
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                SoftphoneEngine model = new SoftphoneEngine();

                MainWindow window = new MainWindow(model);
                window.Show();
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Could not initialize softphone: \r\n");
                sb.Append(ex.Message);
                sb.Append("\r\n");
                sb.Append(ex.InnerException);
                sb.Append(ex.StackTrace);
                MessageBox.Show(sb.ToString());
                Application.Current.Shutdown();
            }

            base.OnStartup(e);
        }
    }
}
