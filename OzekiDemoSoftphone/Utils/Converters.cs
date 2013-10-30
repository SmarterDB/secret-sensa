using System;
using Ozeki.Network;
using Ozeki.VoIP;
using OzekiDemoSoftphone.PM.Data;

namespace OzekiDemoSoftphone.Utils
{
    /// <summary>
    /// Data, or information converters.
    /// </summary>
    static class Converters
    {
        /// <summary>
        /// Phone line info as SIPAccount.
        /// </summary>
        /// <param name="pli">The provided phone line information.</param>
        /// <returns>An SIP account.</returns>
        public static SIPAccount AsSIPAccount(this PhoneLineInfo pli)
        {

            string domain;
            int port = 5060;
            string[] splittedDomain = pli.Domain.Split(':');
            if (splittedDomain.Length > 1)
                port = Int32.Parse(splittedDomain[1]);

            domain = splittedDomain[0];
            SIPAccount acc = new SIPAccount(
                pli.RegRequired,
                pli.DisplayName,
                pli.Username,
                pli.RegisterName,
                pli.Password,
                domain,
                port,
                pli.Proxy
            );

            return acc;
        }

        /// <summary>
        /// Extracts information from IPhoneLine object.
        /// </summary>
        /// <param name="line">The phone line object as information source.</param>
        /// <returns>The information about phone line.</returns>
        public static PhoneLineInfo AsPhoneLineInfo(this IPhoneLine line)
        {
            return new PhoneLineInfo(
                line.SIPAccount.DisplayName,
                line.SIPAccount.UserName,
                line.SIPAccount.RegisterName,
                line.SIPAccount.RegisterPassword,
                line.SIPAccount.DomainServerPort != 5060 ? (line.SIPAccount.DomainServerHost + ":" + line.SIPAccount.DomainServerPort) : line.SIPAccount.DomainServerHost,
                line.SIPAccount.OutboundProxy,
                line.SIPAccount.RegistrationRequired,
                line.TransportType,
                line.SRTPMode
            );
        }

        /// <summary>
        /// Extracts information from IPhoneCall object.
        /// </summary>
        /// <param name="call">The phone call object as information source.</param>
        /// <returns>The information about phone call.</returns>
        public static PhoneCallInfo AsPhoneCallInfo(this IPhoneCall call)
        {
            PhoneLineInfo pli = call.PhoneLine.AsPhoneLineInfo();
            CallDirection type = (call.IsIncoming) ? CallDirection.Incoming : CallDirection.Outgoing;
            return new PhoneCallInfo(call.CallID, pli, call.OtherParty.UserName, type);
        }
    }
}
