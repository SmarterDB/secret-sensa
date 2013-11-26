using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OPSSDKCommon;
using System.Collections.Concurrent;

namespace OPSCallAssistant.Utils
{
    static class Logger
    {
        static Logger()
        {
            LastMessages = new ConcurrentQueue<string>();
        }

        public static ConcurrentQueue<string> LastMessages { get; private set; }
 
        public static event EventHandler<GenericEventArgs<string>> LogReceived;

        public static void Log(string message)
        {
            AddMessage(message);

            var handler = LogReceived;

            if (handler != null)
                handler(null, new GenericEventArgs<string>(message));
        }

        static void AddMessage(string message)
        {
            if (LastMessages.Count > 100)
            {
                while (LastMessages.Count > 50)
                {
                    string removable;

                    LastMessages.TryDequeue(out removable);
                }
            }

            LastMessages.Enqueue(message);

        }
    }
}
