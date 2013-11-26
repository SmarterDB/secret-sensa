//using Ozeki.VoIP;
//using System;

//namespace ozeki.pbx.voip.client
//{
//    //These classes is just an example how to use OZEKI OPS SDK, what dll is added to the References in Solution Explorer
//    //Feel free to use the other components of this SDK
//    //For More information please visit : http://www.ozekiphone.com/-net-api-684.html
//    class TestCall
//    {
//        private static CallHandlerSample _callHandlerSample;

//        public static void Main(string[] args)
//        {
//            _callHandlerSample = new CallHandlerSample("localhost", "admin", "12345", "9000"); //Use your settings, and configured API Extension

//            _callHandlerSample.Call("1001");

//            SubscribeCallHandlerSampleEvents();

//            //Waiting for incoming Calls(IncomingCallReceived), or program end by user.
//            Console.ReadLine();
//            UnsubscribeCallHandlerSampleEvents();
//        }

//        private static void callHandlerSample_IncomingCallReceived(object sender, VoIPEventArgs<OPSSDK.ICall> e)
//        {
//            Console.WriteLine("Incoming call (" + e.Item.OtherParty + ") accepted.");
//            e.Item.Accept();
//        }

//        private static void SubscribeCallHandlerSampleEvents()
//        {
//            _callHandlerSample.IncomingCallReceived += callHandlerSample_IncomingCallReceived;
//        }

//        private static void UnsubscribeCallHandlerSampleEvents()
//        {
//            _callHandlerSample.IncomingCallReceived -= callHandlerSample_IncomingCallReceived;
//        }
//    }
//}
