using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using OzekiDemoSoftphone.GUI;
using OzekiDemoSoftphone.PM;
using OzekiDemoSoftphone.Softphone;
using Ozeki.Media.MediaHandlers.Video;

namespace OzekiDemoSoftphone
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                //Application.Run(new ViewForm());
                //Application.Run(new RandomEventViewForm());
                
                SoftphoneEngine softPhone = SoftphoneEngine.Instance;
                MainForm mainForm = new MainForm(softPhone);
                Application.Run(mainForm);
            }
            catch (Exception e)
            {
                var sb = new StringBuilder();
                sb.AppendLine("Some error happened.");
                sb.AppendLine();
                sb.AppendLine("Exception:");
                sb.AppendLine(e.Message);
                sb.AppendLine();
                if(e.InnerException != null)
                {
                    sb.AppendLine("Inner Exception:");
                    sb.AppendLine(e.InnerException.Message);
                    sb.AppendLine();
                }
                sb.AppendLine("StackTrace:");
                sb.AppendLine(e.StackTrace);

                MessageBox.Show(sb.ToString());
            }
            finally
            {
                Application.Exit();
            }
        }
    }
}
