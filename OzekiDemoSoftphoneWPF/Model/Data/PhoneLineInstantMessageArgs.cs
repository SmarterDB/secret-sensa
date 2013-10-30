using System;
using Ozeki.VoIP;

namespace OzekiDemoSoftphoneWPF.Model.Data
{
    public class PhoneLineInstantMessageArgs : EventArgs
    {
        public IPhoneLine PhoneLine { get; private set; }
        public MessageDataPackage Message { get; private set; }

        public PhoneLineInstantMessageArgs(IPhoneLine phoneLine, MessageDataPackage message)
        {
            PhoneLine = phoneLine;
            Message = message;
        }
    }
}
