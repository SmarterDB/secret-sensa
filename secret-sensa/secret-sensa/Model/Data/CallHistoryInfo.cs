using System;
using Ozeki.VoIP;

namespace secret_sensa.Model.Data
{
    public class CallHistoryInfo : EventArgs
    {
        public DateTime Date { get; private set; }
        public DialInfo PhoneLine { get; private set; }
        public DialInfo OtherParty { get; private set; }
        public bool IsIncoming { get; private set; }
        public CallState CallState { get; private set; }
        public string ReasonOfState { get; private set; }        

        public CallHistoryInfo(DialInfo caller, DialInfo callee, bool isIncoming, CallState callState, string reasonOfState)
        {
            Date = DateTime.Now;
            PhoneLine = caller;
            OtherParty = callee;
            IsIncoming = isIncoming;
            CallState = callState;
            ReasonOfState = reasonOfState;
        }
    }

    public static class CallHistoryInfoEx
    {
        public static CallHistoryInfo ToPhoneCallInfo(this IPhoneCall call)
        {
            SIPAccount account = call.PhoneLine.SIPAccount;
            DialInfo caller = new DialInfo(account.DisplayName, account.UserName, account.DomainServerHost);
            return new CallHistoryInfo(caller, call.DialInfo, call.IsIncoming, call.CallState, call.ReasonOfState);
        }
    }
}
