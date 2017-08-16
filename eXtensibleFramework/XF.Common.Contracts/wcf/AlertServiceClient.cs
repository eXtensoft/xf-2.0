// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System.Collections.Generic;
    using XF.Common.Wcf;

    public class AlertServiceClient : ServiceClient<IAlertService>, IAlertService
    {
        public AlertServiceClient() 
        {

        }

        public AlertServiceClient(ServiceSettings settings)
        {
            Initialize(settings);
        }

        void IAlertService.Post(AlertSubscriber model)
        {
            Proxy.Post(model);
        }

        void IAlertService.Put(AlertSubscriber model)
        {
            Proxy.Put(model);
        }

        void IAlertService.Delete(string id)
        {
            Proxy.Delete(id);
        }

        AlertSubscriber IAlertService.Get(string id)
        {
            return Proxy.Get(id);
        }

        IEnumerable<AlertSubscriber> IAlertService.GetAll()
        {
            return Proxy.GetAll();
        }

        IEnumerable<IProjection> IAlertService.GetAllProjections()
        {
            return Proxy.GetAllProjections();
        }
    }
}
