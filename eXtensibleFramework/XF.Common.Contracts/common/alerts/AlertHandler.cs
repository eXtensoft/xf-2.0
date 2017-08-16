// Licensed to eXtensoft LLC under one or more agreements.
// eXtensoft LLC licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XF.Common
{
    public sealed class AlertHandler
    {

        private static object sync = new object();
        private static volatile IAlertPublisher publisher;

        public static IAlertPublisher Publisher
        {
            get
            {
                if (publisher == null)
                {
                    lock(sync)
                    {
                        if (publisher == null)
                        {
                            try
                            {
                                publisher = AlertPublisherLoader.Load();
                            }
                            catch 
                            {
                            }
                        }
                    }
                }
                return publisher;
            }
        }

    }

}
