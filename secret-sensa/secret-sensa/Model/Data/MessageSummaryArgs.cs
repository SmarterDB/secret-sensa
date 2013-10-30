using System;
using Ozeki.VoIP;
using Ozeki.VoIP.MessageSummary;

namespace secret_sensa.Model.Data
{
    public class MessageSummaryArgs : EventArgs
    {
        public IPhoneLine PhoneLine { get; private set; }
        public VoIPMessageSummary MessageSummary { get; private set; }

        public MessageSummaryArgs(IPhoneLine phoneLine, VoIPMessageSummary messageSummary)
        {
            PhoneLine = phoneLine;
            MessageSummary = messageSummary;
        }
    }
}
