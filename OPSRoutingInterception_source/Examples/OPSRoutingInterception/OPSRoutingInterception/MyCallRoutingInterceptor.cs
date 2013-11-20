
using System;
using System.Collections.Generic;
using OPSSDK;
using OPSSDKCommon.Model.Route;

namespace OPSRoutingInterception
{
    class MyCallRoutingInterceptor : ICallRoutingInterceptor
    {
        List<string> dndExtensions;

        public MyCallRoutingInterceptor()
        {
             dndExtensions = new List<string>();
        }

        public Destination GetDestination(RouteParams routeParams)
        {
            Console.WriteLine("Call is being routed with the following parameters: owner: {0}, caller id: {1}, dialed number: {2}, state: {3}", routeParams.CallerInfo.Owner, routeParams.CallerInfo.CallerId, routeParams.CallerInfo.DialedNumber, routeParams.State);

            if (routeParams.Destination.DialedNumber == "*90" && !dndExtensions.Contains(routeParams.CallerInfo.Owner))
            {
                Console.WriteLine("Extension {0} added to the DND list.", routeParams.CallerInfo.Owner);
                dndExtensions.Add(routeParams.CallerInfo.Owner);
            }

            if (routeParams.Destination.DialedNumber == "*91" && dndExtensions.Contains(routeParams.CallerInfo.Owner))
            {
                Console.WriteLine("Extension {0} removed from the DND list.", routeParams.CallerInfo.Owner);
                dndExtensions.Remove(routeParams.CallerInfo.Owner);
            }

            if (dndExtensions.Contains(routeParams.Destination.DialedNumber))
            {
                return new Destination(routeParams.CallerInfo.Owner, null, null, 10);
            }

            return null;
        }
    }
}
