
using OPSSDK;
namespace OPSCallAssistant.Model
{
    public class CallInfo
    {
        public CallInfo()
        {
            
        }

        public CallInfo(ISession session, string url, string usedPhoneNumber)
        {
            Session = session;
            Url = url;
            UsedPhoneNumber = usedPhoneNumber;
        }

        public ISession Session { get; private set; }
        public string Url { get; private set; }
        public string UsedPhoneNumber { get; private set; }
       
    }
}
