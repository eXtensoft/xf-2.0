// <copyright company="eXtensible Solutions LLC" file="EventService.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

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
