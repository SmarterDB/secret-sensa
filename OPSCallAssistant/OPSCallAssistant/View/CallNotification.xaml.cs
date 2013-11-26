using System;
using System.Windows.Forms;
using Awesomium.Core;
using System.Windows;
using System.Diagnostics;
using System.Collections.Generic;
using OPSCallAssistant.Model;
using OPSSDKCommon.Model.Call;
using OPSCallAssistant.Utils;
using System.ComponentModel;

namespace OPSCallAssistant.View
{
    /// <summary>
    /// Interaction logic for CallNotification.xaml
    /// </summary>
    public partial class CallNotification : Window, INotifyPropertyChanged
    {
        private string currentUrl;
        Queue<Action> pendingAction;


        public CallNotification()
        {
            pendingAction = new Queue<Action>();
            HideWindow();
            InitializeComponent();

            WebView.LoadingFrameComplete += WebView_LoadingFrameComplete;
            WebView.DocumentReady += WebView_DocumentReady;
        }

        Uri _source;
        public Uri Source { 
                             get { return _source; } 
                             set {
                                   _source = value;

                                   var handler = PropertyChanged;
                                   
                                   if(handler != null)
                                     handler(this, new PropertyChangedEventArgs("Source"));
                                 } 
                          }

        void WebView_LoadingFrameComplete(object sender, FrameEventArgs e)
        {
            if (!e.IsMainFrame)
                return;

            var k = 5;
        }

        void WebView_DocumentReady(object sender, UrlEventArgs e)
        {

            Logger.Log("Document ready");

            if (e.HasErrors)
            {
                Logger.Log("Document ready error");
                return;
            }

            if (WebView.Title == "Error")
                return;

            documentReady = true;
            CreateCallbacks();
            ShowWindow();
            ExecutePendingActions();

        }

        private void CreateCallbacks()
        {
            Logger.Log("Creating external callbacks");

            JSObject external = WebView.CreateGlobalJavascriptObject("external");

            external.Bind("openBrowser", false, OpenBrowser);
            external.Bind("close", false, CloseWindow);
            external.Bind("transferTo", false, TransferTo);
        }

        private void TransferTo(object sender, JavascriptMethodEventArgs e)
        {
            try
            {
          
                if (e.Arguments.Length != 1)
                {
                    Logger.Log("JS transfer to cannot be executed. Parameter list is not valid");
                    return;
                }
      

                var target = e.Arguments[0].ToString();
                Logger.Log("Js transfer to: " + target);

                var session = callInfo.Session;

                if (session.State == OPSSDKCommon.Model.Session.SessionState.Ringing || session.State == OPSSDKCommon.Model.Session.SessionState.Created || session.State == OPSSDKCommon.Model.Session.SessionState.Setup)
                {
                    Logger.Log("Call is ringing. Call forwarding to " + target);
                    session.Forward(target);
                }
                else
                {
                    CallParty callParty;
                    if (callInfo.UsedPhoneNumber == session.Source)
                        callParty = CallParty.Caller;
                    else
                        callParty = CallParty.Callee;


                    Logger.Log(string.Format("Call transferrring to {0} transferor {1}", target, callParty));
                    //TODO music
                    session.BlindTransfer(callParty, target);
                }


            }
            catch(Exception ex)
            {
                Logger.Log(ex.Message);
                Logger.Log(ex.StackTrace);
            }
        }

        private void CloseWindow(object sender, JavascriptMethodEventArgs e)
        {
            Logger.Log("js CloseWindow");
            HideWindow();
        }

        private void OpenBrowser(object sender, JavascriptMethodEventArgs e)
        {
            try
            {
                if(e.Arguments.Length != 1)
                    return;

                Process.Start(e.Arguments[0].ToString());
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                Logger.Log(ex.StackTrace);
            }
        }

        private double GetDocumentHeight()
        {

            string result = string.Empty;

            try
            {
                result = WebView.ExecuteJavascriptWithResult("(function(){return document.body.style.height})()");
                return int.Parse(result.ToString().ToLower().Replace("px", string.Empty));
            }
            catch (Exception)
            {
                Logger.Log("Unable to parse body height: " + result.ToString());
                return 300;
            }

        }

        private double GetDocumentWidth()
        {

            string result = string.Empty;

            try
            {
                result = WebView.ExecuteJavascriptWithResult("(function(){return document.body.style.width})()");
                return int.Parse(result.ToString().ToLower().Replace("px", string.Empty));
            }
            catch (Exception)
            {
                Logger.Log("Unable to parse body width: " + result.ToString());
                return 300;
            }

        }

        CallInfo callInfo;
        bool documentReady;
        public void LoadPage(CallInfo callInfo)
        {
            Logger.Log("Loading page");

            HideWindow();
            ClearPendingActions();
            Show();

            if(callInfo != null)
                callInfo.Session.SessionStateChanged -= Session_SessionStateChanged;


            this.callInfo = callInfo;
            currentUrl = callInfo.Url;
            callInfo.Session.SessionStateChanged += Session_SessionStateChanged;

            documentReady = false;

            Logger.Log("Loading current url: " + currentUrl);


            Source = new Uri(currentUrl);
          
            //WebView.Reload(true);
        }

        void Session_SessionStateChanged(object sender, Ozeki.VoIP.VoIPEventArgs<OPSSDKCommon.Model.Session.SessionState> e)
        {
            Dispatcher.BeginInvoke(new Action(() => {
                                                        try
                                                        {
                                                            Logger.Log("Call state changed: " + e.Item);

                                                            var action = new Action(() =>
                                                            {
                                                                Logger.Log("Js notify call state changed: " + e.Item);
                                                                WebView.ExecuteJavascript("(function(){try{callStateChanged('" + e.Item.ToString() + "');}catch(err){}})()");
                                                            });
                                                            if (documentReady)
                                                                action();
                                                            else
                                                            {
                                                                Logger.Log("Js cannot be executed while document is not ready.");
                                                                AddPendingAction(action);
                                                            }

                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            Logger.Log(ex.Message);
                                                            Logger.Log(ex.StackTrace);
                                                        }
                                                    }));
          
        }

 

        private void HideWindow()
        {
            Logger.Log("HideWindow");

            WindowStyle = WindowStyle.None;
            Visibility = Visibility.Hidden;
            ShowInTaskbar = false;
            ShowActivated = false;

            Hide();
        }

        private void ShowWindow()
        {
            Logger.Log("ShowWindow");
            Visibility = Visibility.Visible;

            WindowStyle = WindowStyle.ToolWindow;
            var width = GetDocumentWidth();
            var height = GetDocumentHeight();

            Logger.Log(string.Format("Body width: {0} height: {1}", width, height));

            var source = PresentationSource.FromVisual(App.Current.MainWindow);

            var dpiX = 96.0 * source.CompositionTarget.TransformToDevice.M11;
            var dpiY = 96.0 * source.CompositionTarget.TransformToDevice.M22;

            var scaleX = 96.0 / dpiX;
            var scaleY = 96.0 / dpiY;

            Width = width * scaleX;
            Height = height * scaleY;

            Left = (Screen.PrimaryScreen.WorkingArea.Width * scaleX - ActualWidth);
            Top = (Screen.PrimaryScreen.WorkingArea.Height * scaleY - ActualHeight);

  
            Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            HideWindow();

        }

        public void Init()
        {
            HideWindow();
            Show();
        }

       
        private void AddPendingAction(Action action)
        {
            Logger.Log("Add pedding action");
            pendingAction.Enqueue(action);
        }

        private void ClearPendingActions()
        {
            Logger.Log("Clear pending actions");
            pendingAction = new Queue<Action>();
        }


        private void ExecutePendingActions()
        {
            Logger.Log("Executes pending actions");

            while (pendingAction.Count > 0)
            {
                var action = pendingAction.Dequeue();
                action();
            }
 
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
