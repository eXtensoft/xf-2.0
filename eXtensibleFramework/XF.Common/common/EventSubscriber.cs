// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;

    public sealed class EventSubscriber : IDisposable
    {
        private ServiceHost host = null;
        private EventService service = null;
        private const string DefautPipeName = "pipeXF";


        public string ErrorMessage { get; set; }
        public string PipeName { get; set; }
        
        public Action<EventTypeOption,List<TypedItem>> HandleEvent
        {
            get { return service.HandleEvent; }
            set { service.HandleEvent = value; }
        }

        public EventSubscriber()
            : this(DefautPipeName) { }

        public EventSubscriber(string pipeName)
        {
            PipeName = pipeName;
            service = new EventService();
        }

        public bool ServiceOn()
        {
            return (TryHostService());
        }



        private bool TryHostService()
        {
            bool b = false;
            try
            {
                host = new ServiceHost(service, new Uri(EventService.URI));
                host.AddServiceEndpoint(typeof(IEventService), new NetNamedPipeBinding(), PipeName);
                host.Open();
                b = true;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return b;
        }





        #region IDisposable

        void IDisposable.Dispose()
        {
            if (host != null && host.State != CommunicationState.Closed)
            {
                host.Close();
            }
        }

        #endregion


    }
}
