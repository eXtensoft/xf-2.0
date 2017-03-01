// <copyright file="ServiceClient`1.cs" company="eXtensible Solutions LLC">
// Copyright © 2015 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.ComponentModel.Composition;
    using System.ServiceModel;

    public abstract class ServiceClient<T>
    {
        #region local fields

        private ChannelFactory<T> _Factory = null;
        private T _Proxy;

        public T Proxy
        {
            get { return _Proxy; }
        }

        #endregion local fields

        #region properties

        private IUserIdentityProvider _IdentityProvider = null;

        [Import(typeof(IUserIdentityProvider), AllowDefault = true)]
        public IUserIdentityProvider IdentityProvider
        {
            get
            {
                return (_IdentityProvider != null) ? _IdentityProvider : new WindowsIdentityProvider();
            }
            set
            {
                _IdentityProvider = value;
            }
        }

        public string AppContextKey { get; set; }

        public string EndpointName { get; set; }

        #endregion properties

        public void Initialize(string endpointName = "default")
        {
            Initialize(endpointName, String.Empty);
        }



        public void Initialize(string endpointName, string appContextKey)
        {
            EndpointName = endpointName;
            AppContextKey = appContextKey;
            LocalInitialization();
        }

        public void Initialize(ServiceSettings settings)
        {
            AppContextKey = settings.AppContextKey;
            if (!String.IsNullOrEmpty(settings.EndpointName))
            {
                EndpointName = settings.EndpointName;
                LocalInitialization();
            }
            else if (!String.IsNullOrEmpty(settings.EndpointAddress))
            {
                _Factory = new ChannelFactory<T>(settings.ServiceBinding, new EndpointAddress(settings.EndpointAddress));
                CreateChannel();
            }
        }

        private void LocalInitialization()
        {
            CreateChannelFactory();
            CreateChannel();
        }

        protected void CreateChannelFactory()
        {
            _Factory = new ChannelFactory<T>(EndpointName);
        }

        private void CreateChannel()
        {
            _Proxy = _Factory.CreateChannel();
            ICommunicationObject obj = _Proxy as ICommunicationObject;
            obj.Faulted += new EventHandler(InnerChannel_Faulted);
        }

        private void AbortChannel()
        {
            ICommunicationObject obj = _Proxy as ICommunicationObject;
            if (obj != null)
            {
                obj.Abort();
            }
        }

        protected void RecyleChannel()
        {
            AbortChannel();
            CreateChannel();
        }

        private void InnerChannel_Faulted(object sender, EventArgs e)
        {
            RecyleChannel();
        }
    }
}

