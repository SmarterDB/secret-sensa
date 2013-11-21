
using System;
using System.Collections.Generic;
using OPSSDK;
using OPSSDKCommon.Model.Route;

namespace OPSRoutingInterception
{
    class MyCallRoutingInterceptor : ICallRoutingInterceptor
    {
        List<string> dndExtensions;
        OpsClient _client;

        public MyCallRoutingInterceptor(OpsClient client)
        {
             dndExtensions = new List<string>();
             _client = client;
             var ext0 = _client.GetAPIExtension();
             var ext1 = _client.GetAPIExtension("1000");
             var ext2 = _client.GetAPIExtension("9999");
             var server = _client.GetAPIExtension("server");
             var pgnotebook = _client.GetAPIExtension("pgnotebook");

             //server.IncomingCall += server_IncomingCall;
             //server.Disconnected += server_Disconnected;
             //pgnotebook.IncomingCall += pgnotebook_IncomingCall;
             //pgnotebook.Disconnected += pgnotebook_Disconnected;
        }

        public Destination GetDestination(RouteParams routeParams)
        {
            Console.WriteLine("Call is being routed with the following parameters: owner: {0}, caller id: {1}, dialed number: {2}, state: {3}", routeParams.CallerInfo.Owner, routeParams.CallerInfo.CallerId, routeParams.CallerInfo.DialedNumber, routeParams.State);

            //var lst = client.GetExtensionInfos();
            //var lst2 = client.GetPhoneBook();
            //var sessions = _client.GetActiveSessions();

            

            //if (routeParams.Destination.DialedNumber == "*90" && !dndExtensions.Contains(routeParams.CallerInfo.Owner))
            //{
            //    Console.WriteLine("Extension {0} added to the DND list.", routeParams.CallerInfo.Owner);
            //    dndExtensions.Add(routeParams.CallerInfo.Owner);
            //}

            //if (routeParams.Destination.DialedNumber == "*91" && dndExtensions.Contains(routeParams.CallerInfo.Owner))
            //{
            //    Console.WriteLine("Extension {0} removed from the DND list.", routeParams.CallerInfo.Owner);
            //    dndExtensions.Remove(routeParams.CallerInfo.Owner);
            //}

            //if (dndExtensions.Contains(routeParams.Destination.DialedNumber))
            //{
            //    return new Destination(routeParams.CallerInfo.Owner, null, null, 10);
            //}

            return null;
        }

        void pgnotebook_Disconnected(object sender, EventArgs e)
        {
            Console.WriteLine("pg notebook Disconnected: {0}", e);
        }

        void pgnotebook_IncomingCall(object sender, Ozeki.VoIP.VoIPEventArgs<ICall> e)
        {
            Console.WriteLine("pg notebook Incoming call: {0}", e);
        }

        void server_Disconnected(object sender, EventArgs e)
        {
            Console.WriteLine("Server Disconnected: {0}", e);
        }

        void server_IncomingCall(object sender, Ozeki.VoIP.VoIPEventArgs<ICall> e)
        {
            Console.WriteLine("Server Incoming call: {0}", e);
        }
    }
}
