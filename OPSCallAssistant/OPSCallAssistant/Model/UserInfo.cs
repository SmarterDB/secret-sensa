using System.ComponentModel;
namespace OPSCallAssistant.Model
{
    public class UserInfo : IDataErrorInfo
    {
        public UserInfo()
        {
            ServerURL = "http://vir.ozeki.hu/index.php?owpn=300&source={0}&callerId={1}&dialedNumber={2}&destination={3}&direction={4}&managedExtId={5}&callId={6}";
        }

        public string ServerAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ServerURL { get; set; }
        public string CurrentPhoneNumber { get; set; }

        public string this[string columnName]
        {
            get
            {

                switch (columnName)
                {
                    case "ServerAddress":
                        if (string.IsNullOrEmpty(ServerAddress))
                            return "This field is required";
                        break;
                    case "Username":
                        if (string.IsNullOrWhiteSpace(Username))
                            return "This field is required";
                        break;
                    case "Password":
                        if (string.IsNullOrWhiteSpace(Password))
                            return "This field is required";
                        break;

                }

                return null;
            }
        }

        public string Error { get; set; }

        public bool IsValid
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password) &&
                       !string.IsNullOrWhiteSpace(ServerAddress);
            }
        }
    }
}
