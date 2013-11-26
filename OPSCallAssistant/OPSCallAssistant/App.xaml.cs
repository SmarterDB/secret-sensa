using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using OPSCallAssistant.Model;
using OPSCallAssistant.Utils;
using OPSCallAssistant.View;
using Awesomium.Core;

namespace OPSCallAssistant
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly SingletonApplicationEnforcer enforcer;
        private Dispatcher dispatcher;
        public App()
        {
            enforcer = new SingletonApplicationEnforcer(DisplayArgs, "OPSCallAssistant");
            ShutdownMode = ShutdownMode.OnLastWindowClose;
            
        }

        public static string[] Args;

        

        protected override void OnStartup(StartupEventArgs e)
        {
            GalaSoft.MvvmLight.Ioc.SimpleIoc.Default.Register<IClient>(() => new RealClient());    
            
            
            GalaSoft.MvvmLight.Ioc.SimpleIoc.Default.Register<ISettingsRepository>(() => new SettingsRepository());
            Args = e.Args;
            
            if (Args.Length == 0)
            {
                if (enforcer.ShouldApplicationExit())
                {
                    Environment.Exit(0);
                }
            }

            Current.MainWindow = new LoginWindow();
            dispatcher = Current.MainWindow.Dispatcher;
            Current.MainWindow.Show();

            base.OnStartup(e);

        }

        public void DisplayArgs(IEnumerable<string> args)
        {
            Args = args.ToArray();

            if (Args.Length == 2)
            {
                if (enforcer.ShouldApplicationExit())
                {
                    Environment.Exit(0);
                }
            }
            else
            {
                dispatcher.Invoke(new Action(() =>
                                                                        {
                                                                            var window = Current.MainWindow as IWindow;

                                                                            if (window != null)
                                                                                window.ShowWindow();
                                                                        }));


            }
        }
    }
}
