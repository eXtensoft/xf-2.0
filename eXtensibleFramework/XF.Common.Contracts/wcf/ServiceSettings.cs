// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Channels;

    public sealed class ServiceSettings
    {
        #region properties

        public string EndpointName { get; private set; }

        public string AppContextKey { get; private set; }

        public string EndpointAddress { get; private set; }

        public bool UsesHttps { get; private set; }

        public BindingTypeOptions BindingType { get; private set; }

        public Binding ServiceBinding { get { return GenerateBinding(); } }

        #endregion properties

        #region constructors

        public ServiceSettings(string endpointName, string endpointAddress, BindingTypeOptions bindingType, string appContextKey = "")
        {
            EndpointName = (!String.IsNullOrEmpty(endpointName)) ? endpointName : String.Empty;

            if (!String.IsNullOrEmpty(endpointAddress))
            {
                EndpointAddress = endpointAddress;
                int index = endpointAddress.IndexOf("https");
                UsesHttps = (index > -1) ? true : false;
            }

            BindingType = bindingType;

            AppContextKey = (!String.IsNullOrEmpty(appContextKey)) ? appContextKey : String.Empty;
        }

        #endregion constructors

        private Binding GenerateBinding()
        {
            Binding binding = null;
            if (BindingType.HasFlag(BindingTypeOptions.BasicHttp))
            {
                binding = new BasicHttpBinding();
            }
            else if (BindingType.HasFlag(BindingTypeOptions.Tcp))
            {
                binding = new NetTcpBinding();
            }
            else if (BindingType.HasFlag(BindingTypeOptions.WsHttp))
            {
                binding = new WSHttpBinding();
            }
            if (binding == null)
            {
                binding = new WSHttpBinding();
            }
            return binding;
        }
    }
}
