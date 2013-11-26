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
using OPSCallAssistant.Model;
using GalaSoft.MvvmLight.Ioc;

namespace OPSCallAssistant.View
{
    /// <summary>
    /// Interaction logic for ConfigWindow.xaml
    /// </summary>
    public partial class ConfigWindow : Window
    {
        ISettingsRepository settingsRepository;
        UserInfo userInfo;

        public ConfigWindow()
        {
            InitializeComponent();
            settingsRepository = SimpleIoc.Default.GetInstance<ISettingsRepository>();

            userInfo = settingsRepository.GetUserInfo();
            ServerUrl.Text = userInfo.ServerURL;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new Uri(string.Format(ServerUrl.Text, "4234", "545435", "325", "5345", "423423", "234324", "2343242"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ServerUrl.Text = userInfo.ServerURL;
                return;
            }

            userInfo.ServerURL = ServerUrl.Text;

            settingsRepository.SetUserInfo(userInfo);
            //TODO save
            Close();
        }
    }
}
