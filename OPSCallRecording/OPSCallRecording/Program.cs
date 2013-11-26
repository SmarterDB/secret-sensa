using System;
using System.Collections.Concurrent;
using System.IO;
using OPSSDK;
using OPSSDKCommon.Model.Call;
using Ozeki.Media.MediaHandlers;

namespace OPSCallRecording
{
    class Program
    {
        static OpsClient client;
        static string prompt = string.Empty;
        static ConcurrentDictionary<ISession, MP3StreamRecorder> currentCalls = new ConcurrentDictionary<ISession, MP3StreamRecorder>();
 
        static void Main(string[] args)
        {
            ShowGreetingMessage();
            ReadLoginInfos();
            Console.ReadLine();
        }

        private static void ShowGreetingMessage()
        {
            Console.WriteLine("This is a simple Ozeki Phone System XE demo written in C#.");
            Console.WriteLine("It can be used to record calls by custom conditions on Ozeki Phone System XE.");
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

        static void client_SessionCreated(object sender, Ozeki.VoIP.VoIPEventArgs<ISession> e)
        {
            Console.WriteLine("Call created. Source: {0}, caller id: {1}, dialed number: {2}, destination: {3}", e.Item.Source, e.Item.CallerId, e.Item.DialedNumber, e.Item.Destination);
            CreateCallRecorder(e.Item);
        }

        static void client_SessionCompleted(object sender, Ozeki.VoIP.VoIPEventArgs<ISession> e)
        {
            Console.WriteLine("Call completed. Source: {0}, caller id: {1}, dialed number: {2}, destination: {3}", e.Item.Source, e.Item.CallerId, e.Item.DialedNumber, e.Item.Destination);
            Console.WriteLine();
            DisposeCallRecorder(e.Item);
        }

        static void CreateCallRecorder(ISession session)
        {
            try
            {
                var fileName = string.Format("{0}_{1}_{2}.mp3", session.CallerId, session.Destination, session.SessionID);
                Console.WriteLine("Call will be recorded to the following path: {0}.", Path.Combine(Directory.GetCurrentDirectory(), fileName));

                var mp3Recorder = currentCalls.GetOrAdd(session, (s) => new MP3StreamRecorder(fileName));
                session.ConnectAudioReceiver(CallParty.All, mp3Recorder);
                mp3Recorder.StartStreaming();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void DisposeCallRecorder(ISession session)
        {
            try
            {
                MP3StreamRecorder recorder;
                if(currentCalls.TryGetValue(session, out recorder))
                {

                    recorder.Dispose();
                    session.DisconnectAudioReceiver(CallParty.All, recorder);
                }
       
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
