using System;
using System.Collections.Concurrent;
using System.IO;
using OPSSDK;
using OPSSDKCommon.Model.Call;
using Ozeki.Media.MediaHandlers;

namespace OPSCallLogging {
    class Program
    {
        static OpsClient client;
        static string prompt = string.Empty;
        

        static void Main(string[] args)
        {
            ShowGreetingMessage();
            ReadLoginInfos();
            Console.ReadLine();
        }

        private static void ShowGreetingMessage()
        {
            Console.WriteLine("This is a simple Ozeki Phone System XE demo written in C#.");
            Console.WriteLine("It can be used to log calls by custom conditions on Ozeki Phone System XE.");
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine();
        }

        private static void ReadLoginInfos()
        {
            Console.WriteLine("Please enter the IP address of Ozeki Phone System XE.");

            var serverAddress = Read("Server address (default: 127.0.0.1)", false);

            if (string.IsNullOrEmpty(serverAddress))
                serverAddress = "127.0.0.1";

            Console.WriteLine("Please enter the username and password of the user created in Ozeki Phone System XE");
            var username = Read("Username (default: admin)", false);

            if (string.IsNullOrEmpty(username))
                username = "admin";

            var password = Read("Password", true);
            Login(serverAddress, username, password);
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
                client.SessionCreated += new EventHandler<Ozeki.VoIP.VoIPEventArgs<OPSSDK.ISession>>(opsClient_SessionCreated);
                
            }
            else
            {
                Console.WriteLine("Connection fail try again.");
                Console.WriteLine("Please check whether the IP address is correct and the given user has right to use OPS SDK. This can be checked under PBX features/Preferences/User access profile.");
                ReadLoginInfos();
            }

        }
              

        static void opsClient_SessionCreated(object sender, Ozeki.VoIP.VoIPEventArgs<OPSSDK.ISession> e)
        {
	        Console.Clear();
            Console.WriteLine("Source: " + e.Item.Source);
            Console.WriteLine("SessionID: " + e.Item.SessionID);
	        Console.WriteLine("Call direction: " + e.Item.CallDirection);
	        Console.WriteLine("Caller: " + e.Item.CallerId );
            Console.WriteLine("Callee: " + e.Item.DialedNumber);
	        Console.WriteLine("Ring duration: " + e.Item.RingDuration);
	        Console.WriteLine("State: " + e.Item.State);
	        Console.WriteLine("State duration: " + e.Item.StateDuration);
	        Console.WriteLine("Start time: " + e.Item.StartTime);
	        Console.WriteLine("Talk duration: " + e.Item.TalkDuration);
            Console.WriteLine("Destination: "  + e.Item.Destination);
        
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
