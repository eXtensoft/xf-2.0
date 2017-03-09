// <copyright company="Recorded Books, Inc" file="ResponseConfiguration.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

namespace XF.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class ResponseConfiguration
    {
        public static HttpMessageProvider MessageProvider { get; private set; }

        public static void Configure(HttpMessageProvider messageProvider)
        {
            if (MessageProvider == null)
            {
                MessageProvider = messageProvider;
            }
        }

        static ResponseConfiguration()
        {
            MessageProvider = new XmlHttpMessageProvider();
            try
            {
                MessageProvider.Initialize();
            }
            catch
            {
            }
            
        }
    }

}
