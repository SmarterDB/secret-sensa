using System;
using Ozeki.VoIP;

namespace ozeki.voip.sip.client
{
    //These classes is just an example how to use OZEKI VoIP SIP SDK, what dll is added to the References in Solution Explorer
    //Feel free to use the other components of this SDK
    //For More information please visit : http://www.voip-sip-sdk.com and http://www.ozekiphone.com/ozeki-voip-sip-client-997.html

    class TestCall
    {
        private static CallHandlerSample _callHandlerSample;

        static void Main(string[] args)
        {
            _callHandlerSample = new CallHandlerSample("888", "192.168.113.13");

            SubscribeCallHandlerSampleEvents();

            Console.ReadLine();
            UnsubscribeCallHandlerSampleEvents();
        }

        static void callHandlerSample_IncomingCallReceived(object sender, VoIPEventArgs<IPhoneCall> incomingCall)
        {
            incomingCall.Item.Accept();
        }

        private static void callHandlerSample_RegistrationSucceded(object sender, EventArgs e)
        {
            _callHandlerSample.Call("1001");
        }

        private static void SubscribeCallHandlerSampleEvents()
        {
            _callHandlerSample.RegistrationSucceded += callHandlerSample_RegistrationSucceded;
            _callHandlerSample.IncomingCallReceived += callHandlerSample_IncomingCallReceived;
        }

        private static void UnsubscribeCallHandlerSampleEvents()
        {
            _callHandlerSample.RegistrationSucceded -= callHandlerSample_RegistrationSucceded;
            _callHandlerSample.IncomingCallReceived -= callHandlerSample_IncomingCallReceived;
        }
    }
}
