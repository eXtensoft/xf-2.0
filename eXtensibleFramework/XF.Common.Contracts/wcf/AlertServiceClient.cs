// <copyright company="eXtensible Solutions LLC" file="AlertServiceClient.cs">
// Copyright © 2015 All Right Reserved
// </copyright>



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
