// <copyright company="eXtensoft, LLC" file="AlertHandler.cs">
// Copyright © 2016 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

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
