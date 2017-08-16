// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.WebApi
{
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
