using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OPSSDK;

namespace OPSRoutingInterception
{
    class Program
    {
        static OpsClient client;
        static string prompt = string.Empty;

        static void Main(string[] args)
        {
            ShowGreetingMessage();
            ReadLoginInfos();
            SetRoutingInterceptor(client);
            Console.ReadLine();
        }

        private static void ShowGreetingMessage()
        {
            Console.WriteLine("This is a simple Ozeki Phone System XE demo written in C#.");
            Console.WriteLine("It can be used to create dynamic routing on Ozeki Phone System XE.");
            Console.WriteLine(@"The sample program sets ""do not disturb"" state and declines every call,");
            Console.WriteLine("when *90 is dialled.");
            Console.WriteLine("You can cancel this if you dial *91.");
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine();
        }

        private static void ReadLoginInfos()
        {
            Console.WriteLine("Please enter the IP address of Ozeki Phone System XE.");

            //var serverAddress = Read("Server address (default: 127.0.0.1)", false);

            //if (string.IsNullOrEmpty(serverAddress))
            //    serverAddress = "127.0.0.1";

            //Console.WriteLine("Please enter the username and password of the user created in Ozeki Phone System XE");
            //var username = Read("Username (default: admin)", false);

            //if (string.IsNullOrEmpty(username))
            //    username = "admin";

            //var password = Read("Password", true);
            var serverAddress = "ozekitest.cloudapp.net";
            var username = "admin";
            var password = "myKcuHiJjxFjODVNeymD";
            Login(serverAddress, username, password);
        }

        private static void SetRoutingInterceptor(OpsClient client)
        {
            client.SetCallRoutingInterceptor(new MyCallRoutingInterceptor(client));
        }

        private static void Login(string serverAddress, string username, string password)
        {
            client = new OpsClient();
            Console.WriteLine("Connecting...");

            client.ErrorOccurred += client_ErrorOccurred;
            var result = client.Login(serverAddress, username, password);

            if (result)
            {
                Console.WriteLine("Successfully connected to {0} with username: {1}.", serverAddress, username);
                client.SessionCreated += client_SessionCreated;
                client.SessionCompleted += client_SessionCompleted;
            }
            else
            {
                Console.WriteLine("Connection fail try again.");
                Console.WriteLine("Please check whether the IP address is correct and the given user has right to use OPS SDK. This can be checked under PBX features/Preferences/User access profile.");
                ReadLoginInfos();
            }

        }

        static void client_SessionCompleted(object sender, Ozeki.VoIP.VoIPEventArgs<ISession> e)
        {
            Console.WriteLine("SessionCompleted.CallDirection: {0}", e.Item.CallDirection);
            Console.WriteLine("SessionCompleted.CallerId: {0}", e.Item.CallerId);
            Console.WriteLine("SessionCompleted.Destination: {0}", e.Item.Destination);
            Console.WriteLine("SessionCompleted.DialedNumber: {0}", e.Item.DialedNumber);
            Console.WriteLine("SessionCompleted.RingDuration: {0}", e.Item.RingDuration);
            Console.WriteLine("SessionCompleted.SessionID: {0}", e.Item.SessionID);
            Console.WriteLine("SessionCompleted.Source: {0}", e.Item.Source);
            Console.WriteLine("SessionCompleted.StartTime: {0}", e.Item.StartTime);
            Console.WriteLine("SessionCompleted.State: {0}", e.Item.State);
            Console.WriteLine("SessionCompleted.StateDuration: {0}", e.Item.StateDuration);
            Console.WriteLine("SessionCompleted.TalkDuration: {0}", e.Item.TalkDuration);
        }

        static void client_SessionCreated(object sender, Ozeki.VoIP.VoIPEventArgs<ISession> e)
        {
            Console.WriteLine("SessionCreated.CallDirection: {0}", e.Item.CallDirection);
            Console.WriteLine("SessionCreated.CallerId: {0}", e.Item.CallerId);
            Console.WriteLine("SessionCreated.Destination: {0}", e.Item.Destination);
            Console.WriteLine("SessionCreated.DialedNumber: {0}", e.Item.DialedNumber);
            Console.WriteLine("SessionCreated.RingDuration: {0}", e.Item.RingDuration);
            Console.WriteLine("SessionCreated.SessionID: {0}", e.Item.SessionID);
            Console.WriteLine("SessionCreated.Source: {0}", e.Item.Source);
            Console.WriteLine("SessionCreated.StartTime: {0}", e.Item.StartTime);
            Console.WriteLine("SessionCreated.State: {0}", e.Item.State);
            Console.WriteLine("SessionCreated.StateDuration: {0}", e.Item.StateDuration);
            Console.WriteLine("SessionCreated.TalkDuration: {0}", e.Item.TalkDuration);
        }

        static void client_ErrorOccurred(object sender, ErrorInfo e)
        {
            Console.WriteLine(e.Message);
        }

        static string Read(string inputName, bool readWhileEmpty)
        {
            Console.Write(prompt + "" + inputName + ": ");
            while (true)
            {
                var input = Console.ReadLine();

                if (!readWhileEmpty)
                    return input;

                if (!string.IsNullOrEmpty(input))
                    return input;

                Console.WriteLine(inputName + " cannot be empty.");
                Console.Write(inputName + ": ");
            }
        }
    }
}
