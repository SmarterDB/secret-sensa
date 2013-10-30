using Ozeki.VoIP;
using System;

namespace OzekiDemoSoftphoneWPF.Model.Data
{
    public class PhoneCallInstantMessageArgs : EventArgs
    {
        public IPhoneCall PhoneCall { get; private set; }
        public MessageDataPackage Message { get; private set; }

        public PhoneCallInstantMessageArgs(IPhoneCall phoneCall, MessageDataPackage message)
        {
            PhoneCall = phoneCall;
            Message = message;
        }
    }
}
