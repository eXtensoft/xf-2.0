// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;

    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]   
    public class EventService  : IEventService
    {
        public static string URI = "net.pipe://localhost/pipe/foobar";

        public Action<EventTypeOption, List<TypedItem>> HandleEvent { get; set; }



        void IEventService.Write(EventTypeOption option, List<TypedItem> items)
        {
            if (HandleEvent != null)
            {
                HandleEvent(option, items);
            }
        }
    }
}
