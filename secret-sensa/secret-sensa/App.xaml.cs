using System;
using System.Text;
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
        public static bool Validate(string propertyName, string value)
        {
            if (value == null || string.IsNullOrEmpty(value.Trim()))
            {
                MessageBox.Show(string.Format("{0} cannot be empty!", propertyName));
                return false;
            }

            return true;
        }

        public static void Register(SoftphoneEngine Model)
        {
            if (!Validate("User name", Model.AccountModel.UserName))
                return;

            if (!Validate("Register name", Model.AccountModel.RegisterName))
                return;

            if (!Validate("Domain", Model.AccountModel.Domain))
                return;

            var line = Model.AddPhoneLine(Model.AccountModel.SIPAccount, Model.AccountModel.TransportType, Model.AccountModel.NatConfig, Model.AccountModel.SRTPMode);

            if (Model.SelectedLine == null)
                Model.SelectedLine = line;

            try
            {
                Model.RegisterPhoneLine();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                SoftphoneEngine model = new SoftphoneEngine();

                MainWindow window = new MainWindow(model);

                Register(model);

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
